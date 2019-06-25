using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Projectiles
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    class RuyoProjectileBasicData : MonoBehaviour, IProjectile
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
        public IDebuff AssignedDebuff { get; set; }
    }
}
