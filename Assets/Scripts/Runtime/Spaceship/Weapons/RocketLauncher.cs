namespace Spaceship
{
	using UnityEngine;

	public class RocketLauncher : IProjectilesLauncher<Rocket>
	{
		#region Fields
		[SerializeField] float _rocketSpeed = 100.0f;

		[Header("Component : ")]
		[SerializeField] private AmmoComponent _ammoComponent = null;

		[SerializeField] // temp
		private Transform _target = null;
		#endregion Fields

		#region Methods
		public override void Init(SpaceshipController owner)
		{
			base.Init(owner);

			FetchWeaponComponents(_ammoComponent);
		}

		protected override void Fire(Transform muzzle)
		{
			Rocket rocket = Projectiles.GetElement();

			rocket.Activate();
			rocket.SetOrigin(muzzle);
			rocket.SetSpeed(_rocketSpeed);
			rocket.SetTarget(_target);

			rocket.RocketDeactivate += _ammoComponent.ReloadOneAmmo;
		}
		#endregion Methods
	}
}
