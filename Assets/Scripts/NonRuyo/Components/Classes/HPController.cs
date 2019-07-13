using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components
{
	/// <summary>
	/// Zarządza stanem zdrowia obiektu
	/// </summary>
	class HPController : MonoBehaviour
	{
		/// <summary>
		/// Max HP
		/// </summary>
		public float HPMax;
		/// <summary>
		/// Liczba punktów HP regenerowana w czasie regenInterval
		/// </summary>
		public float HPRegen;
		/// <summary>
		/// Czas w sek. pomiędzy regeneracją życia
		/// </summary>
		public float regenInterval;
		/// <summary>
		/// Czy HP może odnawiać się po spadku poniżej 0 (przydatne dla tarcz)
		/// </summary>
		public bool canRegenerateAfterDeath;
		/// <summary>
		/// Czas w sek. do rozpoczęcia regeneracji po "śmierci"
		/// </summary>
		public float afterDeathRegenDelay;

		/// <summary>
		///Aktualne HP. Minimalna wartość to 0f (obiekt martwy)
		/// </summary>
		private float _currentHP;
		/// <summary>
		/// Licznik czasu regeneracji
		/// </summary>
		private float _timeAfterLastRegenUpdate = 0;

		public void Start()
		{
			_currentHP = HPMax; //Start with max HP
		}

		/// <summary>
		/// Aktualizuje stan zdrowia
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

		/// <summary>
		/// Zmniejsza zdrowie o zadaną wartosć
		/// </summary>
		/// <param name="damage"></param>
		public void ApplyDamage(float damage)
		{
			_currentHP -= damage;
			if (_currentHP <= 0f)
				_currentHP = 0f;
		} 

		/// <summary>
		/// Sprawdza, czy obiekt umarł
		/// </summary>
		/// <returns>True jeżeli HP ma wartość 0f, false w przecinym wypadku</returns>
		public bool IsDead()
		{
			if (_currentHP == 0f)
				return true;
			else
				return false;
		}

		/// <summary>
		/// !IsDead
		/// </summary>
		/// <returns></returns>
		public bool IsAlive()
		{
			if (_currentHP != 0f)
				return true;
			else
				return false;
		}
	}
}
