using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Contracts
{
    public interface IProjectile
    {
        float Damage { get; }
        /// <summary>
        /// Determines who shot the projectile.
        /// </summary>
        Teams Alignment { get; }

        bool HasItsOwnLifeTimer { get; set; }
        bool IsTimed { get; set; }
        float SkillDuration { get; set; }
        /// <summary>
        /// Can this projectile ignore walls' and players' collision bodies?
        /// </summary>
        bool CanPenetrate { get; set; }

        /// <summary>
        /// Debuff assigned to the projectile, can be NULL.
        /// </summary>
        IDebuff AssignedDebuff { get; }

    }
}
