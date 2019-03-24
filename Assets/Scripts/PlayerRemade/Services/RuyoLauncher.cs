using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services.Characters;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services
{
    /// <summary>
    /// Projectile launcher for Ruyo playable character.
    /// </summary>
    public class RuyoLauncher : MonoBehaviour, ILauncher
    {
        #region Members

        public Transform ShotSpawner;
        //unit vector that stores the direction in which the projectiles should be fired
        private Vector2 _directionVector = new Vector2();
        /// <summary>
        /// Length of the direction vector. Used to normalize the direction vector (normal skills)
        /// and to calculate position of mouse click (AoE, teleportation skills)
        /// </summary>
        private float _dirVectLength;

        private Vector2 _playerVelocity = new Vector2();

        #endregion

        #region Ctors
        public RuyoLauncher(Transform shotSpawnerPos)
        {
            this.ShotSpawner = shotSpawnerPos;
        }
        #endregion

        #region Functionalities
        public void LaunchSkill(ISkill skill, Teams owner)
        {
            switch (skill.skillType)
            {
                case SkillType.Basic:
                    LaunchBaseSkill(skill, owner);
                    break;
                case SkillType.First:
                    LaunchFirstSkill(skill, owner);
                    break;
                case SkillType.Second:
                    LaunchSecondSkill(skill, owner);
                    break;
                case SkillType.Third:
                    LaunchThirdSkill(skill, owner);
                    break;
            }
        }

        private void LaunchBaseSkill(ISkill skill, Teams owner)
        {
            skill.UseSkill(_directionVector, owner, _playerVelocity);
        }

        private void LaunchFirstSkill(ISkill skill, Teams owner)
        {
            skill.UseSkill(_directionVector, owner, _playerVelocity);
        }

        private void LaunchSecondSkill(ISkill skill, Teams owner)
        {
            _directionVector *= _dirVectLength;
            _directionVector.x += transform.position.x;
            _directionVector.y += transform.position.y;
            
            
            skill.UseSkill(_directionVector, owner, _playerVelocity);
        }

        private void LaunchThirdSkill(ISkill skill, Teams owner)
        {
            skill.UseSkill(_directionVector, owner, _playerVelocity);
        }


        //calculates new direction vector when the mouse moves
        

        public void UpdateMousePosition(Vector2 currentMousePos)
        {
            Vector2 tempDirection = new Vector2(ShotSpawner.position.x, ShotSpawner.position.y);
            _directionVector = currentMousePos - tempDirection;
            _dirVectLength = _directionVector.magnitude;
            _directionVector /= _dirVectLength;
        }
        /// If skill not recognized - returns FALSE.
        public bool ChkSkillActivationInput(SkillType skillType)
        {
            switch (skillType)
            {
                case SkillType.First:
                    return RuyoInputReceiver.GetInstance().GetKeyDown(RuyoInputReceiver.FirstSkillKey);
                case SkillType.Second:
                    return RuyoInputReceiver.GetInstance().GetKeyDown(RuyoInputReceiver.SecondSkillKey);
                case SkillType.Third:
                    return RuyoInputReceiver.GetInstance().GetKeyDown(RuyoInputReceiver.ThirdSkillKey);

                default:
                    return false;
            }
        }

        public bool IsShooting()
        {
            return RuyoInputReceiver.GetInstance().GetKey(RuyoInputReceiver.LeftMouseButton);
        }

        public void SetEntityVelocity(Vector2 velocity)
        {
            _playerVelocity = velocity;
        }

        public Transform GetTransform()
        {
            return ShotSpawner;
        }
        #endregion


    }
}
//TODO Pass to skills references to ILauncher (for example through another interface) and
//reference to player script, through proper interface that allows for reading of only transform.