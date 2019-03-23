using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services.Projectiles;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Skills
{
    class RuyoSkillTwo : MonoBehaviour, ISkill, ITimedSkill
    {
        #region Members
        ///<summary>The amount of lightnings of this skill.</summary>
        private int lightningsCount { get; set; }
        ///<summary>The bounds of the crosshair connected with this skill.
        ///Necessary for the calculation of the lightnings' offset.</summary>
        public Bounds CrosshairBounds { get; set; }

        public int LightningsCount { get; set; }
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
            GameObject newProjectile;

            float lightningsOffset = CrosshairBounds.size.x / lightningsCount;

            //let the projectile itself spawn the lightnings on it's own.
            //dirVector, in this case, is the position on the client's screen.
            newProjectile = Instantiate(Projectile, dirVector, Quaternion.identity) as GameObject;

            var onCollision = newProjectile.GetComponent<Projectiles.OnCollisionProjectile>();
            onCollision.SetParams(teamTag);

            var destroyByTime = newProjectile.GetComponent<Projectiles.DestroyByTime>();
            destroyByTime.SetLifeTime(SkillLifeTime);

            var proj2 = newProjectile.GetComponent<RuyoProjectile2Anim>();
            proj2.SetLightningsAmount(lightningsOffset, SkillLifeTime);

            StartSkillCD();
        }
        /// <summary>
        /// Starts the cooldown and makes the skill inactive.
        /// </summary>
        private void StartSkillCD()
        {
            SkillCurrCD = 0.0f;
            IsActive = false;
            IsRecharged = false;
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

        public void UpdateSkillCD(float lastFrameTime)
        {
            SkillCurrCD += lastFrameTime;
            if (SkillMaxCD >= SkillCurrCD)
            {
                IsRecharged = true;
            }
        }

    }
}
