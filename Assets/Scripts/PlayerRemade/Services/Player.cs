using System;
using System.Collections.Generic;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Characters;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services.Characters;
using Assets.Scripts.PlayerRemade.Services.Skills;
using Assets.Scripts.PlayerRemade.View;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
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
        private Crosshair _crosshair;
        [SerializeField]
        private CameraWorks _cameraWorks;
        /// <summary>
        /// Reference to selected character factory that will be used to setup the character
        /// for the player.
        /// </summary>
        [SerializeField]
        private ICharacterFactory charFactory;

        [SerializeField] private GameObject _factorySelectorPrefab;
        private ISkillFactorySelector _skillFactorySelector;
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
            _launcher = _myCharacter.CharacterInstance.GetComponentInChildren<ILauncher>();
            //...same with the crosshair + setup the camera script.
            _crosshair = _myCharacter.CharacterInstance.GetComponentInChildren<Crosshair>();
            _crosshair.Begin(_cameraWorks);
            _controller2D = _myCharacter.CharacterInstance.GetComponentInChildren<IController2D>();

            _skillManager.AddObserver(_crosshair);
            //Create the skills factory. Then retrieve the factory script and proper skill factory.
            GameObject factorySelector = Instantiate(_factorySelectorPrefab);
            factorySelector.transform.parent = this.gameObject.transform;
            _skillFactorySelector = factorySelector.GetComponent<ISkillFactorySelector>();
            ISkillFactory skillFactory = _skillFactorySelector.SelectFactory(AvailableCharacters.Ruyo);
            //Using the provided skill factory, generate the skills for the character.
            IGetTransform playerTransform = this;
            IGetTransform shotSpawnerTransform = _launcher;
            //Create and assign the skills to the skills manager and save them temporarily for the
            //skillBarManager.
            IDictionary<SkillType, Sprite> skillsIcons = new Dictionary<SkillType, Sprite>();
                //Basic skill:
            ISkill newSkill = skillFactory.CreateSkill(SkillType.Basic, ref shotSpawnerTransform, ref playerTransform);
            newSkill.GameObject.transform.parent = this.gameObject.transform;
            _skillManager.AddSkill(SkillType.Basic, newSkill);
            //First active skill:
            newSkill = skillFactory.CreateSkill(SkillType.First, ref shotSpawnerTransform, ref playerTransform);
            newSkill.GameObject.transform.parent = this.gameObject.transform;
            _skillManager.AddSkill(SkillType.First, newSkill);
            skillsIcons.Add(SkillType.First, newSkill.icon);
            //Second active skill:
            newSkill = skillFactory.CreateSkill(SkillType.Second, ref shotSpawnerTransform, ref playerTransform);
            newSkill.GameObject.transform.parent = this.gameObject.transform;
            _skillManager.AddSkill(SkillType.Second,newSkill);
            skillsIcons.Add(SkillType.Second, newSkill.icon);
            //Third active skill:
            newSkill = skillFactory.CreateSkill(SkillType.Third, ref shotSpawnerTransform, ref playerTransform);
            newSkill.GameObject.transform.parent = this.gameObject.transform;
            _skillManager.AddSkill(SkillType.Third, newSkill);
            skillsIcons.Add(SkillType.Third, newSkill.icon);

            //Initialize the skillBarManager skill icons.
            SkillsBarManager skillBarManager = _myCharacter.CharacterInstance.GetComponentInChildren<SkillsBarManager>();
            skillBarManager.InitializeSkillsIcons(skillsIcons);
            _skillManager.AddObserver(skillBarManager);

        }
        ///<summary>Called once per frame.</summary>
        //Check movement inputs             MovePlayer
        //Check collisions (Contorller2D)   MovePlayer
        //PrepareMove player                MovePlayer
        //Check skills inputs               Launcher
        //Update skillManager               _skillManager
        //Action on launcher, if necessary  UseCurrentSkill
        //Actuate debuffs                   TODO
        //Update anims                      UpdateAnimations
        public void Update()
        {
            float lastFrameTime = Time.deltaTime;

            //_controller2D.RotateClimbingCharacter(_currentVelocity);
            MovePlayer(lastFrameTime);

            ChkSkillsActivationChange();
            _skillManager.UpdateSkillsState(lastFrameTime);
            
            _launcher.UpdateMousePosition(_crosshair.CrosshairPosition);

            if (_launcher.IsShooting())
            {
                UseCurrentSkill();
            }

            _myCharacter.UpdateAnimations(_currentVelocity, _crosshair.CrosshairPosition, 
                new Vector2(_myCharacter.MoveSpeed, _myCharacter.JumpVelocity), transform);
        }
        public bool ChkHit(IProjectile projectile)
        {
            //TODO you idiot
            if (projectile.Alignment == Teams.Enemy)
            {
                this._myCharacter.ReceiveHit(projectile);
                return true;
            }

            return false;
        }
        /// <summary>
        /// Checks one single skill (if it was activated). If so, calls the _skillManager
        /// to, if possible, change the activated skill.
        /// </summary>
        /// <param name="checkedSkillType">Type of currently checked skill.</param>
        private void ChkSingleSkillActivation(SkillType checkedSkillType)
        {
            if (_launcher.ChkSkillActivationInput(checkedSkillType))
            {
                _skillManager.SelectSkill(checkedSkillType);
            }
        }
        /// <summary>
        /// Check if user activated/deactivated any of the active skills.
        /// </summary>
        private void ChkSkillsActivationChange()
        {
            //Check first skill
            SkillType checkedSkillType = SkillType.First;
            ChkSingleSkillActivation(checkedSkillType);

            //Check second skill
            checkedSkillType = SkillType.Second;
            ChkSingleSkillActivation(checkedSkillType);

            //Check third skill
            checkedSkillType = SkillType.Third;
            ChkSingleSkillActivation(checkedSkillType);
        }
        /// <summary>
        /// Ensures movement of the player. Calls IController2D to check the collisions and input.
        /// Checks raw input.
        /// </summary>
        /// <param name="lastFrameTime">The time it took to calculate through last frame.</param>
        private void MovePlayer(float lastFrameTime)
        {
            Vector2 input = RuyoInputReceiver.GetInstance().RetrieveInput();
            float desiredSpeed = input.x * _myCharacter.MoveSpeed;
            

            _currentVelocity.x = Mathf.SmoothDamp(_currentVelocity.x, desiredSpeed, ref _storeVelocitySmoothing,
                _controller2D.Collisions.below? _myCharacter.AccelTimeGrounded : _myCharacter.AccelTimeAirborne);
            

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

        public ICharacter GetCharacter()
        {
            return _myCharacter;
        }

        #endregion


    }
}

//TODO assign scripts to projectile prefabs. Create Skills prefabs and assign skills to them.
//TODO Find Waldo.