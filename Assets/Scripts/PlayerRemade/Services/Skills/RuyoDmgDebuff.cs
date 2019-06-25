using System;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Services.Skills
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    class RuyoDmgDebuff : IDebuff
    {
        public void PerformTick(float lastFrameTime)
        {
            throw new NotImplementedException();
        }

        public bool IsActive { get; private set; }
        public DebuffType Type { get; private set; }
        public bool IsReadyForPayload { get; set; }

        public void Payload(IDebuffableEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
//TODO