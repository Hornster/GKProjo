﻿using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Enables and disables skills, switches the crosshair graphics when skill is switched.
    /// Remember to use AddObserver and to add skills.
    /// </summary>
    public class SkillManager : IObservable<Sprite>
    {
        #region Members
        private IDictionary<SkillType, ISkill> _skills = new Dictionary<SkillType, ISkill>();
        private ISkill _currentlyActiveSkill;
        private IObserver<Sprite> _crosshair;

        
        public bool IsCurrentSkillReady
        {
            get { return _currentlyActiveSkill.IsRecharged; }
        }
        #endregion
        

        #region Functionalities
        /// <summary>
        /// Adds new skill to the skill manager. If skill is already present at passed key - does nothing.
        /// Works as deselection of other skills.
        /// </summary>
        /// <param name="skillType">Type of the skill.</param>
        /// <param name="skill">The skill object itself.</param>
        public void AddSkill(SkillType skillType, ISkill skill)
        {
            if (!_skills.ContainsKey(skillType))
            {
                _skills.Add(skillType, skill);
            }
        }
        /// <summary>
        /// Resets the crosshair and the activated skill to base one.
        /// </summary>
        public void ResetToBaseSkill()
        {
            SwapActiveSkill(_skills[SkillType.Basic]);
        }
        
        #endregion
        /// <summary>
        /// Adds a crosshair object as observer. Its sprite will be changed accordingly to
        /// selected skill.
        /// </summary>
        /// <param name="observer">Crosshair that will be affected by change of  selected skill.</param>
        public void AddObserver(IObserver<Sprite> observer)
        {
            _crosshair = observer;
        }

        /// <summary>
        /// Selects given skill (if its cooldown is done). If basic skill is to be selected -
        /// doesn't care about cooldown. Triggers change of crosshair if needed.
        /// </summary>
        /// <param name="skillType"></param>
        public void SelectSkill(SkillType skillType)
        {
            ISkill selectedSkill = _skills[skillType];

            if (skillType != SkillType.Basic)
            {
                if (!selectedSkill.IsRecharged)
                {
                    return;//If the skill is not base one andis not rechared - do not switch it
                }
            }

            SwapActiveSkill(selectedSkill);
        }
        /// <summary>
        /// Gets the currently selected skill, then deactivates it and switches to
        /// basic one. Changes the crosshair, too.
        /// </summary>
        /// <returns>Currently selected skill</returns>
        public ISkill UseSkill()
        {
            ISkill usedSkill = _currentlyActiveSkill;
            
            _currentlyActiveSkill = null;
            ResetToBaseSkill();

            return usedSkill;
        }
        /// <summary>
        /// Swaps the _currentlyActiveSkill with provided one.
        /// </summary>
        /// <param name="newSkill">New skill to be selected.</param>
        private void SwapActiveSkill(ISkill newSkill)
        {
            _currentlyActiveSkill.DeactivateSkill();
            _currentlyActiveSkill = newSkill;
            _currentlyActiveSkill.ActivateSkill();

            _crosshair.Notify(_currentlyActiveSkill.SkillCrosshair);
        }
    }
}
