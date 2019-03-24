
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Model;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    class AnimationSupervisor : IObserver<IAnimationData>
    {

        #region Members

        /// <summary>
        /// The reference to the AnimationController that has direct influence on the character's animations.
        /// Set through the code.
        /// </summary>
        AnimationController animController;

        private MovementInfo _movementInfo;
        private CollisionInfo _collisionInfo;
        private bool _isClimbing;

        /// <summary>
        /// The reference to the Controller2D that stores data about movement and collisions state.
        /// Set through the code.
        /// </summary>
        //Controller2D collisionData;
        #endregion
        #region Ctors
        public AnimationSupervisor(AnimationController animationController)//, Controller2D collisionsController)
        {
            animController = animationController;
            //collisionData = collisionsController;
        }

        #endregion

        #region Functionalities
        /// <summary>
        /// Performs checks for any changes in animation state.
        /// </summary>
        /// <param name="currentVelocity">The velocity of the character during the current frame.</param>
        public void ChkAnims(Vector3 currentVelocity, Vector3 crosshairPosition, Vector2 maxVelocity, Transform playerTransform)
        {
            //before checking the horizontal velocity, let's make sure that the object's value is in fact 0
            //the object was copied so there's high probability that the value will be really close to 0, but not equal to it.
            // Darn floats! xD
            if (Mathf.Abs(currentVelocity.x) < 0.00001f)
                currentVelocity.x = 0;

            SetRunBlendTreeParameter(currentVelocity, crosshairPosition, maxVelocity, playerTransform);

            DetectJump();
            DetectClimbing();


            if (!animController.IsHoldingWall)
            {
                animController.IsRunning = currentVelocity.x != 0.0f;

                if (_collisionInfo.below)
                {
                    animController.IsGrounded = true;
                    animController.IsJumping = false;
                    animController.IsDoubleJumping = false;
                    animController.CanDoubleJump = true;
                }
                else
                    animController.IsGrounded = false;
            }
            else
            {
                OnClimbRotate(playerTransform);
            }

            animController.VelocityY = currentVelocity.y;
        }

        void OnClimbRotate(Transform playerTransform)
        {
            if (_collisionInfo.right)
            {
                playerTransform.localScale = new Vector3(-Mathf.Abs(playerTransform.localScale.x), playerTransform.localScale.y, playerTransform.localScale.z);//If the player is holding a wall from right side - force rotation to right
            }
            else if(_collisionInfo.left)
            {
                playerTransform.localScale = new Vector3(Mathf.Abs(playerTransform.localScale.x), playerTransform.localScale.y, playerTransform.localScale.z);//If the player is holding a wall from left side - force rotation to left
            }
            animController.IsRunningBackwards = false;
            /*if (animController.IsRunningBackwards)
            {
                playerTransform.localScale = new Vector3(-playerTransform.localScale.x, playerTransform.localScale.y, playerTransform.localScale.z);
                
            }*/
        }
        /// <summary>
        /// Determines whether does the character run towards the crosshair. If not, forces Backward Running
        /// animation set. Position of the character is taken from the gameObject which is this script attached to.
        /// </summary>
        /// <param name="currentVelocity">The current velocity vector of the played (WITHOUT frame time influence!)</param>
        /// <param name="crosshairPosition">The position of the crosshair.</param>
        /// <param name="maxVelocity">The max available velocity for this chracter, as a vector.</param>
        void SetRunBlendTreeParameter(Vector3 currentVelocity, Vector3 crosshairPosition, Vector2 maxVelocity, Transform playerTransform)
        {
            //Equals 1 if the crosshair is to the left of the character. 1 otherwise.
            int sideOfCrosshair = ChkCharCursorPosition(crosshairPosition, playerTransform.position);
            float rescaleX = Mathf.Abs(playerTransform.localScale.x);
            bool shouldRunBackwards = false;

            animController.NormalizeRunSpeed = Mathf.Abs(currentVelocity.x / maxVelocity.x);
            
            if (currentVelocity.x > 0)
            {
                if (sideOfCrosshair > 0)
                {
                    rescaleX *= -1;
                    shouldRunBackwards = false;
                }
                else
                {
                    //SideOfCroshair < 0
                    rescaleX *= 1;
                    shouldRunBackwards = true;
                    animController.NormalizeRunSpeed = -animController.NormalizeRunSpeed;
                }
            }
            else
            {
                //CurrVelocity.x < 0
                if (sideOfCrosshair > 0)
                {
                    rescaleX *= -1;
                    shouldRunBackwards = true;
                    animController.NormalizeRunSpeed = -animController.NormalizeRunSpeed;
                }
                else
                {
                    //SideOfCroshair < 0
                    rescaleX *= 1;
                    shouldRunBackwards = false;
                }
            }
            playerTransform.localScale = new Vector3(rescaleX, playerTransform.localScale.y,
                playerTransform.localScale.z);
        
            SetIsRunningBackwards(shouldRunBackwards);

        }
       

        public void Notify(IAnimationData observedObject)
        {
            _collisionInfo = observedObject.Collisions;
            _movementInfo = observedObject.Movement;
        }
        /// <summary>
        /// Substracts the x values (cursorPosition - characterPosition) in order to find out
        /// on what side of the character the crosshair currently is.
        /// Returns the sign only:
        /// -1 if the crosshair is on the left
        /// 1 if the crosshair is on the right.
        /// </summary>
        /// <param name="crosshairPosition">2D vector of cursor position</param>
        /// <param name="characterPosition">2D vector of character position</param>
        int ChkCharCursorPosition(Vector3 crosshairPosition, Vector3 characterPosition)
        {
            float test = crosshairPosition.x - characterPosition.x;
            return System.Math.Sign(test);
                //return -1;   //the crosshair is on the right of the character
            
                //return 1;  //the crosshair is on the left of the character
        }
        /// <summary>
        /// Sets the IsRunningBackwards parameter in animController.
        /// </summary>
        /// <param name="isRunningBackwards">Value that is going to be set.</param>
        void SetIsRunningBackwards(bool isRunningBackwards)
        {
            animController.IsRunningBackwards = isRunningBackwards;
        }
        /// <summary>
        /// Checks for changes in animation state connected with climbing walls.
        /// </summary>
        void DetectClimbing()
        {
            if (_movementInfo.isClimbing)
            {
                animController.IsJumping = false;
                animController.IsDoubleJumping = false;
                animController.IsHoldingWall = true;
                if (_movementInfo.climbUp)
                {
                    animController.IsClimbingUp = true;
                    animController.IsClimbingDown = false;
                }
                else if (_movementInfo.climbDown)
                {
                    animController.IsClimbingUp = false;
                    animController.IsClimbingDown = true;
                }
                else
                {
                    animController.IsClimbingUp = false;
                    animController.IsClimbingDown = false;
                }
            }
            else
            {
                animController.IsHoldingWall = false;
                if (!_movementInfo.isClimbing)
                {
                    animController.IsClimbingUp = false;
                    animController.IsClimbingDown = false;
                    //animController.IsGrounded = false;
                }

            }
        }
        /// <summary>
        /// Checks for changes in animation state connected with jumping.
        /// </summary>
        void DetectJump()
        {
            if (!animController.IsJumping || !animController.IsDoubleJumping)
                if (Input.GetButtonDown("Jump"))
                {
                    animController.IsGrounded = false;
                    if (!animController.IsJumping)
                    {
                        animController.IsJumping = true;
                    }
                    else if (!animController.IsDoubleJumping)
                    {
                        animController.IsDoubleJumping = true;
                        animController.CanDoubleJump = false;
                    }
                }
        }
        
        #endregion
    }
}
