using System;
using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Characters;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services.Projectiles;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Services.Characters
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    class RuyoCharacter : ICharacter
    {
        #region Members

        public Action CharacterDiedCallback { get; set; }
        public GameObject CharacterInstance { get; set; }
        public Teams team { get; set; }
        public float AccelTimeAirborne { get; set; }
        public float MoveSpeed { get; set; }
        public float AccelTimeGrounded { get; set; }
        public float ClimbingMoveSpeed { get; set; }
        public float CurrentHP { get; set; }
        public float MaxHP { get; set; }
        public float Gravity { get; private set; }
        public float JumpVelocity { get; private set; }
        public float DoubleJumpFactor { get; set; }
        public AnimationController AnimationController { get; set; }
        public AnimationSupervisor AnimationSupervisor { get; set; }

        //DEFAULTS
        private float jumpHeight = 4f;
        //how long should it take for the player to reach the highest point after jumping? (in seconds)
        private float timeToJumpApex = 0.4f;
        //Strength of the double jump
        private float doubleJumpFactor = 0.75f;
        //latency in reaction to controls while airborne
        private float accelerationTimeAirborne = .2f;
        //latency in reaction to controls while grounded
        private float accelerationTimeGrounded = 0.025f;
        private float climbingMoveSpeed = 8;
        private float moveSpeed = 12.0f;
        private float maxHP = 100;
        

        #endregion
        #region Ctors

        public RuyoCharacter()
        {
            DoubleJumpFactor = doubleJumpFactor;
            ClimbingMoveSpeed = climbingMoveSpeed;
            AccelTimeAirborne = accelerationTimeAirborne;
            AccelTimeGrounded = accelerationTimeGrounded;
            MoveSpeed = moveSpeed;
            MaxHP = maxHP;
            CurrentHP = maxHP;
        }
        #endregion
        #region Functionalities
        public void ReceiveHit(IProjectile projectile)
        {
            if (projectile.Alignment != Teams.Player)
            {
                Debug.Log($"Character hit. HP before: {this.CurrentHP}");
                GetHit(projectile.Damage, projectile.AssignedDebuff);
                Debug.Log($"After hit: {this.CurrentHP}");
                if (CurrentHP <= 0.0f)
                {
                    Debug.Log("Character died.");
                    Respawn();
                    CharacterDiedCallback();
                }
            }
        }

        public void Respawn()
        {
            CurrentHP = MaxHP;
            Debug.Log($"Character reset. Hp restored to {this.CurrentHP}");
        }

        public void UpdateState(float LastFrameTime)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateAnimations(Vector3 currentVelocity, Vector3 crosshairPosition, Vector2 maxVelocity,
            Transform playerTransform)
        {
            AnimationSupervisor.ChkAnims(currentVelocity, crosshairPosition, maxVelocity, playerTransform);
        }
        

        /// <summary>
        /// Allows for setting up custom values of gravity that affects the player.
        /// </summary>
        /// <param name="timeToJumpApex">Time it takes the character to reach highest point of jump.</param>
        /// <param name="jumpHeight">How high the player can jump.</param>
        public void CalcGravity(float timeToJumpApex, float jumpHeight)
        {
            Gravity = -(2 * jumpHeight) / (timeToJumpApex*timeToJumpApex);
            JumpVelocity = Mathf.Abs(Gravity) * timeToJumpApex;
            this.timeToJumpApex = timeToJumpApex;
            this.jumpHeight = jumpHeight;
        }
        /// <summary>
        /// Use default values to calculate gravity that affects the player.
        /// </summary>
        public void CalcGravity()
        {
            Gravity = -(2 * jumpHeight) / (timeToJumpApex*timeToJumpApex);
            JumpVelocity = Mathf.Abs(Gravity) * timeToJumpApex;
        }

        private void GetHit(float damage, IDebuff appliedDebuff)
        {
            this.CurrentHP -= damage;
        }
        #endregion
    }
}
