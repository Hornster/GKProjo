using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Used as extension for launchers. Adds method that allows for
    /// updating the speed of object which the launcher is assigned to.
    /// </summary>
    public interface ILauncherExtensionEntitySpeed
    {
        /// <summary>
        /// Updates the velocity of the entity.
        /// </summary>
        /// <param name="velocity">New velocity.</param>
        void SetEntityVelocity(Vector2 velocity);
    }
}
