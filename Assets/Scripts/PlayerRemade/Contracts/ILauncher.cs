using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    /// <summary>
    /// Interface for projectile launchers. Provides three methods,
    /// each for it's own skill.
    /// </summary>
    public interface ILauncher : ILauncherExtensionEntitySpeed, IGetTransform
    {
        /*void LaunchBaseSkill(ISkill skill, Teams owner);
        void LaunchFirstSkill(ISkill skill, Teams owner);
        void LaunchSecondSkill(ISkill skill, Teams owner);
        void LaunchThirdSkill(ISkill skill, Teams owner);*/
        /// <summary>
        /// Uses the passed skill.
        /// </summary>
        /// <param name="skill">Skill to launch.</param>
        /// <param name="owner">Owner of the skill.</param>
        void LaunchSkill(ISkill skill, Teams owner);
        /// <summary>
        /// Updates the position of the mouse.
        /// </summary>
        /// <param name="currentMousePos"></param>
        void UpdateMousePosition(Vector2 currentMousePos);
        /// <summary>
        /// Checks if given skill has been activated (key pressed for that skill).
        /// </summary>
        /// <param name="skillType">Skill which will be checked.</param>
        /// <returns>Is the key assigned to the skill pressed?</returns>
        bool ChkSkillActivationInput(SkillType skillType);
        /// <summary>
        /// Is the character using currently selected skill?
        /// </summary>
        /// <returns>True if character is using the selected skill. FALSE otherwise.</returns>
        bool IsShooting();
    }
}
