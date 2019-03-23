namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Determines objects that can be hit.
    /// </summary>
    public interface IHittable
    {
        /// <summary>
        /// Was the object successfully hit with a projectile?
        /// </summary>
        /// <param name="projectile">Projectile data</param>
        /// <returns>TRUE if entity was hit successfully. FALSE otherwise/</returns>
        bool ChkHit(IProjectile projectile);
    }
}
