using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components.Projectile
{

	interface IProjectileController
	{
		void StartProjectile();
		void UpdateProjectile();
		void UpdateProjectileMovement();
		void UpdateProjectileLifetime();
	}
}
