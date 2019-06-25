using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    // public class RuyoProjectile2Data : ProjectileData {
    /// <summary>
    /// Data for the projectile of the second skill.
    /// The Initialize() method has to be called before any meanings of reading the data from this class.
    /// </summary>
    //
    public class RuyoProjectile2Data : MonoBehaviour, IProjectile
    {
        [field: SerializeField]
        public float Damage { get; set; }
        [field: SerializeField]
        public Teams Alignment { get; set; }
        [field: SerializeField]
        public bool HasItsOwnLifeTimer { get; set; }
        [field: SerializeField]
        public bool IsTimed { get; set; }
        [field: SerializeField]
        public float SkillDuration { get; set; }
        [field: SerializeField]
        public bool CanPenetrate { get; set; }
        [field: SerializeField]
        public int ClustersAmount { get; set; }
        public IDebuff AssignedDebuff { get; set; }

        [HideInInspector]
        public float ClusterOffset { get; set; }
        [HideInInspector]
        public float ClusterLastingTime { get; set; }

        public void Initialize()
        {
            Transform AOESprite = GetComponent<Transform>();
        
            //ClustersAmount = 8;
            ClusterOffset = GetComponent<SpriteRenderer>().sprite.bounds.size.x / ClustersAmount ;
            //SkillDuration = 0.5f;
            //HasItsOwnLifeTimer = true;
            ClusterLastingTime = SkillDuration / (ClustersAmount / 2.0f);
            //effectType = Data.effects.RUYODMGBOOST;
        }
    }
}
//TODO