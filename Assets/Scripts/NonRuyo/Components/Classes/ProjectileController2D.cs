using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{
	/// <summary>
	/// Controlls projectiles that fly with constant speed 
	/// </summary>
	class ProjectileController2D : MonoBehaviour, IProjectileController
	{
		/// <summary>
		/// Indicates whether projectile started moving
		/// </summary>
		private bool _started = false;
		/// <summary>
		/// Maximum lifetime of projectile
		/// </summary>
		public float MaxLifetime { get; set; }
		/// <summary>
		/// Speed of projectile
		/// </summary>
		public float Speed { get; set; }
		/// <summary>
		/// Normalized direction vector
		/// </summary>
		public Vector2 Direction { get; set; }

		private float _currentLifetime = 0;

		/// <summary>
		/// If movement started, updates projectile lifetime and position
		/// </summary>
		void Update()
		{
			if (_started)
			{
				UpdateProjectileLifetime();
				UpdateProjectileMovement();
			}
		}
		/// <summary>
		/// Empty
		/// </summary>
		public void UpdateProjectile()
		{
			
		}
		/// <summary>
		/// Increases projectile lifetime and destroys it
		/// </summary>
		public void UpdateProjectileLifetime()
		{
			_currentLifetime += Time.deltaTime;
			if (_currentLifetime > MaxLifetime)
				Destroy(gameObject);
		}

		/// <summary>
		/// Moves projectile
		/// </summary>
		public void UpdateProjectileMovement()
		{
			if(_started)
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = Direction * Speed;
			}
		}

		/// <summary>
		/// Activates projectile
		/// </summary>
		public void Start() {
			_started = true;
		}
	}
}
