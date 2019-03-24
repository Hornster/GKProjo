using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Model
{
    public class SkillsState
    {
        public SkillType currentlyActiveSkill { get; set; }
        /// <summary>
        /// Contains information about skills cooldowns. Data format:
        /// Key: SkillType, Value:[currentCooldown, maxCooldown]
        /// </summary>
        public IDictionary<SkillType, Tuple<float, float>> skillsCooldowns = new Dictionary<SkillType, Tuple<float, float>>();
        /// <summary>
        /// Adds skill cooldown data.
        /// </summary>
        /// <param name="skillType">Type of the skill.</param>
        /// <param name="currentCooldown">Current cooldown value.</param>
        /// <param name="maxCooldown">The tijme it takes for the skill to fully recharge.</param>
        public void AddSkillCooldown(SkillType skillType, float currentCooldown, float maxCooldown)
        {
            skillsCooldowns.Add(skillType, Tuple.Create(currentCooldown, maxCooldown));
        }
    }
}
