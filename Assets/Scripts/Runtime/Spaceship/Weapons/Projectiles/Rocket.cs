﻿namespace Spaceship
{
	using System;
	using UnityEngine;

	public class Rocket : IProjectile<Rocket>
	{
		#region Events
		private Action _rocketDeactivateEventHandler = null;
		public event Action RocketDeactivate
		{
			add
			{
				_rocketDeactivateEventHandler -= value;
				_rocketDeactivateEventHandler += value;
			}
			remove
			{
				_rocketDeactivateEventHandler -= value;
			}
		}

		#endregion Events

		#region Fields
		private Transform _target = null;
		private Rigidbody _rigidbody = null;
		private Vector3 _velocity = Vector3.zero;
		private float _speed = 0.0f;
		private float _steeringSensibility = 0.015f;
		#endregion Fields

		#region Properties
		#endregion Properties

		#region Methods
		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		public void SetTarget(Transform target)
		{
			_target = target;
		}

		public void SetSpeed(float speed)
		{
			_speed = speed;
		}

		public void SetSteeringSensibility(float steeringSensibility)
		{
			_steeringSensibility = steeringSensibility;
		}

		private void Update()
		{
			Vector3 direction = Vector3.Normalize(_target.position - transform.position);

			_velocity = Vector3.Lerp(_velocity, direction * _speed, _steeringSensibility);
			_rigidbody.velocity = _velocity;
		}

		private void OnCollisionEnter(Collision collision)
		{
			if (collision.transform == _target)
			{
				Deactivate();
				if (_rocketDeactivateEventHandler != null)
				{
					_rocketDeactivateEventHandler.Invoke();
					_rocketDeactivateEventHandler = null;
				}
			}
		}
		#endregion Methods
	}
}
