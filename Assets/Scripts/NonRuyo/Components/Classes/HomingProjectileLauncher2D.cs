using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{
	/// <summary>
	/// Wyrzytnia pocisków kierowanych
	/// </summary>
	class HomingProjectileLauncher2D : AbstractProjectileLauncher2D
	{
		/// <summary>
		/// Zwraca false 
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		override public bool Launch(Vector2 direction)
		{
			return false;

		}

		/// <summary>
		/// Jeżeli cooldown od ostatniego wystrzału minął, tworzy pocisk i nadaje mu właściwości
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		public override bool Launch(Transform target)
		{
			if (_timeSinceLastLaunch >= launchCooldown)
			{
				GameObject newProjectile = Instantiate(projectilePrefab, gameObject.transform.position, new Quaternion());
				AbstractProjectileController projectileController = newProjectile.GetComponent<AbstractProjectileController>();
				projectileController.Speed = projectileSpeed;
				projectileController.MaxLifetime = maxLifeTime;
				(projectileController as HomingProjectileController2D).Target = target;
				projectileController.StartProjectile();
				_timeSinceLastLaunch = 0;
				return true;
			}
			else return false;
		}
	}
}
