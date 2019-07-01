using Assets.Scripts.NonRuyo.Skills;
using Assets.Scripts.NonRuyo.Components;
using Assets.Scripts.NonRuyo.Components.Projectile;
using Assets.Scripts.NonRuyo.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Enemy.Drone.Sniper
{
	/// <summary>
	/// Zarząda używaniem skilli
	/// </summary>
	class SniperDroneSkillController : MonoBehaviour
	{
		FieldOfViewController2D _radar;
		TurretController _turret;
		GameObject _player; 
		public List<ISkill> SkillList { get; set; }

		public void Start()
		{
			SkillList = new List<ISkill>();
			_radar = gameObject.GetComponentInChildren<FieldOfViewController2D>();
			_turret = gameObject.GetComponentInChildren<TurretController>();
			_player = GameObject.FindGameObjectWithTag("CharCollisionBody");
			SkillList.Add(new Shot(gameObject.GetComponentInChildren<ProjectileLauncher2D>()));
		}

		public void Update()
		{
			if (_radar.IsDetected() && _turret.PointsOnPlayer()) 
			{
					SkillList[0].Use(_player.transform);
			}
		}
	}
}
