using Assets.Scripts.PlayerRemade.Model;

namespace Assets.Scripts.PlayerRemade.Contracts
{
    /// <summary>
    /// Information required for the AnimationSupervisor to work.
    /// All data contained here is necessary for the animations.
    /// </summary>
    public interface IAnimationData
    {
        CollisionInfo Collisions { get; }
        MovementInfo Movement { get; }
    }
}
