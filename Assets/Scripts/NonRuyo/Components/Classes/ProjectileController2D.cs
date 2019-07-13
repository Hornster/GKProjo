using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{
	/// <summary>
	/// Kontroler pocisku niekierowanego
	/// </summary>
	class ProjectileController2D : AbstractProjectileController
	{
		/// <summary>
		/// Porusza pocisk w zadanym kierunku
		/// </summary>
		public override void UpdateProjectileMovement()
		{
			if (_started)
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = Direction * Speed;
			}
		}
	}
}
