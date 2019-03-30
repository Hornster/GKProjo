
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
        //stores a reference to the border around the skill 1 icon.
        [SerializeField]
        private Image skill1ActivationMarker;
        //stores a reference to the border around the skill 2 icon.
        [SerializeField]
        private Image skill2ActivationMarker;
        //stores a reference to the border around the skill 3 icon.
        [SerializeField]
        private Image skill3ActivationMarker;
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

        public void Start()
        {
            skill1Img.type = Image.Type.Filled;
            skill2Img.type = Image.Type.Filled;
            skill3Img.type = Image.Type.Filled;

            skill1Img.fillMethod = Image.FillMethod.Radial360;
            skill2Img.fillMethod = Image.FillMethod.Radial360;
            skill3Img.fillMethod = Image.FillMethod.Radial360;

            skill1Img.fillClockwise = true;
            skill2Img.fillClockwise = true;
            skill3Img.fillClockwise = true;
        }
        /// <summary>
        /// Resets the color of all skills to white, making all of the skills seem to be not selected.
        /// </summary>
        private void ResetSkillsMarks()
        {
            skill1ActivationMarker.color = Color.clear;
            skill2ActivationMarker.color = Color.clear;
            skill3ActivationMarker.color = Color.clear;
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
                    skill1ActivationMarker.color = Color.yellow;
                    break;
                case SkillType.Second:
                    skill2ActivationMarker.color = Color.yellow;
                    break;
                case SkillType.Third:
                    skill3ActivationMarker.color = Color.yellow;
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
