namespace Spaceship
{
	using UnityEngine;

	/// <summary>
	/// <see cref="IProjectilesLauncher"/> is a generic <see cref="IWeapon"/> which will fire projectils
	/// </summary>
	/// <typeparam name="TProjectile">It's the <see cref="IProjectile"/> which will be launch</typeparam>
	public abstract class IProjectilesLauncher<TProjectile> : IWeapon where
		TProjectile : IProjectile<TProjectile>
	{
		#region Fields
		[Header("IProjectileLauncher :")]
		[SerializeField] private TProjectile _projectilePrefab = null;

		private Pool<TProjectile> _projectiles = null;
		#endregion Fields

		#region Propertied
		public Pool<TProjectile> Projectiles { get => _projectiles; }
		#endregion Propertied

		#region Methods
		/// <inheritdoc/>
		public override void Init(SpaceshipController owner)
		{
			base.Init(owner);

			if (_projectiles == null)
			{
				_projectiles = new Pool<TProjectile>(_projectilePrefab);
			}
		}

		/// <inheritdoc/>
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