using System;
using Assets.Scripts.PlayerRemade.Enums;
using UnityEngine;

namespace Assets.Scripts.PlayerRemade.Contracts.Skills
{
    public interface ISkill
    {

        #region Members
        SkillType skillType { get; }
        /// <summary>
        /// Value assigned to this skill, for example amount of damage or teleportation distance
        /// </summary>
        float SkillValue { get; }
        ///<summary>Returns true if the skill is recharged and can be used again. </summary>
        bool IsRecharged { get; }

        ///<summary>Retruns boolean that indicates whether the skill is currently selected by user.</summary>
        bool IsActive { get; }

        /// <summary>
        /// Returns the total time needed for the skill to become recharged.
        /// </summary>
        /// <returns>Time, in seconds.</returns>
        float SkillMaxCD { get; }

        /// <summary>
        /// Returns the current amount of time that passed since last skill use.
        /// The time stops increasing when its value is greater than the MaxCD value.
        /// </summary>
        /// <returns>Time, in seconds.</returns>
        float SkillCurrCD { get; }

        /// <summary>
        /// /// <summary>
        /// Returns a crosshair sprite assigned to the skill. Returns NULL if none attached.
        /// </summary>
        Sprite SkillCrosshair { get; }
        /// <summary>
        /// Retrieves the gameobject containing the projectile of this skill. Can be NULL if none has been assigned.
        /// </summary>
        GameObject Projectile { get; }
        /// <summary>
        /// Icon assigned to this skill. Can be null.
        /// </summary>
        Sprite icon { get; }

        #endregion

        /// <summary>
        /// Activates the skills.
        /// </summary>
        /// <param name="dirVector">Direction in which the skill will be activated (from player to crosshair), or the position of the skill execution (needs confirmation)</param>//TODO confirm that dirVector is used also for strict position fo skill execution
        /// <param name="teamTag">Team that launched the projectile.</param>
        /// <param name="playerSpeed">Speed of the player - added to bullet velocity.</param>
        void UseSkill(Vector2 dirVector, Teams teamTag, Vector2 playerSpeed);
        /// <summary>
        /// Updates the cooldown timer of the skill.
        /// </summary>
        /// <param name="lastFrameTime">Time of last frame.</param>
        void UpdateSkillCD(float lastFrameTime);
        /// <summary>
        /// Activates skill. By default, if skill is recharged already. 
        /// </summary>
        /// <returns>If skill has been activated successfully - TRUE. Otherwise FALSE.</returns>
        bool ActivateSkill();
        /// <summary>
        /// Deactivates skill.
        /// </summary>
        void DeactivateSkill();
    }

}