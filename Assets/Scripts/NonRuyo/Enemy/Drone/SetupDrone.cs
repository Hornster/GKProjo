using Assets.Scripts.NonRuyo.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Enemy.Drone.Sniper
{
	/// <summary>
	/// Konfiguruje komponenty dla drona 
	/// </summary>
	class SetupDrone : MonoBehaviour
	{
		public void Start()
		{
			var fovcontroller = gameObject.GetComponentInChildren<FieldOfViewController2D>();
			fovcontroller.LayerFilter[LayerMask.NameToLayer("Player")] = true; 
			fovcontroller.TrackObjectsWithTag("CharCollisionBody");
			
		}
	}
}
