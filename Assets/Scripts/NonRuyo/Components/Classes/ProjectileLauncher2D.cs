using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{
	/// <summary>
	/// Provides capability to launch projetiles
	/// </summary>
	class ProjectileLauncher2D : AbstractProjectileLauncher2D
	{

		/// <summary>
		/// Launches single projectile
		/// </summary>
		/// <param name="direction">Direction towards which projectiles are launched</param>
		/// <returns>True if launched, false otherwise</returns>
		override public bool Launch(Vector2 direction)
		{
			if (_timeSinceLastLaunch >= launchCooldown)
			{
				GameObject newProjectile = Instantiate(projectilePrefab, gameObject.transform.position, new Quaternion());
				ProjectileController2D projectileController = newProjectile.GetComponent<ProjectileController2D>();
				projectileController.Speed = projectileSpeed;
				projectileController.MaxLifetime = maxLifeTime;
				if(directional)
				{
					direction = _directionProvider.GetDirection();
					newProjectile.transform.Rotate(0, 0, Vector2.SignedAngle(-Vector2.up, direction));
				}else
				{
					direction -= GetLaunchPosition();
					direction.Normalize();
				}
				projectileController.Direction = direction;
				projectileController.StartProjectile();
				_timeSinceLastLaunch = 0;
				return true;
			}
			else return false;
		}

		public override bool Launch(Transform target)
		{
			return Launch(target.position);
		}
	}
}
