namespace Spaceship
{
	using UnityEngine;

	public class Rocket : IProjectile<Rocket>
	{
		#region Fields
		private Transform _target = null;
		private Rigidbody _rigidbody = null;
		private Vector3 _velocity = Vector3.zero;
		private float _speed = 0.0f;
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

		private void Update()
		{
			Vector3 direction = Vector3.Normalize(_target.position - transform.position);

			_velocity = Vector3.Lerp(_velocity, direction * _speed, 0.2f);

			_rigidbody.velocity = _velocity;
		}
		#endregion Methods
	}
}
