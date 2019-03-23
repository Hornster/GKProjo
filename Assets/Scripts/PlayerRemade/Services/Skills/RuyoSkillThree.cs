using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Skills
{
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
        #endregion

        public void UseSkill(Vector2 dirVector, Teams teamTag, Vector2 playerSpeed)
        {
            dirVector = GetTeleportCoords(dirVector);

            if ((GetVectorSquareCoords(Camera.main.ScreenToWorldPoint(playerSpeed))) 
                - GetVectorSquareCoords(PlayerTransform.GetTransform().position) <= SkillValue * SkillValue)
            {
                Vector3 position = PlayerTransform.GetTransform().position + new Vector3(dirVector.x, dirVector.y, 0);
                PlayerTransform.GetTransform().position = position;
            }
            StartSkillCD();
            
        }
        ///<summary>Calculates the angle for the teleportation, then returns the vector by which should be the character moved.</summary>
        ///<param name="dirVector"> The teleportation direction vector.</param>
        private Vector2 GetTeleportCoords(Vector2 dirVector)
        {
            double angle = dirVector.y / dirVector.y;
            dirVector.x *= (float)(System.Math.Sin(angle) * TeleportRange);
            dirVector.y *= (float)(System.Math.Cos(angle) * TeleportRange);
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
            if (SkillMaxCD >= SkillCurrCD)
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
