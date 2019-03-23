using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Contracts
{
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
        void LaunchSkill(ISkill skill, Teams owner);

        void UpdateMousePosition(Vector2 currentMousePos);
    }
}
