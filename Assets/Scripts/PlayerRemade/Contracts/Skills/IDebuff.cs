using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Contracts.Skills
{
    public interface IDebuff
    {
        /// <summary>
        /// Called to make the debuff's effects work for current frame.
        /// </summary>
        /// <param name="lastFrameTime">Time of the last game frame.</param>
        void PerformTick(float lastFrameTime);
        /// <summary>
        /// Indicates whether is this debuff still working. If not - set to FALSE.
        /// </summary>
        bool IsActive { get; }
        /// <summary>
        /// Type of the debuff.
        /// </summary>
        DebuffType Type { get; }
        /// <summary>
        /// Determines whether is it the time for this debuff to affect its host.
        /// </summary>
        bool IsReadyForPayload { get; set; }
        /// <summary>
        /// Performs debuff action assigned with this debuff, if it is ready to do so
        /// (IsReadyForPayLoad set to true)
        /// </summary>
        /// <param name="entity">Entity affected by the payload</param>
        void Payload(IDebuffableEntity entity);
    }
}
