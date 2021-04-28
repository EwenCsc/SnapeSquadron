using UnityEngine;

namespace Spaceship
{

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
		public void Reset()
		{
			_currentLifeTime = 0.0f;
			_isFree = false;
			gameObject.SetActive(true);
		}

		public bool IsFree()
		{
			return _isFree;
		}

		public virtual void SetOrigin(Transform origin)
		{
			transform.position = origin.position;
			transform.rotation = origin.rotation;
		}

		public void Activate()
		{
			_isFree = false;
			gameObject.SetActive(true);
			Reset();
		}

		public void Deactivate()
		{
			_isFree = true;
			gameObject.SetActive(false);
		}
		#endregion Methods
	}
}
