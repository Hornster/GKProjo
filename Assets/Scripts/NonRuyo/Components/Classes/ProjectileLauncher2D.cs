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
	class ProjectileLauncher2D : MonoBehaviour
	{
		public float launchCooldown; //Minimal time interval between launches
		public float projectileSpeed; //All projectiles are launched with this speed
		public float maxLifeTime; //Max life time of launched projectiles
		public GameObject projectilePrefab; //Prefab of launched projectiles
		public bool directional;
		private float _timeSinceLastLaunch;
		private IDirectionSource2D _directionProvider;

		private void Start()
		{
			_timeSinceLastLaunch = launchCooldown + 1; //Starts with time since last launch greater than cooldown, reason is the way of updating time since last launch
			if(directional)
			{
				_directionProvider = transform.parent.GetComponent<IDirectionSource2D>();
			}
		}

		/// <summary>
		/// Measures time since last launch (if needed)
		/// </summary>
		public void Update()
		{
			if (_timeSinceLastLaunch < launchCooldown)
				_timeSinceLastLaunch += Time.deltaTime;
		}

		/// <summary>
		/// Launches single projectile
		/// </summary>
		/// <param name="direction">Direction towards which projectiles are launched</param>
		/// <returns>True if launched, false otherwise</returns>
		public bool Launch(Vector2 direction)
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
				projectileController.Start();
				_timeSinceLastLaunch = 0;
				return true;
			}
			else return false;
		}

		public Vector3? GetDirection()
		{
			if (directional)
				return _directionProvider.GetDirection();
			else
				return null;
		}

		public Vector2 GetLaunchPosition()
		{
			return transform.position;
		}


	}
}
