using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    interface IProjectileMover
    {
        void Initialize();
        void SetMoveDirection(Vector2 direction, Vector2 playerVelocity);
    }
}

