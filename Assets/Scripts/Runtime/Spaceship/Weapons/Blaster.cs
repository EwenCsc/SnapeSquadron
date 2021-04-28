namespace Spaceship
{
	using UnityEngine;

	public class Blaster : IProjectilesLauncher<Bullet>
	{
		#region Fields
		[Header("Blaster")]
		[SerializeField] private float _projectilSpeed = 150.0f;

		[Header("Blaster Components :")]
		[SerializeField] private CooldownComponent _cooldownComponent = null;
		#endregion Fields

		#region Methods
		public override void Init(SpaceshipController owner)
		{
			base.Init(owner);

			FetchWeaponComponents(_cooldownComponent);
		}

		protected override void Fire(Transform muzzle)
		{
			Bullet projectile = Projectiles.GetElement();

			projectile.Activate();
			projectile.SetOrigin(muzzle);
			projectile.SetVelocity((transform.forward * _projectilSpeed) + Spaceship.Rigidbody.velocity);
		}
		#endregion Methods
	}
}
