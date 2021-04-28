namespace Spaceship
{
	using UnityEngine;

	public class RocketLauncher : IProjectilesLauncher<Rocket>
	{
		#region Fields
		[SerializeField] float _rocketSpeed = 100.0f;

		[SerializeField]
		private Transform _target = null;
		#endregion Fields

		#region Methods
		protected override void Fire(Transform muzzle)
		{
			Rocket rocket = Projectiles.GetElement();

			rocket.Activate();
			rocket.SetOrigin(muzzle);
			rocket.SetSpeed(_rocketSpeed);
			rocket.SetTarget(_target);
		}
		#endregion Methods
	}
}
