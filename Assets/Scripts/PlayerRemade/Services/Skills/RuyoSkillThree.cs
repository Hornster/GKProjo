using System.Collections.Generic;
using System.Drawing;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Skills
{
    /// <summary>
    /// Needs to have the CharacterColliderTag set in the editor, both on gameobject with the
    /// collider for the character body and in the script field. This allows the character to teleport
    /// without the risk of being tossed outside of the map or becoming one with the ground.
    /// </summary>
    class RuyoSkillThree : MonoBehaviour, ISkill, ITimedSkill
    {
        #region Members

        public IGetTransform PlayerTransform { get; set; }
        public float TeleportRange { get; set; }
        public float SkillLifeTime { get; set; }
        public SkillType skillType { get; set; }
        public float SkillValue { get; set; }
        public bool IsRecharged { get; set; }
        public bool IsActive { get; set; }
        public float SkillMaxCD { get; set; }
        public float SkillCurrCD { get; set; }
        public Sprite SkillCrosshair { get; set; }
        public GameObject Projectile { get; set; }
        public GameObject GameObject { get; set; }

        public string CharacterColliderTag;
        
        public Sprite icon
        {
            get { return skillIcon; }
        }

        private Collider2D collider;
        private CastRays castRays;
        [SerializeField]
        private LayerMask checkedLayers;
        [SerializeField]
        private Vector2 skinWidth;//Distance (divided by half) by which the ray origins will be pushed inside the collider.

        [SerializeField]
        private Sprite skillIcon;
        #endregion

        void Start()
        {
            var availableColliders = PlayerTransform.GetTransform().gameObject.GetComponentsInChildren<BoxCollider2D>();
            foreach (var collider in availableColliders)
            {
                if (collider.tag == CharacterColliderTag)
                {
                    this.collider = collider;
                    break;
                }
            }

            castRays = CastRays.GetInstance();
        }

        public void UseSkill(Vector2 dirVector, Teams teamTag, Vector2 playerSpeed)
        {
            //dirVector = GetTeleportCoords(dirVector);

            var bodyColliderBounds = collider.bounds;
            var rectangleShape = new RectangleF(bodyColliderBounds.center.x, bodyColliderBounds.center.y, bodyColliderBounds.size.x - skinWidth.x, bodyColliderBounds.size.y - skinWidth.y);

            if ((GetVectorSquareCoords(Camera.main.ScreenToWorldPoint(playerSpeed))) 
                - GetVectorSquareCoords(PlayerTransform.GetTransform().position) <= SkillValue * SkillValue)
            {
                Vector2 movementVector;
                List<LaserPartData> placeholder;   //For now, simply stores all the chunks of the path.
                (placeholder, movementVector) = castRays.ProjectRectangle(rectangleShape, dirVector, TeleportRange, checkedLayers);

                Vector3 position = PlayerTransform.GetTransform().position + new Vector3(movementVector.x, movementVector.y, 0);
                PlayerTransform.GetTransform().position = position;
            }
            StartSkillCD();
        }
        ///<summary>Calculates the angle for the teleportation, then returns the vector by which should be the character moved.</summary>
        ///<param name="dirVector"> The teleportation direction vector.</param>
        private Vector2 GetTeleportCoords(Vector2 dirVector)
        {
            dirVector.Normalize();
            dirVector *= TeleportRange;
            return dirVector;
        }
        /// <summary>
        /// Calculates sum of squared vector parts (x and y only)
        /// </summary>
        /// <param name="vect">Source vector</param>
        /// <returns>Sum of squared vector parts (x and y only)</returns>
        private float GetVectorSquareCoords(Vector3 vect)
        {
            return vect.x * vect.x + vect.y * vect.y;
        }
        /// <summary>
        /// Starts the cooldown and makes the skill inactive.
        /// </summary>
        private void StartSkillCD()
        {
            SkillCurrCD = 0.0f;
            IsRecharged = false;
            IsActive = false;
        }
        public void UpdateSkillCD(float lastFrameTime)
        {
            SkillCurrCD += lastFrameTime;
            if (SkillMaxCD <= SkillCurrCD)
            {
                IsRecharged = true;
            }
        }

        public bool ActivateSkill()
        {
            if (IsRecharged)
            {
                IsActive = true;
            }

            return IsActive;
        }

        public void DeactivateSkill()
        {
            IsActive = false;
        }
        
    }
}
