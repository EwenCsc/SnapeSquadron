namespace Spaceship
{
	using UnityEngine;
	using UnityEngine.UI;

	public abstract class IWeaponComponent
	{
		#region Fields
		private IWeapon _owner = null;
		#endregion Fields

		#region Properties
		public IWeapon Owner { get => _owner; }
		#endregion Properties

		#region Methods
		public virtual void Init(IWeapon owner)
		{
			_owner = owner;

			_owner.WeaponFire += OnFire;
		}

		public virtual void UpdateComponent() { }

		public abstract bool AllowFire();

		public virtual void GenerateBadge(Image logoPrefab, Transform parent) { }

		protected abstract void OnFire();
		#endregion Methods
	}
}
