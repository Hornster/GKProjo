using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{ 
	/// <summary>
	/// Kontroluje podstawowe parametry pocisku
	/// </summary>
	abstract class AbstractProjectileController : MonoBehaviour, IProjectileController
	{
		/// <summary>
		/// Wskazuje czy pocisk się porusza
		/// </summary>
		protected bool _started = false;
		/// <summary>
		/// Czas życia w sekundach
		/// </summary>
		public float MaxLifetime { get; set; }
		/// <summary>
		/// Prędkość
		/// </summary>
		public float Speed { get; set; }
		/// <summary>
		/// Znormalizowany wektor kierunku poruszania 
		/// </summary>
		public Vector2 Direction { get; set; }

		/// <summary>
		/// Licznik aktualnego czasu życia
		/// </summary>
		protected float _currentLifetime = 0;

		/// <summary>
		/// Jeżeli pocisk się porusza, aktualizuje jego czas życia i ruch
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
		/// pusto
		/// </summary>
		virtual public void UpdateProjectile()
		{

		}
		/// <summary>
		/// Aktualizuje czas życia i usuwa pocisk 
		/// </summary>
		virtual public void UpdateProjectileLifetime()
		{
			_currentLifetime += Time.deltaTime;
			if (_currentLifetime > MaxLifetime)
				Destroy(gameObject);
		}

		/// <summary>
		/// Przesuwa pocisk
		/// </summary>
		abstract public void UpdateProjectileMovement();

		/// <summary>
		/// Rozpoczyna ruch pocisku
		/// </summary>
		virtual public void StartProjectile()
		{
			_started = true;
		}
	
	}
}
