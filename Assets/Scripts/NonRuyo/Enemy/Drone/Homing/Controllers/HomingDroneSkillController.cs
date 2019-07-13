using Assets.Scripts.NonRuyo.Skills;
using Assets.Scripts.NonRuyo.Components;
using Assets.Scripts.NonRuyo.Components.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Enemy.Drone.Homing
{
	/// <summary>
	/// Zarząda używaniem skilli
	/// </summary>
	class HomingDroneSkillController : MonoBehaviour
	{
		FieldOfViewController2D _radar;
		GameObject _player;
		public List<ISkill> SkillList { get; set; }

		public void Start()
		{
			SkillList = new List<ISkill>();
			_radar = gameObject.GetComponentInChildren<FieldOfViewController2D>();
			_player = GameObject.FindGameObjectWithTag("CharCollisionBody");
			SkillList.Add(new Shot(gameObject.GetComponentInChildren<HomingProjectileLauncher2D>()));
		}

		public void Update()
		{
			if (_radar.IsDetected())
			{
				SkillList[0].Use(_player.transform);
			}
		}
	}
}
