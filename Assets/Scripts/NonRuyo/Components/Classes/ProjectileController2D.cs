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
	class ProjectileController2D : AbstractProjectileController
	{
		public override void UpdateProjectileMovement()
		{
			if (_started)
			{
				gameObject.GetComponent<Rigidbody2D>().velocity = Direction * Speed;
			}
		}
	}
}
