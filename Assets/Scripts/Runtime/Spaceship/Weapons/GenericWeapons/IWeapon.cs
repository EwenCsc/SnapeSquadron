namespace Spaceship
{
	using System;
	using System.Collections.Generic;
	using UI;
	using UnityEngine;

	public abstract class IWeapon : MonoBehaviour 
	{
		#region Enums
		private enum InputMode
		{
			GET_BUTTON,
			GET_BUTTON_DOWN
		}
		#endregion Enums

		#region Events
		private Action _weaponFireEventHandler = null;
		public event Action WeaponFire
		{
			add
			{
				_weaponFireEventHandler -= value;
				_weaponFireEventHandler += value;
			}
			remove
			{
				_weaponFireEventHandler -= value;
			}
		}
		#endregion Events

		#region Fields
		[Header("Generals")]
		[SerializeField] private Transform[] _muzzles = null;
		[SerializeField] private string _inputKey = "Fire";
		[SerializeField] private InputMode _inputMode = InputMode.GET_BUTTON_DOWN;

		[Header("UI :")]
		//[SerializeField] private IUI _ui = null;
		[SerializeField] private Sprite _weaponLogo = null;

		private List<IWeaponComponent> _weaponComponents = new List<IWeaponComponent>();
		private SpaceshipController _spaceship = null;
		#endregion Fields

		#region Properties
		public Transform[] Muzzles { get => _muzzles; }

		public List<IWeaponComponent> WeaponComponents { get => _weaponComponents; }

		public Sprite WeaponLogo { get => _weaponLogo; }

		public SpaceshipController Spaceship { get => _spaceship; }
		#endregion Properties

		#region Methods
		public virtual void Init(SpaceshipController owner)
		{
			_spaceship = owner;

			//if (_ui)
			//{
			//	_ui.Init(this);
			//}
		}

		public void DeactivateUI()
		{
			//if (_ui != null)
			//{
			//	_ui.Deactivate();
			//}
			//else
			//{
			//	Debug.LogError("Weapon::ActivateUI -> No UI found");
			//}
		}

		public void ActivateUI()
		{
			//if (_ui != null)
			//{
			//	_ui.Activate();
			//}
			//else
			//{
			//	Debug.LogError("Weapon::ActivateUI -> No UI found");
			//}
		}

		private bool CanShoot()
		{
			foreach (IWeaponComponent weaponComponent in _weaponComponents)
			{
				if (weaponComponent.AllowFire() == false)
				{
					return false;
				}
			}

			return true;
		}

		protected void FetchWeaponComponents(params IWeaponComponent[] weaponComponents)
		{
			foreach (IWeaponComponent component in weaponComponents)
			{
				component.Init(this);
				_weaponComponents.Add(component);
			}
		}

		#region Interface
		public virtual void UpdateWeapon()
		{
			foreach (IWeaponComponent weaponComponent in _weaponComponents)
			{
				weaponComponent.UpdateComponent();
			}

			TryFire();
		}

		protected abstract void Fire(Transform muzzle);

		protected virtual bool TryFire()
		{
			switch (_inputMode)
			{
				case InputMode.GET_BUTTON:
					if (Input.GetButton(_inputKey) == false)
					{
						return false;
					}
					break;
				case InputMode.GET_BUTTON_DOWN:
					if (Input.GetButtonDown(_inputKey) == false)
					{
						return false;
					}
					break;
				default:
					return false;
			}

			if (CanShoot())
			{
				if (_weaponFireEventHandler != null)
				{
					_weaponFireEventHandler.Invoke();
				}

				return true;
			}

			return false;
		}
		#endregion Interface
		#endregion Methods
	}
}