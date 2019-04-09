using Assets.Scripts.Emitters;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services.Projectiles;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Skills
{
    ///
    class RuyoSkillBase : MonoBehaviour, IDefaultSkill
    {
        #region Members

        public bool IsDefault { get; set; }
        public SkillType skillType { get; set; }
        public float SkillValue { get; set; }
        public bool IsRecharged { get; set; }
        public bool IsActive { get; set; }
        public float SkillMaxCD { get; set; }
        public float SkillCurrCD { get; set; }
        public Sprite SkillCrosshair { get; set; }
        public GameObject Projectile { get; set; }

        private IParticleEmitter emitter;

        public Sprite icon
        {
            get { return skillIcon; }
        }
        
        [SerializeField]
        private Sprite skillIcon;

        /// <summary>
        /// The transform of the projectile launcher.
        /// </summary>
        public IGetTransform shotSpawner { get; set; }
        #endregion

        #region Ctors
        /// <summary>
        /// 
        /// </summary>
        /// <param name="shotSpawner">If the skill needs the transform of the projectile launcher,
        /// it HAS to be given at the very beggining.</param>
        public RuyoSkillBase(ref IGetTransform shotSpawner)
        {
            this.shotSpawner = shotSpawner;
        }
        #endregion

        public void Start()
        {
            emitter = GetComponentInChildren<IParticleEmitter>();
        }
        public void UseSkill(Vector2 dirVector, Teams teamTag, Vector2 playerSpeed)
        {
            GameObject newProjectile;

            newProjectile = Instantiate(Projectile, shotSpawner.GetTransform().position, Quaternion.identity) as GameObject;

            OnCollisionProjectile onCollision = newProjectile.GetComponent<OnCollisionProjectile>();
            onCollision.SetParams(teamTag);

            DestroyByTime destroyByTime = newProjectile.GetComponent<DestroyByTime>();
            RuyoProjectileBasicData projData = newProjectile.GetComponent<RuyoProjectileBasicData>();
            destroyByTime.SetLifeTime(projData.SkillDuration);

            emitter?.AssignParent(newProjectile.transform);

            IProjectileMover mover = newProjectile.GetComponent<ProjectileMover>();
            mover.Initialize();
            mover.SetMoveDirection(dirVector, playerSpeed);
            

            StartSkillCD();
        }
        /// <summary>
        /// Starts the cooldown and makes the skill inactive.
        /// </summary>
        private void StartSkillCD()
        {
            SkillCurrCD = 0.0f;
            IsRecharged = false;

            if (!IsDefault) //If the skill is default one, we do not want to deactivate it when it is used.
            {
                IsActive = false;
            }
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

        public void ActivateDefault()
        {
            IsActive = true;
        }

        public void DeactivateDefault()
        {
            DeactivateSkill();
        }
    }
}