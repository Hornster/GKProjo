namespace Assets.Scripts.PlayerRemade.Contracts.Skills
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    /// <summary>
    /// Defines skill that will be always active when others are not.
    /// </summary>
    interface IDefaultSkill : ISkill
    {
        /// <summary>
        /// Determines whether is this skill the default skill.
        /// </summary>
        bool IsDefault { get; }
        /// <summary>
        /// Activates default skill. This differs form ISkill version by that it doesn't check
        /// whether is the skill recharged already and, usually, allows for switching the crosshair
        /// and other stuff before the skill is even recharged (unlike ISkill version).
        /// </summary>
        void ActivateDefault();
        /// <summary>
        /// Deactivates the default skill.
        /// </summary>
        void DeactivateDefault();
    }
}
