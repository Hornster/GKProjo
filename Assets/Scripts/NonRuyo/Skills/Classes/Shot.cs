
using Assets.Scripts.NonRuyo.Components.Projectile;
using Assets.Scripts.NonRuyo.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Skills
{
	/// <summary>
	/// Używa komponentu ProjectileLauncher2D do wystrzeliwania pocisków
	/// Rodzaje pocisków, ich szybkość itp zależy od komponentu ProjectileLauncher.
	/// 
	/// </summary>
	class Shot : ISkill
	{
		/// <summary>
		/// Projectile launcher ised to shoot
		/// </summary>
		private AbstractProjectileLauncher2D _cannon;

		/// <summary>
		/// Retrieves projetile contoller reference
		/// </summary>
		/// <param name="cannon">GameObject with projectile contoller</param>
		public Shot(AbstractProjectileLauncher2D cannon)
		{
			_cannon = cannon;
		}

		/// <summary>
		/// Does nothing
		/// </summary>
		/// <returns></returns>
		public bool Cancel()
		{
			return false;
		}

		/// <summary>
		/// Shots projectile towards sepecified game object 
		/// </summary>
		/// <param name="target">Game object to shoot at</param>
		/// <returns>False if unable to use skill</returns>
		public bool Use(Transform target)
		{
			if (InSight(target))
			{
				return _cannon.Launch(target);
			}
			else return false;
			
		}

		private bool InSight(Transform target)
		{
			int layerMask = LayerMask.GetMask("Obstacle") | (1 << target.gameObject.layer);
			Vector2 direction = _cannon.GetDirection() ?? (target.position - _cannon.transform.position);
			RaycastHit2D rh = Physics2D.Raycast(_cannon.transform.position, direction, Mathf.Infinity, layerMask);
			if (rh.collider != null && rh.collider.gameObject.layer != LayerMask.NameToLayer("Obstacle"))
			{
				return true;
			}else
			{
				return false;
			}

		}

		/// <summary>
		/// Shots projectile towards specified position
		/// </summary>
		/// <param name="position">Position to shoot at</param>
		/// <returns>False if unable to use skill</returns>
		public bool Use(Vector3 position)
		{
			return _cannon.Launch(position);
		}
	}
}
