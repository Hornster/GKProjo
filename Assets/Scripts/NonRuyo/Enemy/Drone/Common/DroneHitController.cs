using Assets.Scripts.PlayerRemade.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components
{
	/// <summary>
	/// Kontroluje co dzieje się z obiektem po otrzymaniu trafienia
	/// </summary>
	class DroneHitController : MonoBehaviour
	{
		private HPController _hpController;

		private void Start()
		{
			_hpController = gameObject.GetComponent<HPController>();
		}

		/// <summary>
		/// Nakłada efekty po ptrzymaniu trafienia 
		/// </summary>
		/// <param name="collision"></param>
		private void OnTriggerEnter2D(Collider2D collision)
		{
			IProjectile projectile = collision.gameObject.GetComponent<IProjectile>(); //Jeżeli collider to pocisk to pobierz referncję
			if(projectile != null && projectile.Alignment == PlayerRemade.Enums.Teams.Player)
			{
				if(GetComponentInChildren<ShieldHitController>().gameObject.GetComponent<HPController>().IsDead())
				{
					_hpController.ApplyDamage(projectile.Damage);
					Destroy(collision.gameObject);
					if (_hpController.IsDead())
					{
						Destroy(gameObject);
					}
				}
			}
		}
	}
}
