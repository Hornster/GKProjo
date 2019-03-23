using System;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Characters;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services.Characters;
using Assets.Scripts.PlayerRemade.Services.Skills;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Main player script. Contains other components.
    /// </summary>
    public class Player : MonoBehaviour, IHittable, IGetTransform
    {
        #region Members

        private Vector3 _currentVelocity = new Vector3(0,0,0);
        private float _storeVelocitySmoothing;
        float _velocityXSmoothing;
        float _velocityYSmoothing;
        float _jumpVelocity = 8;
        float _gravity;
        /// <summary>
        /// Responsible for physics, detection various types of movement.
        /// </summary>
        [SerializeField]
        private IController2D _controller2D;
        [SerializeField]
        private ILauncher _launcher;
        [SerializeField]
        private ICharacter _myCharacter;
        private IDebuffManager _debuffManager = new DebuffManager();
        private SkillManager _skillManager = new SkillManager();
        [SerializeField]
        private Crosshair _crosshair;
        [SerializeField]
        private CameraWorks _cameraWorks;
        /// <summary>
        /// Reference to selected character factory that will be used to setup the character
        /// for the player.
        /// </summary>
        [SerializeField]
        private ICharacterFactory charFactory;
        #endregion

        #region Functionalities

        public void Start()
        {
            charFactory = GetComponent<PlayableCharacterFactory>();
            //Create the character.
            _myCharacter = charFactory.CreateCharacter(AvailableCharacters.Ruyo);
            //Assign freshly created character as child of this gameobject.
            _myCharacter.CharacterInstance.transform.parent = transform;
            //Make sure that the created character is positioned in the middle of the player gameobject.
            _myCharacter.CharacterInstance.transform.localPosition = Vector3.zero;  
            //Get the launcher from the character and store its script ref...
            _launcher = _myCharacter.CharacterInstance.GetComponent<ILauncher>();
            //...same with the crosshair + setup the camera script.
            _crosshair = _myCharacter.CharacterInstance.GetComponentInChildren<Crosshair>();
            _crosshair.Begin(_cameraWorks);
            _controller2D = _myCharacter.CharacterInstance.GetComponentInChildren<IController2D>();

            _skillManager.AddObserver(_crosshair);
            ISkillFactorySelector factorySelector = new SkillFactorySelector();
            ISkillFactory skillFactory = factorySelector.SelectFactory(AvailableCharacters.Ruyo);

            IGetTransform playerTransform = this;
            IGetTransform shotSpawnerTransform = _launcher;
            skillFactory.CreateSkill(SkillType.Basic, ref shotSpawnerTransform, ref playerTransform);
        }
        ///<summary>Called once per frame.</summary>
        //Check movement inputs             MovePlayer
        //Check collisions (Contorller2D)   MovePlayer
        //PrepareMove player                MovePlayer
        //Check skills inputs
        //Update skillManager
        //Action on launcher, if necessary
        //Actuate debuffs
        //Update anims
        public void Update()
        {
            float lastFrameTime = Time.deltaTime;

            //_controller2D.RotateClimbingCharacter(_currentVelocity);
            MovePlayer(lastFrameTime);

            _myCharacter.UpdateAnimations(_currentVelocity, _crosshair.CrosshairPosition, new Vector2(_myCharacter.MoveSpeed, _myCharacter.JumpVelocity), transform);
        }
        public bool ChkHit(IProjectile projectile)
        {
            throw new NotImplementedException();
        }

        private void MovePlayer(float lastFrameTime)
        {
            Vector2 input = RuyoInputReceiver.GetInstance().RetrieveInput();
            float desiredSpeed = input.x * _myCharacter.MoveSpeed;
            

            _currentVelocity.x = Mathf.SmoothDamp(_currentVelocity.x, desiredSpeed, ref _storeVelocitySmoothing,
                _myCharacter.AccelTimeGrounded);
            

            //Assign modified speed to another vector - we need to preserve the original, unaffected by time speed.
            Vector3 preparedVelocity = _controller2D.PrepareMove(ref _currentVelocity, input, _myCharacter.JumpVelocity,
                _myCharacter.DoubleJumpFactor, _myCharacter.ClimbingMoveSpeed, _myCharacter.Gravity, lastFrameTime);
           
            
            //Move the player...
            transform.Translate(new Vector3(preparedVelocity.x, preparedVelocity.y, 0.0f));
            //...and the camera after the player. 
            _cameraWorks.MoveCamera(transform.position);
        }
        

        /// <summary>
        /// Checks if the skill is ready and if so- sends it to be launched.
        /// </summary>
        private void UseCurrentSkill()
        {
            if (_skillManager.IsCurrentSkillReady)
            {
                _launcher.LaunchSkill(_skillManager.UseSkill(), _myCharacter.team);
            }
        }

        public Transform GetTransform()
        {
            return transform;
        }
        #endregion


    }
}//TODO Modify new fields in character factory. Remember to call the SetGravity method (or whatever is it called)