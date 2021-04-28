namespace Spaceship
{
	using UnityEngine;

	public abstract class IProjectilesLauncher<TProjectile> : IWeapon where
		TProjectile : IProjectile<TProjectile>
	{
		#region Fields
		[Header("IProjectileLauncher :")]
		[SerializeField] private TProjectile _projectilePrefab = null;

		private Pool<TProjectile> _projectiles = null;
		#endregion Fields

		#region Propertied
		public TProjectile ProjectilePrefab { get => _projectilePrefab; }

		public Pool<TProjectile> Projectiles { get => _projectiles; }
		#endregion Propertied

		#region Methods
		public override void Init(SpaceshipController owner)
		{
			base.Init(owner);

			if (_projectiles == null)
			{
				_projectiles = new Pool<TProjectile>(ProjectilePrefab);
			}
		}

		protected override bool TryFire()
		{
			if (base.TryFire() == true)
			{
				if (_projectilePrefab != null)
				{
					foreach (Transform muzzle in Muzzles)
					{
						Fire(muzzle);
					}
				}
				else
				{
					Debug.LogError("RocketLauncher::Fire -> _projectilePrefab not found");
				}

				return true;
			}

			return false;
		}
		#endregion Methods
	}
}