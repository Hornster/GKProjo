﻿using System;
using Assets.Scripts.PlayerRemade.Contracts.Characters.Healthbars;
using Assets.Scripts.PlayerRemade.Enums;
using Assets.Scripts.PlayerRemade.Services.Characters.Healthbars.Ruyo;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Contracts.Characters
{
    /// <summary>
    /// Made by: Kozuch Karol
    /// </summary>
    public interface ICharacter
    {
        /// <summary>
        /// When the player looses all HP, this delegate can be used to inform the outside world about that. (Observer pattern)
        /// </summary>
        Action CharacterDiedCallback { get; set; }
        /// <summary>
        /// Instance of the character gameobject associated with this character script.
        /// </summary>
        GameObject CharacterInstance { get; }
        Teams team { get; }
        /// <summary>
        /// Delay in reaction to controls while airborne.
        /// </summary>
        float AccelTimeAirborne { get; }
        float MoveSpeed { get; }
        /// <summary>
        /// Delay in reaction to controls while grounded.
        /// </summary>
        float AccelTimeGrounded { get; }
        float ClimbingMoveSpeed { get; }
        float CurrentHP { get; }
        float MaxHP { get; }
        /// <summary>
        /// Basic acceleration of the character towards source of gravity. Decides how high,and for how long,
        /// will the player jump.
        /// </summary>
        float Gravity { get; }
        /// <summary>
        /// Basic vertical velocity of jump from ground.
        /// </summary>
        float JumpVelocity { get; }
        /// <summary>
        /// Used to scale the jump strength when performing double jump.
        /// Multiply this by JumpVelocity.
        /// </summary>
        float DoubleJumpFactor { get; }
        /// <summary>
        /// Adds a reference to the healthbar script for Ruyo. Doesn't have to be called.
        /// </summary>
        /// <param name="healthBar"></param>
        void AddHealthBarReference(IHealthbar healthBar);
        /// <summary>
        /// Called when the entity was hit by a projectile.
        /// </summary>
        void ReceiveHit(IProjectile projectile);
        /// <summary>
        /// Resets the state of the player, for example when they lost all hp and died.
        /// </summary>
        void Respawn();
        /// <summary>
        /// Called fot update of the player.
        /// </summary>
        /// <param name="LastFrameTime">Time of last frame.</param>
        void UpdateState(float LastFrameTime);
        /// <summary>
        /// Used to update the state of the animation of the character.
        /// </summary>
        /// <param name="currentVelocity">Current character's velocity.</param>
        /// <param name="crosshairPosition">Position of the player's crosshair on the screen.</param>
        /// <param name="maxVelocity">Maximal velocity that the character can achieve.</param>
        /// <param name="playerTransform">Transform object of the player.</param>
        void UpdateAnimations(Vector3 currentVelocity, Vector3 crosshairPosition, Vector2 maxVelocity, Transform playerTransform);
       
    }
}
