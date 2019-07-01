using Assets.Scripts.PlayerRemade.Contracts;
using Assets.Scripts.PlayerRemade.Contracts.Skills;
using Assets.Scripts.PlayerRemade.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Enemy.Drone.Sniper
{
	class DroneSniperProjectile : MonoBehaviour, IProjectile
	{

		public float Damage
		{
			get => 40f;
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
