using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Skills
{
	interface ISkill
	{
		bool Use(Transform target);
		bool Use(Vector3 position);
		bool Cancel();
	}
}
