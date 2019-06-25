using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services.Projectiles;
using UnityEngine;
using ISkill = Assets.Scripts.PlayerRemade.Contracts.Skills.ISkill;

//All factories can be thrown into one gameobject


namespace Assets.Scripts.PlayerRemade.Services.Skills
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    class SkillFactoryRuyo : MonoBehaviour, ISkillFactory
    {
        //additional crosshairs - if there's a special type of crosshair fofr given skill, it's
        //reference will be different from null
        public Sprite Skill1Crosshair;
        public Sprite Skill2Crosshair;
        public Sprite Skill3Crosshair;
        
        [SerializeField]
        private GameObject _skillBaseProjectile;
        [SerializeField]
        private GameObject _skill1Projectile;
        [SerializeField]
        private GameObject _skill2Projectile;


        [SerializeField]
        private GameObject _skillBaseGameObject;
        [SerializeField]
        private GameObject _skill1GameObject;
        [SerializeField]
        private GameObject _skill2GameObject;
        [SerializeField]
        private GameObject _skill3GameObject;

        //Cooldowns for the skills
        private const float basicSkillCD = 0.25f;
        private const float Skill1CD = 3.0f;
        private const float Skill2CD = 5.0f;
        private const float Skill3CD = 10.0f;
        private const float PassiveCD = 5.0f;
        //lifetimes, numbers of lightnings for the second skill
        private const float damageFactor = 2.0f;
        private const float basicProjectileDmg = 3f;
        private const float Skill1ProjectileDmg = basicProjectileDmg* damageFactor;
        private const float TeleportationRange = 10;
        
        public ISkill CreateSkill(SkillType skillID, ref IGetTransform shotSpawner, ref IGetTransform skillOwnerTransform)
        {
            ISkill createdSkill = null;
            switch (skillID)
            {
                case SkillType.Basic:
                    createdSkill = CreateBasicSkill(ref shotSpawner);
                    break;
                case SkillType.First:
                    createdSkill = CreateSkillOne(ref shotSpawner);
                    break;
                case SkillType.Second:
                    createdSkill = CreateSkillTwo();
                    break;
                case SkillType.Third:
                    createdSkill = CreateSkillThree(ref skillOwnerTransform);
                    break;
                case SkillType.Passive:
                    createdSkill = CreateSkillPassive();
                    break;
            }

            return createdSkill;
        }
        /// <summary>
        /// Creates and initializes the basic skill for Ruyo.
        /// </summary>
        /// <returns>Created skill.</returns>
        private ISkill CreateBasicSkill(ref IGetTransform shotSpawner)
        {
            GameObject projectile = Instantiate(_skillBaseGameObject);
            RuyoSkillBase newSkill = projectile.GetComponentInChildren<RuyoSkillBase>();

            newSkill.IsActive = false;
            newSkill.SkillMaxCD = basicSkillCD;
            newSkill.SkillValue = basicProjectileDmg;
            newSkill.SkillCurrCD = basicSkillCD;
            newSkill.SkillCrosshair = Skill1Crosshair;
            newSkill.Projectile = _skillBaseProjectile;
            newSkill.skillType = SkillType.Basic;
            newSkill.shotSpawner = shotSpawner;
            newSkill.GameObject = projectile;

            return newSkill;
        }
        /// <summary>
        /// Creates and initializes the first skill for Ruyo. It works similar to the base skill, so we will use same base class.
        /// </summary>
        /// <returns>Created skill.</returns>
        private ISkill CreateSkillOne(ref IGetTransform shotSpawner)
        {
            GameObject skillInstance = Instantiate(_skill1GameObject);
            RuyoSkillBase newSkill = skillInstance.GetComponentInChildren<RuyoSkillBase>();

            newSkill.IsActive = false;
            newSkill.SkillMaxCD  = Skill1CD;
            newSkill.SkillValue = Skill1ProjectileDmg;
            newSkill.SkillCurrCD = Skill1CD;
            newSkill.SkillCrosshair = Skill1Crosshair;
            newSkill.Projectile = _skill1Projectile;  
            newSkill.skillType = SkillType.First;
            newSkill.shotSpawner = shotSpawner;
            newSkill.GameObject = skillInstance;

            return newSkill;
        }
        /// <summary>
        /// Creates and initializes the second skill for Ruyo.
        /// </summary>
        /// <returns>Created skill.</returns>
        private ISkill CreateSkillTwo()
        {
            GameObject skillInstance = Instantiate(_skill2GameObject);
            RuyoSkillTwo newSkill = skillInstance.GetComponentInChildren<RuyoSkillTwo>();

            newSkill.IsActive = false;
            newSkill.SkillMaxCD = Skill2CD;
            newSkill.SkillCurrCD = Skill2CD;
            newSkill.SkillCrosshair = Skill2Crosshair;
            newSkill.Projectile = _skill2Projectile;
            newSkill.CrosshairBounds = Skill2Crosshair.bounds;
            newSkill.skillType = SkillType.Second;
            newSkill.GameObject = skillInstance;

            RuyoProjectile2Data newSkillData = newSkill.Projectile.GetComponentInChildren<RuyoProjectile2Data>();
            RuyoProjectile2Anim newSkillAnim = newSkill.Projectile.GetComponentInChildren<RuyoProjectile2Anim>();

            newSkillData.Initialize();
            newSkillAnim.Initialize();

            return newSkill;
        }
        /// <summary>
        /// Creates and initializes the third skill for Ruyo.
        /// </summary>
        /// <returns>Created skill.</returns>
        private ISkill CreateSkillThree(ref IGetTransform skillOwnerTransform)
        {
            GameObject skillInstance = Instantiate(_skill3GameObject);
            RuyoSkillThree newSkill = skillInstance.GetComponentInChildren<RuyoSkillThree>();

            newSkill.IsActive = false;
            newSkill.SkillMaxCD = Skill3CD;
            newSkill.SkillLifeTime = 0.0f;  //teleportation is instant
            newSkill.SkillCurrCD = Skill3CD;
            newSkill.SkillCrosshair = Skill3Crosshair;
            newSkill.Projectile = null;   //No projectile for teleportation
            newSkill.PlayerTransform = skillOwnerTransform;
            newSkill.skillType = SkillType.Third;
            newSkill.TeleportRange = TeleportationRange;
            newSkill.GameObject = skillInstance;

            return newSkill;
        }
        /// <summary>
        /// For Ruyo, passive skill directly connected with debuff.
        /// </summary>
        /// <returns>Created skill.</returns>
        private ISkill CreateSkillPassive()
        {
            return null;    //Ruyo's passive skill is connected with his projectiles.
        }

        
    }
}

