using Assets.Scripts.PlayerRemade.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components
{
	/// <summary>
	/// Zarządza działaniem tarczy. Rozmiar tarczy zależy od kolidera.
	/// </summary>
	public class ShieldHitController : MonoBehaviour
	{
		private HPController _hpController;
		private SpriteRenderer _sprite;
		private Collider2D _collider;

		private void Start()
		{
			_hpController = gameObject.GetComponent<HPController>();
			_sprite = gameObject.GetComponent<SpriteRenderer>();
			_collider = gameObject.GetComponent<Collider2D>();
		}

		/// <summary>
		/// Wyświetla i uaktywnia (lub na odwrót) sprite i collider tarczy w zależności od tego czy tarcza nie została znisczona
		/// </summary>
		private void Update()
		{
			if (_hpController.IsDead())
			{
				_sprite.enabled = false;
				_collider.enabled = false;
			}else
			{
				_sprite.enabled = true;
				_collider.enabled = true;
			}
		}

		/// <summary>
		/// Nakłada efekty po ptrzymaniu trafienia 
		/// </summary>
		/// <param name="collision"></param>
		private void OnTriggerEnter2D(Collider2D collision)
		{
			
			IProjectile projectile = collision.gameObject.GetComponent<IProjectile>(); //Jeżeli collider to pocisk to pobierz referncję
			if (projectile != null && projectile.Alignment == PlayerRemade.Enums.Teams.Player)
			{
				Debug.Log("Shield");
				_hpController.ApplyDamage(projectile.Damage);
				Destroy(collision.gameObject);
			}
		}
	}

}
