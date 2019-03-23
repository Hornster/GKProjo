using Assets.Scripts.PlayerRemade.Contracts.Skills;

namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Manager of negative effects that can be acquired by given entity.
    /// </summary>
    interface IDebuffManager
    {
        /// <summary>
        /// Performs tick of the debuffs accordingly with passed time.
        /// </summary>
        /// <param name="lastFrameTime">Time of the last frame.</param>
        /// <param name="entity">Entity which can be affected by debuffs payloads</param>
        void Update(float lastFrameTime, IDebuffableEntity entity);
        /// <summary>
        /// Tries to add debuff from the projectile. Projectile can have the debuff field
        /// as null.
        /// </summary>
        /// <param name="projectile">Projectile which hit the owner.</param>
        void AddDebuff(IProjectile projectile);
    }
}
