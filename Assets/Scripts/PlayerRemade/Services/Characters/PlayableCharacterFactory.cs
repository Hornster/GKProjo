
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Characters;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Characters
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public class PlayableCharacterFactory : MonoBehaviour, ICharacterFactory
    {
        /// <summary>
        /// Link to the prefab of Ruyo playable character.
        /// </summary>
        public GameObject RuyoCharacter;
        [SerializeField]
        private Vector3 _characterLocalScale = Vector3.one;

        public Vector3 CharacterLocalScale
        {
            get
            {
                return _characterLocalScale;

            }
            set
            {
                _characterLocalScale = value;
            }
        }

        public ICharacter CreateCharacter(Enums.AvailableCharacters charID)
        {
            switch (charID)
            {
                case Enums.AvailableCharacters.Ruyo:
                    return MakeRuyo();
            }

            throw new System.NotImplementedException("Requested playable character " + charID.ToString() + " is not yet implemented.");
        }

        private RuyoCharacter MakeRuyo()
        {
            
            var character = Instantiate(RuyoCharacter);
            character.transform.localScale = CharacterLocalScale;
            RuyoCharacter characterScript = new RuyoCharacter();

            characterScript.CharacterInstance = character;
            IObservable<IAnimationData> playerController = character.GetComponentInChildren<IController2D>();
            characterScript.AnimationController = character.GetComponentInChildren<AnimationController>();
            AnimationSupervisor animSupervisor = new AnimationSupervisor(characterScript.AnimationController);
            characterScript.AnimationSupervisor = animSupervisor;
            characterScript.CalcGravity();      //Use defaults

            characterScript.team = Teams.Player;
            playerController.AddObserver(animSupervisor);

            return characterScript;
        }
        
    }
}
