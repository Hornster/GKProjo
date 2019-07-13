using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{ 
	/// <summary>
	/// Kontroluje tworzenie pocisków
	/// </summary>
	abstract class AbstractProjectileLauncher2D : MonoBehaviour
	{
		/// <summary>
		/// Czas pomiędzy kolejnymi wystrzałami
		/// </summary>
		public float launchCooldown; 
		/// <summary>
		/// Prędkość tworzonych pocisków
		/// </summary>
		public float projectileSpeed;
		/// <summary>
		/// Czas życia pocisków
		/// </summary>
		public float maxLifeTime; 
		/// <summary>
		/// Prefab pocisku
		/// </summary>
		public GameObject projectilePrefab; 
		/// <summary>
		/// Licznik czasu od ostatniego wystrzału
		/// </summary>
		protected float _timeSinceLastLaunch;
		/// <summary>
		/// Wskazuje czy wyrzutnai pocisków jest zależna od kierunku
		/// </summary>
		public bool directional;
		/// <summary>
		/// Źródło z którego pobierany jest aktualny kierunek w którym zwrócona jest wyrzytnia (np. wieżyczka)
		/// </summary>
		protected IDirectionSource2D _directionProvider;

		/// <summary>
		/// Pobranie źródła kierunku
		/// </summary>
		protected void Start()
		{
			_timeSinceLastLaunch = launchCooldown + 1; 
			if (directional)
			{
				_directionProvider = transform.parent.GetComponent<IDirectionSource2D>(); 
			}
		}

		/// <summary>
		/// Aktualizacjs czasu od ostatniego wystrzału
		/// </summary>
		virtual protected void Update()
		{
			if (_timeSinceLastLaunch < launchCooldown)
				_timeSinceLastLaunch += Time.deltaTime;
		}

		/// <summary>
		/// Sprawdza, czy wyrzytnia jest zależna od kierunku i zwraca kierunek 
		/// </summary>
		/// <returns>null jeżeli wyrzytnia niekierunkowa, kierunek w którym zwrócona jest wyrzutnia w przeciwnym wypadku</returns>
		public Vector2? GetDirection()
		{
			if (directional)
				return _directionProvider.GetDirection();
			else
				return null;
		}

		/// <summary>
		/// Zwraca pozycję wyrzutni
		/// </summary>
		/// <returns></returns>
		public Vector2 GetLaunchPosition()
		{
			return transform.position;
		}

		/// <summary>
		/// Wystrzał w kierunku punktu
		/// </summary>
		/// <param name="direction"></param>
		/// <returns></returns>
		abstract public bool Launch(Vector2 direction);
		/// <summary>
		/// Wystrzał w kierunku celu
		/// </summary>
		/// <param name="target"></param>
		/// <returns></returns>
		abstract public bool Launch(Transform target);
	}
}
