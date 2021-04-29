namespace Spaceship
{
	using UnityEngine;

	/// <summary>
	/// Basic gun with infinit ammunition and no overheating.
	/// </summary>
	public class Blaster : IProjectilesLauncher<Bullet>
	{
		#region Fields
		[Header("Blaster")]
		[SerializeField] private float _projectilSpeed = 150.0f;

		[Header("Blaster Components :")]
		[SerializeField] private CooldownComponent _cooldownComponent = null;
		#endregion Fields

		#region Methods
		/// <inheritdoc/>
		public override void Init(SpaceshipController owner)
		{
			base.Init(owner);

			FetchWeaponComponents(_cooldownComponent);
		}

		/// <inheritdoc/>
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
