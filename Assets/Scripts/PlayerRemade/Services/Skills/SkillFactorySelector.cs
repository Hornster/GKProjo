using System;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;

namespace Assets.Scripts.PlayerRemade.Services.Skills
{
    public class SkillFactorySelector : ISkillFactorySelector
    {
        public ISkillFactory SelectFactory(AvailableCharacters availableCharacter)
        {
            ISkillFactory factory = null;
            switch (availableCharacter)
            {
                case AvailableCharacters.Ruyo:
                    factory =  new SkillFactoryRuyo();
                    break;
                default:
                    throw new NotImplementedException("Requested skill factory for passed character (" + availableCharacter.ToString() + ") was not implemented.");
            }

            return factory;
        }
    }
}
