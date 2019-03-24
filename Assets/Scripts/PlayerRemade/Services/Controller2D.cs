using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Model;
using Assets.Scripts.PlayerRemade.Services.Characters;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// You need to create a collision layer and assign all obstacles to the layer and select
    /// the layer in the options of the script under the "collisionMask" property.
    /// Has to be on the player's character's gameobject.
    /// </summary>
    [RequireComponent(typeof (BoxCollider2D))]
    public class Controller2D : MonoBehaviour, IController2D, IAnimationData {
        #region Members

        private IList<IObserver<IAnimationData>> _observingAnimators = new List<IObserver<IAnimationData>>();
        //Used in determining 
        ClimbDeterminator _climbDeterminator;
        //used for making sure that input is NOT USED because that ASSHOLE is DRUNK TO THE BOOT and detects input which is NOT PRESENT.
        const float InputPadding = 0.0005f;

        public const float SkinWidth = .015f;
        public const float MaxClimbDistanceFromWall = 0.3f;
        public const float MinClimbDistanceFromWall = 0.01f;
        public int HorizontalRayCount = 6;
        public int VerticalRayCount = 4;
        public LayerMask CollisionMask;
        public float MaxClimbAngle = 60;
        public float MaxDescendAngle = 60;
        public float MaxWallClimbAngle = 110;

        float _horizontalRaySpacing, _verticalRaySpacing;

        private float _currJumpVelocity;
        private float _currDoubleJumpFactor;
        private float _currClimbMoveSpeed;
        /// <summary>
        /// Gets the collisions clone. Used by IAnimationData interface.
        /// </summary>
        public CollisionInfo Collisions
        {
            get { return collisions; }
        }
        /// <summary>
        /// Gets the movement clone. Used by IAnimationData interface.
        /// </summary>
        public MovementInfo Movement
        {
            get { return movement; }
        }
        public CollisionInfo collisions = new CollisionInfo();
        public MovementInfo movement = new MovementInfo();

        BoxCollider2D collider;
        RaycastOrigins raycastOrigins;
        
        #endregion

        private void Start()
        {
            collider = GetComponent<BoxCollider2D>();
            movement.isClimbing = false;
            movement.canDoubleJump = true;

            _climbDeterminator = new ClimbDeterminator();

            CalculateRaySpacing();
        }

       /* public void RotateClimbingCharacter(Vector3 currentVelocity)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(currentVelocity.x), transform.localScale.y, transform.localScale.z);
        }*/
        public Vector3 PrepareMove(ref Vector3 velocity, Vector2 rawInput, float currJumpVelocity,
            float currDoubleJumpFactor, float currClimbMoveSpeed,float currGravity, float lastFrameTime)
        {
            Vector3 timeScaledVelocity;
            //Check if the player is climbing or standing on ground or colliding with ceiling.
            //If yes - set their Y velocity to 0
            velocity.y = ChkVerticalCollisions(velocity.y);

            _currClimbMoveSpeed = currClimbMoveSpeed;
            _currDoubleJumpFactor = currDoubleJumpFactor;
            _currJumpVelocity = currJumpVelocity;

            currGravity *= lastFrameTime;

            ChkInputs(ref velocity, rawInput, currGravity);
            timeScaledVelocity = new Vector3(velocity.x,velocity.y, velocity.z);
            //Finished checking input. Multiply the velocity and gravity by last frame time.
            timeScaledVelocity *= lastFrameTime;

            SetClimbingYProjectionLength(currGravity);

            if (Mathf.Abs(timeScaledVelocity.x) < InputPadding)
            {
                timeScaledVelocity.x = 0;
            }
            collisions.Reset();

            bool isClimbingUpKeyPressed = RuyoInputReceiver.GetInstance().GetKey(RuyoInputReceiver.ClimbKey);

            ClimbingUpOrDown();
            movement.SetIsClimbing(isClimbingUpKeyPressed);
            UpdateRayCastOrigins();
            

            collisions.velocityOld = timeScaledVelocity;

            if (timeScaledVelocity.y < 0 && !IsClimbingWall())
            {
                DescendSlope(ref timeScaledVelocity);
            }

            if (timeScaledVelocity.x != 0)
            {
                HorizontalCollisions(ref timeScaledVelocity);
            }

            if (IsClimbingWall() && !(collisions.below || collisions.above))
            {
                ChkWallCollisions(ref timeScaledVelocity);//TODO repair the x velocity when shift pressed bug in here
            }

            if (timeScaledVelocity.y != 0)
                VerticalCollisions(ref timeScaledVelocity);

            movement.ResetDoubleJump(collisions);

            //transform.Translate(velocity);

            NotifyObservers();

            return timeScaledVelocity;
        }
        /// <summary>
        /// Checks the inputs of special movement keys (jump, climbing)
        /// and raw input. These need special care since current collision status has
        /// influence on these and vice-versa.
        /// </summary>
        /// <param name="input">Raw input from X and Y axes (Input.GetAxes).</param>
        /// <param name="currentGravity">Current gravity (NOT(!!!!!!!!!!!) scaled by lastFrameTime).</param>
        void ChkInputs(ref Vector3 currentVelocity, Vector2 input, float currentGravity)
        {
            if (input.y > 0)
            {
                movement.climbUp = true;
                movement.climbDown = false;
            }
            else if (input.y < 0)
            {
                movement.climbUp = false;
                movement.climbDown = true;
            }
            else
            {
                movement.climbUp = movement.climbDown = false;
            }

            movement.climbKeyOn = RuyoInputReceiver.GetInstance().GetKey(RuyoInputReceiver.ClimbKey);

            if (RuyoInputReceiver.GetInstance().GetKeyDown(RuyoInputReceiver.JumpKey))
            {
                if (collisions.below)
                    currentVelocity.y = _currJumpVelocity;
                else if (CanDoubleJump())
                {
                    currentVelocity.y = _currJumpVelocity * _currDoubleJumpFactor;
                    movement.canDoubleJump = false;
                }
            }
            else if (movement.climbKeyOn && ((collisions.left || collisions.right) && IsClimbingWall()))
            {
                // float targetVelocityY = input.y * _currClimbMoveSpeed;
                //velocity.y = Mathf.SmoothDamp(velocity.y, targetVelocityY, ref velocityYSmoothing, accelerationTimeGrounded);
                currentVelocity.y = input.y * _currClimbMoveSpeed;
            }
            else
                currentVelocity.y += currentGravity;
        }

        public void SetClimbingYProjectionLength(float length)
        {
            movement.SetProjectionLength(length);
        }

        /// <summary>
        /// Determines, basing only on the state of Y axis, whether is the character climbing up or down. 
        /// </summary>
        void ClimbingUpOrDown()
        {
            Vector2 input = RuyoInputReceiver.GetInstance().RetrieveInput();
            if (input.y > 0)
            {
                movement.climbUp = true;
                movement.climbDown = false;
            }
            else if (input.y < 0)
            {
                movement.climbUp = false;
                movement.climbDown = true;
            }
            else
            {
                movement.climbUp = movement.climbDown = false;
            }
        }

        void HorizontalCollisions(ref Vector3 velocity)
        {
            float directionX = Mathf.Sign(velocity.x);
            float rayLength = Mathf.Abs(velocity.x) + SkinWidth;

            for (int i = 0; i < HorizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (_horizontalRaySpacing * i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, CollisionMask);

                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);

                if (hit)
                {
                    float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                

                    if (i == 0 && slopeAngle <= MaxClimbAngle)
                    {
                        if(collisions.descendingSlope)
                        {
                            collisions.descendingSlope = false;
                            velocity = collisions.velocityOld;
                        }
                        float distanceToSlopeStart = 0;
                        if(slopeAngle != collisions.slopeAngleOld)
                        {
                            distanceToSlopeStart = hit.distance - SkinWidth;
                            velocity.x = distanceToSlopeStart * directionX;
                        }
                        ClimbSlope(ref velocity, slopeAngle);
                        velocity.x += distanceToSlopeStart * directionX;
                    }

                    if (!collisions.climbingSlope || slopeAngle > MaxClimbAngle)
                    {
                        movement.HoldLeftWall(directionX);
                        velocity.x = (hit.distance - SkinWidth) * directionX;
                        rayLength = hit.distance;

                        if(collisions.climbingSlope)
                        {
                            velocity.y = Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Abs(velocity.x);
                        }

                        collisions.left = directionX == -1;
                        collisions.right = directionX == 1;
                    }
                    ////////////////////////////////////////
                    if(!collisions.climbingSlope && !collisions.descendingSlope)
                    {
                        if (movement.climbKeyOn && slopeAngle < MaxWallClimbAngle && slopeAngle > MaxClimbAngle)
                        {
                            if (!movement.climbUp && !movement.climbDown)
                                velocity.y = 0;

                            movement.isClimbing = true;
                        }
                        else
                            movement.isClimbing = false;
                    }
                    else
                        movement.isClimbing = false;
                    ////////////////////////////////////////
                }
            
            }
        }
        //isStillClimbing is set to false if more than (or equal to) half of the horizontal rays can't detect the wall.
        //If only upper rays cannot detect the wall, then the character is given an extra kick of velocity.y which allows him to
        //jump on the ledge
        void ChkWallCollisions(ref Vector3 velocity)
        {
            RaycastHit2D[] rayHits = new RaycastHit2D[2]; //we need only the furthest and nearest raycasthit
            bool isStillClimbing = true;
            int notCollidingRaysCounter = 0;
            float directionX = (movement.holdsLeftWall) ? -1 : 1;
            float distanceToWallMove = MaxClimbDistanceFromWall * directionX; //the max distance that the character can move towards the wall.
            float rayLength = Mathf.Abs(distanceToWallMove) + SkinWidth;

            float smallestDistanceFromWall = MaxClimbDistanceFromWall;

            for(int i = 0; i < HorizontalRayCount; i++)
            {
                Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
                rayOrigin += Vector2.up * (_horizontalRaySpacing *  i);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, CollisionMask);
                Debug.DrawRay(rayOrigin, Vector2.right * directionX * rayLength, Color.red);
                if (!hit)
                {
                    notCollidingRaysCounter++;
                    if(notCollidingRaysCounter > HorizontalRayCount/2)
                        isStillClimbing = false;
                }
                else
                {
                    SetRaycastHits(rayHits, hit);
                    if (directionX < 0)
                        collisions.left = true;
                    else
                        collisions.right = true;

                    

                    if(hit.distance < smallestDistanceFromWall)
                    {
                        smallestDistanceFromWall = hit.distance;
                    }
                }
            }

            if (!isStillClimbing)
            {
                movement.isClimbing = false;
            }
            else
            {
                velocity.x = 0;
                if (movement.climbDown || movement.climbUp)
                    velocity = GetXInClimbing(rayHits, velocity);

                ChkSmallestDistanceFromWall(smallestDistanceFromWall, ref velocity);
            }
            
        }
        /// <summary>
        /// Checks whether is the character too close to the wall - this can cause jamming the character and other weird behaviour.
        /// If the character is too close - sets the x param of the velocity vector to minClimbDistanceFromWall
        /// (taking into account the direction)
        /// </summary>
        /// <param name="smallestDistanceFromWall">The smallest distance from the wall among the colliding rays.</param>
        /// <param name="velocity">The velocity vector of the character.</param>
        void ChkSmallestDistanceFromWall(float smallestDistanceFromWall, ref Vector3 velocity)
        {
            if(smallestDistanceFromWall < MinClimbDistanceFromWall)
            {
                if(collisions.left)
                {
                    velocity.x += MinClimbDistanceFromWall;
                }
                else if(collisions.right)
                {
                    velocity.x -= MinClimbDistanceFromWall;
                }
            }
        }
        void SetRaycastHits(RaycastHit2D[] borderHits, RaycastHit2D hit)
        {
            if(!borderHits[0])
            {
                borderHits[0] = hit;
            }
            else
            {
                borderHits[1] = hit;
            }
        }
        /// <summary>
        /// returns the value of distance to the wall from the closest ray, depending whether is the character
        /// climbing up or down 
        /// </summary>
        /// <param name="borderHits">the highest and lowest rays that have hit the wall.</param>
        /// <param name="velocity">the velocity of the character as a 2D vector</param>
        /// <returns>New velocity vector that will put the player in proper place near the angled wall</returns>
        Vector2 GetXInClimbing(RaycastHit2D[] borderHits, Vector2 velocity)
        {
            try
            {
                Vector2 wallVector = borderHits[0].point - borderHits[1].point;
                float velocityMagnitude = velocity.magnitude;
                wallVector.Normalize();

                wallVector.x = Mathf.Abs(wallVector.x) * Mathf.Sign(velocity.x);
                wallVector.y = Mathf.Abs(wallVector.y) * Mathf.Sign(velocity.y);

                Vector2 resultVector = new Vector2(velocityMagnitude * wallVector.x, velocityMagnitude * wallVector.y); //when the wall is to the left of the character

                resultVector.x *= _climbDeterminator.GetSignForWallClimb(movement.climbDown, !movement.holdsLeftWall, borderHits[0].point, borderHits[1].point);
                //if (!movement.holdsLeftWall)                            //in case the wall is to the right from the character, we need to negate the x velocity
                //    resultVector.x = -resultVector.x;

                return resultVector;
            }
            catch(UnityException ex)
            {
                throw ex;
            }
        
        
        }
        void VerticalCollisions(ref Vector3 velocity)
        {
            float directionY = Mathf.Sign(velocity.y);
            float rayLength = Mathf.Abs(velocity.y) + SkinWidth;
        
            for (int i = 0; i < VerticalRayCount; i++)
            {
                Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
                rayOrigin += Vector2.right *( _verticalRaySpacing * i + velocity.x);
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, CollisionMask);

                if(hit)
                {
                    velocity.y = (hit.distance - SkinWidth) * directionY;
                    rayLength = hit.distance;

                    if(collisions.climbingSlope)
                    {
                        velocity.x = velocity.y / Mathf.Tan(collisions.slopeAngle * Mathf.Deg2Rad) * Mathf.Sign(velocity.x);
                    }

                    collisions.below = directionY == -1;
                    collisions.above = directionY == 1;
                }
            
                Debug.DrawRay(rayOrigin, Vector2.up * directionY * rayLength, Color.red);
            }
            if(collisions.climbingSlope)
            {
                float directionX = Mathf.Sign(velocity.x);
                rayLength = Mathf.Abs(velocity.x) + SkinWidth;
                Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomLeft:raycastOrigins.bottomRight) + Vector2.up * velocity.y;
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right *directionX, rayLength, CollisionMask);

                if(hit)
                {
                    float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                    if(slopeAngle != collisions.slopeAngle)
                    {
                        velocity.x = (hit.distance - SkinWidth) * directionX;
                        collisions.slopeAngle = slopeAngle;
                    }
                }
            }
        }

        void ClimbSlope(ref Vector3 velocity, float slopeAngle)
        {
            float moveDistance = Mathf.Abs(velocity.x);
            float climbVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;

            if (velocity.y <= climbVelocityY)
            {
                velocity.y = climbVelocityY;
                velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                collisions.below = true;
                collisions.climbingSlope = true;
                collisions.slopeAngle = slopeAngle;
            }
        }

        void DescendSlope(ref Vector3 velocity)
        {
            float directionX = Mathf.Sign(velocity.x);
            Vector2 rayOrigin = ((directionX == -1) ? raycastOrigins.bottomRight : raycastOrigins.bottomLeft);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, -Vector2.up, Mathf.Infinity, CollisionMask);

            if(hit)
            {
                float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
                if(slopeAngle != 0 && slopeAngle <= MaxDescendAngle)
                {
                    if(Mathf.Sign(hit.normal.x) == directionX)
                    {
                        if(hit.distance - SkinWidth <= Mathf.Tan(slopeAngle * Mathf.Deg2Rad)*Mathf.Abs(velocity.x))
                        {
                            float moveDistance = Mathf.Abs(velocity.x);
                            float descendVelocityY = Mathf.Sin(slopeAngle * Mathf.Deg2Rad) * moveDistance;
                            velocity.x = Mathf.Cos(slopeAngle * Mathf.Deg2Rad) * moveDistance * Mathf.Sign(velocity.x);
                            velocity.y -= descendVelocityY;

                            collisions.slopeAngle = slopeAngle;
                            collisions.descendingSlope = true;
                            collisions.below = true;
                        }
                    }
                }
            }
        }

        void UpdateRayCastOrigins()
        {
            Bounds bounds = collider.bounds;
            bounds.Expand(SkinWidth * -2);

            raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
            raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
            raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
            raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
        }
	
        void CalculateRaySpacing()
        {
            Bounds bounds = collider.bounds;
            bounds.Expand(SkinWidth * -2);

            HorizontalRayCount = Mathf.Clamp(HorizontalRayCount, 2, int.MaxValue);
            VerticalRayCount = Mathf.Clamp(VerticalRayCount, 2, int.MaxValue);

            _horizontalRaySpacing = bounds.size.y / (HorizontalRayCount -1);
            _verticalRaySpacing = bounds.size.x / (VerticalRayCount - 1);
        }

        private void NotifyObservers()
        {
            foreach (var observer in _observingAnimators)
            {
                observer.Notify(this);
            }
        }

        public bool IsClimbingWall()
        {
            return movement.isClimbing;
        }

        private bool CanDoubleJump()
        {
            if (movement.canDoubleJump && !collisions.left && !collisions.right)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Checks if the player is colliding with ceiling or ground or is climbing.
        /// If yes - sets the velocity to 0. Otherwise returns same elocity as provided.
        /// </summary>
        /// <param name="velocityY">Current Y velocity of the player's character.</param>
        /// <returns></returns>
        private float ChkVerticalCollisions(float velocityY)
        {
            if (collisions.above || collisions.below || (movement.isClimbing && movement.climbKeyOn && (collisions.left || collisions.right)))
                velocityY = 0.0f;

            return velocityY;
        }

        public void AddObserver(IObserver<IAnimationData> observer)
        {
            _observingAnimators.Add(observer);
        }

    }
}
