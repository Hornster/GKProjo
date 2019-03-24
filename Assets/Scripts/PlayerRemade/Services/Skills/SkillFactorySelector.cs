using System;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Skills
{
    [RequireComponent(typeof(SkillFactoryRuyo))]
    public class SkillFactorySelector : MonoBehaviour, ISkillFactorySelector
    {
        public ISkillFactory SelectFactory(AvailableCharacters availableCharacter)
        {
            ISkillFactory factory = null;
            switch (availableCharacter)
            {
                case AvailableCharacters.Ruyo:
                    factory =  GetComponent<SkillFactoryRuyo>();
                    break;
                default:
                    throw new NotImplementedException("Requested skill factory for passed character (" 
                                                      + availableCharacter.ToString() 
                                                      + ") was not implemented. If you think it was, however, then add the script to SkillFactory Selector's gameobject.");
            }

            return factory;
        }
    }
}
