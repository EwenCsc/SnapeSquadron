namespace UI
{
	using Spaceship;
	using System;
	using UnityEngine;
	using UnityEngine.UI;

	[Obsolete]
	public class BlasterUI : IWeaponUI<Blaster>
	{
		#region Fields
		[SerializeField] Image _cooldownFillImage = null;

		private CooldownComponent _cooldownComponent = null;
		#endregion Fields

		#region Methods
		public override void Init(UnityEngine.Object owner)
		{
			base.Init(owner);

			_cooldownFillImage.fillAmount = 0;
		}

		protected override void SetOwner(Blaster owner)
		{
			Owner = owner;

			Owner.Spaceship.WeaponSet += OnWeaponSet;

			foreach (IWeaponComponent weaponComponent in Owner.WeaponComponents)
			{
				if (weaponComponent is CooldownComponent)
				{
					_cooldownComponent = weaponComponent as CooldownComponent;
					_cooldownComponent.UpdateCooldown += OnUpdateCooldown;
				}
			}
		}

		private void OnUpdateCooldown()
		{
			_cooldownFillImage.fillAmount = _cooldownComponent.GetCooldownLevel();
		}
		#endregion Methods
	}
}
