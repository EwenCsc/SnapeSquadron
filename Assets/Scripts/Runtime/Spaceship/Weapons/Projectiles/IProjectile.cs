namespace Spaceship
{
	using UnityEngine;

	/// <summary>
	/// A generic Projectile which will be fired by <see cref="IProjectilesLauncher"/>.
	/// </summary>
	public abstract class IProjectile<TProjectile> : MonoBehaviour, IPoolable where
		TProjectile : IProjectile<TProjectile>
	{
		#region Fields
		[SerializeField] private float _lifeTime = 10.0f;

		private float _currentLifeTime = 0.0f;
		private bool _isFree = false;
		#endregion Fields

		#region Properties
		public float LifeTime { get => _lifeTime; }

		public float CurrentLifeTime
		{
			get => _currentLifeTime;
			protected set => _currentLifeTime = value;
		}
		#endregion Properties

		#region Methods
		/// <inheritdoc/>
		public void Reset()
		{
			_currentLifeTime = 0.0f;
			_isFree = false;
			gameObject.SetActive(true);
		}

		/// <inheritdoc/>
		public bool IsFree()
		{
			return _isFree;
		}

		/// <summary>
		/// Set the spawn point of the <see cref="Bullet"/>
		/// </summary>
		/// <param name="origin">Spawn point (generaly a weapon's muzzle)</param>
		public virtual void SetOrigin(Transform origin)
		{
			transform.position = origin.position;
			transform.rotation = origin.rotation;
		}

		/// <inheritdoc/>
		public void Activate()
		{
			_isFree = false;
			gameObject.SetActive(true);
			Reset();
		}

		/// <inheritdoc/>
		public void Deactivate()
		{
			_isFree = true;
			gameObject.SetActive(false);
		}
		#endregion Methods
	}
}
