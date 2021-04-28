namespace Spaceship
{
	using UnityEngine;

	[RequireComponent(typeof(Rigidbody))]
	public class Bullet : IProjectile<Bullet>
	{
		#region Fields
		[SerializeField] private Rigidbody _rigidbody = null;
		#endregion Fields

		#region Methods
		public virtual void SetVelocity(Vector3 velocity)
		{
			if (_rigidbody != null)
			{
				_rigidbody.velocity = velocity;
			}
			else
			{
				Debug.LogError("Spaceship::Projectile : No rigidbody found");
			}
		}

		private void Update()
		{
			CurrentLifeTime += Time.deltaTime;

			if (CurrentLifeTime >= LifeTime)
			{
				CurrentLifeTime = 0.0f;
				Deactivate();
			}
		}
		#endregion Methods
	}
}