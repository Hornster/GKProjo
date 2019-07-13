using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{
	class HomingProjectileLauncher2D : AbstractProjectileLauncher2D
	{
		override public bool Launch(Vector2 direction)
		{
			return false;

		}

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
