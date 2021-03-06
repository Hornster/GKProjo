﻿using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Enemy.Drone.Homing
{
	/// <summary>
	/// Dane pocisku naprowadzającego
	/// </summary>
	class DroneHomingProjectile : MonoBehaviour, IProjectile
	{

		public float Damage
		{
			get => 15f;
		}

		public Teams Alignment
		{
			get => Teams.Enemy;
		}

		public bool HasItsOwnLifeTimer
		{
			get => false;
		}

		public bool IsTimed
		{
			get => false;
		}

		public float SkillDuration
		{
			get => 0;
		}

		public bool CanPenetrate
		{
			get
			{
				return false;
			}
		}

		public IDebuff AssignedDebuff
		{
			get => null;
		}
	}
}
