using Assets.Scripts.NonRuyo.Components.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{
	class HomingProjectileController2D : AbstractProjectileController
	{
		protected Transform _target;
		public override void UpdateProjectileMovement()
		{
			if (_started)
			{
				if(Target == null)
				{
					Destroy(gameObject);
					return;
				}
				Direction = (Target.position - transform.position).normalized;
				gameObject.GetComponent<Rigidbody2D>().velocity = Direction * Speed;
			}
		}

		public Transform Target {
			get { return _target;  }
			set { _target = value; }
		} 
		
	}
}
