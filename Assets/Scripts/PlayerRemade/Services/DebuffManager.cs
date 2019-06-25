using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public class DebuffManager : IDebuffManager
    {
        #region Members
        private IDictionary<DebuffType, IDebuff> activeDebuffs = new Dictionary<DebuffType, IDebuff>();
        #endregion

        #region Functionalities
        public void Update(float lastFrameTime, IDebuffableEntity entity)
        {
            ChkActiveDebuffs(lastFrameTime);
            Payload(entity);
        }

        public void AddDebuff(IProjectile projectile)
        {
            if (projectile.AssignedDebuff == null)
            {
                return;
            }

            IDebuff debuff = projectile.AssignedDebuff;
            if (!activeDebuffs.ContainsKey(debuff.Type)) //Debuffs do not stack.
            {
                activeDebuffs.Add(debuff.Type, debuff);
            }
            else
            {
                TrySetReadyForPayload(debuff.Type);
            }
        }
        /// <summary>
        /// Performs time update and checks whether are there debuffs that should be deactivated and deleted.
        /// </summary>
        private void ChkActiveDebuffs(float lastFrameTime)
        {
            foreach (var debuff in activeDebuffs)
            {
                debuff.Value.PerformTick(lastFrameTime);
                if (!debuff.Value.IsActive)
                {
                    activeDebuffs.Remove(debuff.Key);
                }
            }
        }
        /// <summary>
        /// Checks special cases of debuffs - the ones that depend on some custom condition, like
        /// the debuff must be already applied when we try to apply it again.
        /// </summary>
        /// <param name="debuffType"></param>
        private void TrySetReadyForPayload(DebuffType debuffType)
        {
            IDebuff debuff;
            if (activeDebuffs.TryGetValue(debuffType, out debuff))
            {
                
                switch (debuffType)
                {
                    case DebuffType.RuyoDamageBoost:
                        if (!debuff.IsReadyForPayload)
                        {
                            debuff.IsReadyForPayload = true;
                        }

                        break;
                }
            }
        }
        /// <summary>
        /// Activates ready debuffs, making them activate their effect.
        /// </summary>
        /// <param name="entity">Entity that will be affected by the debuffs.</param>
        private void Payload(IDebuffableEntity entity)
        {
            foreach (var debuff in activeDebuffs.Values)
            {
                if (debuff.IsReadyForPayload)
                {
                    debuff.Payload(entity);
                }
            }
        }
        #endregion
    }
}
