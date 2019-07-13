using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{ 
	abstract class AbstractProjectileLauncher2D : MonoBehaviour
	{
		public float launchCooldown; //Minimal time interval between launches
		public float projectileSpeed; //All projectiles are launched with this speed
		public float maxLifeTime; //Max life time of launched projectiles
		public GameObject projectilePrefab; //Prefab of launched projectiles
		protected float _timeSinceLastLaunch;
		public bool directional;
		protected IDirectionSource2D _directionProvider;

		protected void Start()
		{
			_timeSinceLastLaunch = launchCooldown + 1; //Starts with time since last launch greater than cooldown, reason is the way of updating time since last launch
			if (directional)
			{
				_directionProvider = transform.parent.GetComponent<IDirectionSource2D>();
			}
		}

		/// <summary>
		/// Measures time since last launch (if needed)
		/// </summary>
		virtual protected void Update()
		{
			if (_timeSinceLastLaunch < launchCooldown)
				_timeSinceLastLaunch += Time.deltaTime;
		}

		public Vector2? GetDirection()
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

		abstract public bool Launch(Vector2 direction);
		abstract public bool Launch(Transform target);
	}
}
