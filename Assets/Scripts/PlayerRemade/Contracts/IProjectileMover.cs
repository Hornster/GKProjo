using UnityEngine;

namespace Assets.Scripts
{
    interface IProjectileMover
    {
        void Initialize();
        void SetMoveDirection(Vector2 direction, Vector2 playerVelocity);
    }
}

