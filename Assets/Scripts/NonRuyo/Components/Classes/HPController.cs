using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components
{
	/// <summary>
	/// Stores information about object HP and updates it over time
	/// </summary>
	class HPController : MonoBehaviour
	{
		/// <summary>
		/// Maximum value of object HP
		/// </summary>
		public float HPMax;
		/// <summary>
		/// Health point regeneration rate. Amount of HP regenerated every regen period.
		/// </summary>
		public float HPRegen;
		/// <summary>
		/// Time between HP regen updates.
		/// </summary>
		public float regenInterval;
		/// <summary>
		/// Determines if HP can regenerate after dropping below 0
		/// </summary>
		public bool canRegenerateAfterDeath;
		/// <summary>
		/// Regeneration time delay after HP dropped below 0
		/// </summary>
		public float afterDeathRegenDelay;

		/// <summary>
		/// Current HP value. Minimum value is 0f. Object with this hp value is considered dead
		/// </summary>
		private float _currentHP;
		private float _timeAfterLastRegenUpdate = 0;

		public void Start()
		{
			_currentHP = HPMax; //Start with max HP
		}

		/// <summary>
		/// Handles regeneration
		/// </summary>
		public void Update()
		{
			if(_currentHP < HPMax) //Regenerate only if hp is damaged
			{
				if(IsDead() && canRegenerateAfterDeath) //Check if HP dropped below 0 and regen is possible
				{
					if(_timeAfterLastRegenUpdate >= afterDeathRegenDelay) //Compare time after last regen to regen delay after death
					{
						_currentHP += HPRegen;
						if (_currentHP > HPMax)
							_currentHP = HPMax;
						_timeAfterLastRegenUpdate = 0;
					}
					else 
						_timeAfterLastRegenUpdate += Time.deltaTime; //Update regen time
				}
				else if(_timeAfterLastRegenUpdate < regenInterval) //If its not regeneration time
				{
					_timeAfterLastRegenUpdate += Time.deltaTime; //Update regen time
				}
				else //Its regen time
				{
					_currentHP += HPRegen;
					if (_currentHP > HPMax)
						_currentHP = HPMax;
					_timeAfterLastRegenUpdate = 0;
				}
			}
		}

		public void ApplyDamage(float damage)
		{
			_currentHP -= damage;
			if (_currentHP <= 0f)
				_currentHP = 0f;
		} 

		public bool IsDead()
		{
			if (_currentHP == 0f)
				return true;
			else
				return false;
		}

		public bool IsAlive()
		{
			if (_currentHP != 0f)
				return true;
			else
				return false;
		}
	}
}
