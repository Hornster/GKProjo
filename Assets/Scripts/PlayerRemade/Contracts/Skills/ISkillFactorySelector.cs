namespace Assets.Scripts.PlayerRemade.Contracts.Skills
{
    /// <summary>
    /// Selects proper skill factory.
    /// </summary>
    public interface ISkillFactorySelector
    {
        /// <summary>
        /// Selects skill factory basing on character type.
        /// </summary>
        /// <param name="availableCharacter">Character which skill set factory will be returned.</param>
        /// <returns>Skill factory for the character.</returns>
        ISkillFactory SelectFactory(Enums.AvailableCharacters availableCharacter);
    }
}
