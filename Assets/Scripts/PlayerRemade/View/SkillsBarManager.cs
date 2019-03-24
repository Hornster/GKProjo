
using System;
using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.PlayerRemade.View
{
    public class SkillsBarManager : MonoBehaviour, Contracts.IObserver<SkillsState>
    {
        #region Members
        //stores a reference to the background graphic of the skills bar
        [SerializeField]
        private Image skillBckGndImg;
        //stores a reference to the image of the first skill
        [SerializeField]
        private Image skill1Img;
        //stores a reference to the image of the second skill
        [SerializeField]
        private Image skill2Img;
        //stores a reference to the image of the third skill
        [SerializeField]
        private Image skill3Img;
        #endregion Members

        #region Functionalities
        ///<summary>Modifies the icon of a skill accordingly to current cooldown value.</summary>
        ///<param name="skillsState" >The current values of cooldowns and information about currently selected skill.</param>
        private void SetSkillIconOnCD(SkillsState skillsState)
        {
            foreach (var skillData in skillsState.skillsCooldowns)
            {
                Tuple<float, float> skillCdData = skillData.Value;
                float skillCurrentCD = skillCdData.Item1 / skillCdData.Item2;

                switch (skillData.Key)//No need to check the rest of values.
                {
                    case SkillType.First:
                        skill1Img.fillAmount = skillCurrentCD;
                        break;
                    case SkillType.Second:
                        skill2Img.fillAmount = skillCurrentCD;
                        break;
                    case SkillType.Third:
                        skill3Img.fillAmount = skillCurrentCD;
                        break;
                }
            }
        }
        /// <summary>
        /// Resets the color of all skills to white, making all of the skills seem to be not selected.
        /// </summary>
        private void ResetSkillsMarks()
        {
            skill1Img.color = Color.white;
            skill2Img.color = Color.white;
            skill3Img.color = Color.white;
        }
        /// <summary>
        /// Clearly marks currently activated skill from the bar.
        /// </summary>
        /// <param name="skillType"></param>
        private void MarkActiveSkill(SkillType skillType)
        {
            ResetSkillsMarks();
            switch (skillType)//No need to check the rest of values.
            {
                case SkillType.First:
                    skill1Img.color += Color.yellow;
                    break;
                case SkillType.Second:
                    skill2Img.color += Color.yellow;
                    break;
                case SkillType.Third:
                    skill3Img.color += Color.yellow;
                    break;
            }
        }
        /// <summary>
        /// Called from outside, by observables usually - updates the visual state of icons that represent skills on the skillbar.
        /// </summary>
        /// <param name="observedObject"></param>
        public void Notify(SkillsState observedObject)
        {
            SetSkillIconOnCD(observedObject);
            MarkActiveSkill(observedObject.currentlyActiveSkill);
        }
        /// <summary>
        /// Used to setup the icons for the skills on the skillbar.
        /// </summary>
        /// <param name="skillIcons">A dictionary containing pairs of SkillType and Sprite for this skill type.</param>
        public void InitializeSkillsIcons(IDictionary<SkillType, Sprite> skillIcons)
        {
            foreach (var skill in skillIcons)
            {
                switch (skill.Key)
                {
                    case SkillType.First:
                        skill1Img.sprite = skill.Value;
                        break;
                    case SkillType.Second:
                        skill2Img.sprite = skill.Value;
                        break;
                    case SkillType.Third:
                        skill3Img.sprite = skill.Value;
                        break;
                }
            }
        }

        public void InitializeSkillbarBg(Sprite bg)
        {
            skillBckGndImg.sprite = bg;
        }
        #endregion Functionalities
    }
}
