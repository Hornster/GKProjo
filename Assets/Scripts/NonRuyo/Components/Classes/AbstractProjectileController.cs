using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{ 
	abstract class AbstractProjectileController : MonoBehaviour, IProjectileController
	{
		/// <summary>
		/// Indicates whether projectile started moving
		/// </summary>
		protected bool _started = false;
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

		protected float _currentLifetime = 0;

		/// <summary>
		/// If movement started, updates projectile lifetime and position
		/// </summary>
		virtual protected void Update()
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
		virtual public void UpdateProjectile()
		{

		}
		/// <summary>
		/// Increases projectile lifetime and destroys it
		/// </summary>
		virtual public void UpdateProjectileLifetime()
		{
			_currentLifetime += Time.deltaTime;
			if (_currentLifetime > MaxLifetime)
				Destroy(gameObject);
		}

		/// <summary>
		/// Moves projectile
		/// </summary>
		abstract public void UpdateProjectileMovement();

		/// <summary>
		/// Activates projectile
		/// </summary>
		virtual public void StartProjectile()
		{
			_started = true;
		}
	
	}
}
