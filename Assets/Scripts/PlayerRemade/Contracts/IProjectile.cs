using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    /// <summary>
    /// Interface that can be used by classes that represent projectile data.
    /// </summary>
    public interface IProjectile
    {
        float Damage { get; }
        /// <summary>
        /// Determines who shot the projectile.
        /// </summary>
        Teams Alignment { get; }
        /// <summary>
        /// Can this projectile evaporate from the time-space continuum by itself after given time?
        /// </summary>
        bool HasItsOwnLifeTimer { get; }
        bool IsTimed { get; }
        /// <summary>
        /// If this skill has its own timer - what amount of time does it exist?
        /// </summary>
        float SkillDuration { get; }
        /// <summary>
        /// Can this projectile ignore walls' and players' collision bodies?
        /// </summary>
        bool CanPenetrate { get; }

        /// <summary>
        /// Debuff assigned to the projectile, can be NULL.
        /// </summary>
        IDebuff AssignedDebuff { get; }

    }
}
