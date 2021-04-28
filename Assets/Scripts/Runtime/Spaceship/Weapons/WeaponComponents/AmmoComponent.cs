namespace Spaceship
{
	using System;
	using UI;
	using UnityEngine;
	using UnityEngine.UI;

	[Serializable]
	public class AmmoComponent : IWeaponComponent
	{
		#region Events
		private Action _ammoUpdatingEventHandler = null;
		public event Action AmmoUpdating
		{
			add
			{
				_ammoUpdatingEventHandler -= value;
				_ammoUpdatingEventHandler += value;
			}
			remove
			{
				_ammoUpdatingEventHandler -= value;
			}
		}

		#endregion Events

		#region Fields
		[SerializeField] private uint _ammosCapacity = 0;
		[SerializeField] private Sprite _emptyBadge = null;
		[SerializeField] private Text _text = null;
		
		private Text _label;

		private uint _ammo = 0;
		#endregion Fields

		#region Methods
		public override void Init(IWeapon owner)
		{
			base.Init(owner);

			_ammo = _ammosCapacity;
			AmmoUpdating += OnAmmoUpdate;
		}

		public override bool AllowFire()
		{
			return _ammo > 0;
		}

		public void ReloadOneAmmo()
		{
			_ammo++;

			if (_ammoUpdatingEventHandler != null)
			{
				_ammoUpdatingEventHandler.Invoke();
			}
		}

		public override void GenerateBadge(Image logoPrefab, Transform parent)
		{
			Image badge = Badge.Generate(_emptyBadge, logoPrefab, parent);

			_label = GameObject.Instantiate(_text, badge.transform.position, Quaternion.identity, badge.transform);
			_label.text = _ammo.ToString();
			_label.rectTransform.sizeDelta = new Vector2(30, 30);

			//Label label = new Label("0");
			//label.style.width = 30;
			//label.style.height = 30;
			//label.transform.position = badge.transform.position;
		}

		protected override void OnFire()
		{
			_ammo--;

			if (_ammoUpdatingEventHandler != null)
			{
				_ammoUpdatingEventHandler.Invoke();
			}
		}

		private void OnAmmoUpdate()
		{
			_label.text = _ammo.ToString();
		}
		#endregion Methods
	}
}
