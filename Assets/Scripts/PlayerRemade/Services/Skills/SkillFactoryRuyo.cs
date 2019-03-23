using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;
using ISkill = Assets.Scripts.PlayerRemade.Contracts.Skills.ISkill;

//All factories can be thrown into one gameobject


namespace Assets.Scripts.PlayerRemade.Services.Skills
{
    class SkillFactoryRuyo : ISkillFactory
    {
        //additional crosshairs - if there's a special type of crosshair fofr given skill, it's
        //reference will be different from null
        public Sprite Skill1Crosshair;
        public Sprite Skill2Crosshair;
        public Sprite Skill3Crosshair;

        public GameObject Skill1Projectile;
        public GameObject Skill2Projectile;

        //Cooldowns for the skills
        private const float basicSkillCD = 1.0f;
        private const float Skill1CD = 3.0f;
        private const float Skill2CD = 5.0f;
        private const float Skill3CD = 10.0f;
        private const float PassiveCD = 5.0f;
        //lifetimes, numbers of lightnings for the second skill
        private const float projectile2LifeTime = 0.5f;
        private const int lightningNumber = 12;
        private const float basicProjectileLifeTime = 0.25f;
        private const float damageFactor = 2.0f;
        private const float lifeTimeFactor = 1.5f;  //Used to extend the time the first skill projectile lives for
        private const float basicProjectileDmg = 3f;
        private const float Skill1ProjectileDmg = basicProjectileDmg* damageFactor;
        private const float Skill2ProjectileDmg = 8f;
        private const float Skill3Value = 750f;
        
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
            RuyoSkillBase newSkill = new RuyoSkillBase(ref shotSpawner);

            newSkill.IsActive = false;
            newSkill.SkillMaxCD = basicSkillCD;
            newSkill.SkillValue = basicProjectileDmg;
            newSkill.SkillLifeTime = basicProjectileLifeTime;
            newSkill.SkillCurrCD = basicSkillCD;
            newSkill.SkillCrosshair = Skill1Crosshair;
            newSkill.Projectile = Skill1Projectile;
            newSkill.skillType = SkillType.Basic;

            return newSkill;
        }
        /// <summary>
        /// Creates and initializes the first skill for Ruyo. It works similar to the base skill, so we will use same base class.
        /// </summary>
        /// <returns>Created skill.</returns>
        private ISkill CreateSkillOne(ref IGetTransform shotSpawner)
        {
            RuyoSkillBase newSkill = new RuyoSkillBase(ref shotSpawner);

            newSkill.IsActive = false;
            newSkill.SkillMaxCD  = Skill1CD;
            newSkill.SkillValue = Skill1ProjectileDmg;
            newSkill.SkillLifeTime = basicProjectileLifeTime*lifeTimeFactor;
            newSkill.SkillCurrCD = Skill1CD;
            newSkill.SkillCrosshair = Skill1Crosshair;
            newSkill.Projectile = Skill1Projectile;  
            newSkill.skillType = SkillType.First;

            return newSkill;
        }
        /// <summary>
        /// Creates and initializes the second skill for Ruyo.
        /// </summary>
        /// <returns>Created skill.</returns>
        private ISkill CreateSkillTwo()
        {
            RuyoSkillTwo newSkill = new RuyoSkillTwo();

            newSkill.IsActive = false;
            newSkill.SkillMaxCD = Skill2CD;
            newSkill.SkillValue = Skill2ProjectileDmg;
            newSkill.SkillLifeTime = projectile2LifeTime;
            newSkill.SkillCurrCD = Skill2CD;
            newSkill.SkillCrosshair = Skill2Crosshair;
            newSkill.Projectile = Skill2Projectile;
            newSkill.LightningsCount = lightningNumber;
            newSkill.CrosshairBounds = Skill2Crosshair.bounds;
            newSkill.skillType = SkillType.Second;

            return newSkill;
        }
        /// <summary>
        /// Creates and initializes the third skill for Ruyo.
        /// </summary>
        /// <returns>Created skill.</returns>
        private ISkill CreateSkillThree(ref IGetTransform skillOwnerTransform)
        {
            RuyoSkillThree newSkill = new RuyoSkillThree();

            newSkill.IsActive = false;
            newSkill.SkillMaxCD = Skill3CD;
            newSkill.SkillValue = Skill3Value;
            newSkill.SkillLifeTime = 0.0f;  //teleportation is instant
            newSkill.SkillCurrCD = Skill3CD;
            newSkill.SkillCrosshair = Skill3Crosshair;
            newSkill.Projectile = null;   //No projectile for teleportation
            newSkill.PlayerTransform = skillOwnerTransform;
            newSkill.skillType = SkillType.Third;

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

