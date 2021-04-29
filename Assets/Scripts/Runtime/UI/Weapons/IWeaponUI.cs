namespace UI
{
	using Spaceship;
	using System;
	using UnityEngine;

	[Obsolete]
	public abstract class IWeaponUI<TWeapon> : IUI where
		TWeapon : IWeapon
	{
		#region Fields
		private TWeapon _owner = null;
		#endregion Fields

		#region Properties
		public TWeapon Owner 
		{ 
			get => _owner;
			protected set => _owner = value;
		}
		#endregion Properties

		#region Methods
		public override void Init(UnityEngine.Object owner)
		{
			SetOwner(owner as TWeapon);
		}

		protected abstract void SetOwner(TWeapon owner);

		protected void OnWeaponSet(IWeapon weapon)
		{
			if (weapon == Owner)
			{
				Owner.ActivateUI();
			}
			else
			{
				Owner.DeactivateUI();
			}
		}
		#endregion Methods
	}
}