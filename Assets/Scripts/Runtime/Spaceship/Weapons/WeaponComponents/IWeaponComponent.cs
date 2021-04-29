namespace Spaceship
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Contains the logic of different specific component( or constraint)
	/// </summary>
	public abstract class IWeaponComponent
	{
		#region Fields
		private IWeapon _owner = null;
		#endregion Fields

		#region Properties
		public IWeapon Owner { get => _owner; }
		#endregion Properties

		#region Methods
		/// <param name="owner"><see cref="IWeapon"/> which control the <see cref="IWeaponComponent"/></param>
		public virtual void Init(IWeapon owner)
		{
			_owner = owner;

			_owner.WeaponFire += OnFire;
		}

		/// <summary>
		/// Logic called every frames to manage the component.
		/// </summary>
		public virtual void UpdateComponent() { }

		/// <summary>
		/// Check conditions to allow or not a <see cref="IWeapon"/> to fire.
		/// </summary>
		public abstract bool AllowFire();

		/// <summary>
		/// Genrate the UI of this <see cref="IWeaponComponent"/>
		/// </summary>
		public virtual void GenerateUI(Image logoPrefab, Transform parent) { }

		/// <summary>
		/// Logic of the component when its <see cref="IWeapon"/> fires.
		/// This func is subscribed to its <see cref="IWeapon"/>'s fire event handler.
		/// </summary>
		protected abstract void OnFire();
		#endregion Methods
	}
}
