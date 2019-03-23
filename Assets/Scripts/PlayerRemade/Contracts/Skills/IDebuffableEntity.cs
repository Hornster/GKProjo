
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Contracts.Skills
{
    /// <summary>
    /// Describes all forms of entities that can be affected by debuffs.
    /// Used to inflict changes to the entities.
    /// </summary>
    public interface IDebuffableEntity : IHittable
    {
        /// <summary>
        /// Scales the velocity of the entity accordingly to passed factor (multiplication)
        /// </summary>
        /// <param name="factor"></param>
        void ModifyVelocity(Vector2 factor);
    }
}
