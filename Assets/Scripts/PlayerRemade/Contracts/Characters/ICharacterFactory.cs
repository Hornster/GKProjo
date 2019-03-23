using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Contracts.Characters
{
    interface ICharacterFactory
    {
        Vector3 CharacterLocalScale { get; set; }
        /// <summary>
        /// Creates character of given ID.
        /// </summary>
        /// <param name="charID">ID of the character.</param>
        /// analyzes and resolves physics collisions. Used to read state from by the animation
        /// supervisor.</param>
        /// <returns>New character script.</returns>
        ICharacter CreateCharacter(Enums.AvailableCharacters charID);
    }
}

