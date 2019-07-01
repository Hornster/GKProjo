﻿using Assets.Scripts.NonRuyo.Components.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.NonRuyo.Components
{
	class TurretController : MonoBehaviour, IDirectionSource2D
	{
		private GameObject _player;
		private ProjectileLauncher2D _cannon;
		private Vector2 _direction;

		public float angleSpeed;
			
		private void Start()
		{
			_player = GameObject.FindGameObjectWithTag("Player");
			_cannon = GetComponentInChildren<ProjectileLauncher2D>();
			_direction = (_cannon.transform.position - transform.position).normalized;

		}

		private void Update()
		{
			/*
			 Poprawka:
			 Najpierw oblicza się kąt pomiędzy wektorem od wieżycxki do gracza
			 a wektorem aktualnego kieunku
			 POtem, jeżeli kąt ten jest mniejszy niż kąt 
			 */
			
			Vector2 vectorToPlayer = (_player.transform.position - transform.position).normalized;
			float angleBetweenDirectionAndPlayer = Vector2.SignedAngle(_direction, vectorToPlayer);
			float currentRotationAngle = Time.deltaTime * angleSpeed * (angleBetweenDirectionAndPlayer > 0 ? 1f : -1f);
			if (Math.Abs(angleBetweenDirectionAndPlayer) < Math.Abs(currentRotationAngle))
			{
				transform.Rotate(new Vector3(0, 0, angleBetweenDirectionAndPlayer));
			}else
			{
				transform.Rotate(new Vector3(0, 0, currentRotationAngle));
			}
			_direction = (_cannon.transform.position - transform.position).normalized;
		}

		public bool PointsOnPlayer()
		{
			int layerMask = LayerMask.GetMask("Player");
			RaycastHit2D rh = Physics2D.Raycast(transform.position, _direction, Mathf.Infinity, layerMask);
			if (rh.collider != null && rh.collider.tag == "CharCollisionBody")
				return true;
			else return false;
		}

		public Vector2 GetDirection()
		{
			return _direction;
		}
	}
}
