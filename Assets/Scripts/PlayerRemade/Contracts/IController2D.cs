using Assets.Scripts.PlayerRemade.Model;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    /// <summary>
    /// Allows for processing player's input.
    /// </summary>
    interface IController2D : IObservable<IAnimationData>
    {
        /// <summary>
        /// Stores information about collision status.
        /// </summary>
        CollisionInfo Collisions { get; }
        /// <summary>
        /// Performs checks for special movement keys, resolves collisions and returns
        /// modified velocity vector.
        /// </summary>
        /// <param name="velocity">Current velocity of the character, not affected by time. Will be affected by gravity*lastFrameTime.</param>
        /// <param name="rawInput">Input read from the controls.</param>
        /// <param name="currJumpVelocity">Y velocity of regular jump.</param>
        /// <param name="currDoubleJumpFactor">Factor scaling the jump strength for mid-air jump.</param>
        /// <param name="currClimbMoveSpeed">Current climbing speed of the character.</param>
        /// <param name="currGravity">Current gravity the character is affected by.</param>
        /// <param name="lastFrameTime">The time it took to perform last frame. Used to scale velocity.</param>
        /// <returns>Velocity vector scaled by provided lastFrameTime and gravity.</returns>
        Vector3 PrepareMove(ref Vector3 velocity, Vector2 rawInput, float currJumpVelocity, 
            float currDoubleJumpFactor, float currClimbMoveSpeed, float currGravity, float lastFrameTime);
        
    }
}
