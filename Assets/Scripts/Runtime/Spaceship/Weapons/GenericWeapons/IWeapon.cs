namespace Spaceship
{
	using System;
	using System.Collections.Generic;
	using UI;
	using UnityEngine;

	/// <summary>
	/// <see cref="IWeapon"/> is a generic Object which will be use by a <see cref="SpaceshipController"/> to shoot its opponents.
	/// </summary>
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
		/// <summary>
		/// Init the <see cref="IWeapon"/>
		/// </summary>
		/// <param name="owner"><see cref="SpaceshipController"/> which control this <see cref="IWeapon"/></param>
		public virtual void Init(SpaceshipController owner)
		{
			_spaceship = owner;

			//if (_ui)
			//{
			//	_ui.Init(this);
			//}
		}

		[Obsolete]
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

		[Obsolete]
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

		/// <summary>
		/// Check each <see cref="IWeaponComponent"/> if their condtions are ok to allow the fireing. 
		/// </summary>
		/// <returns><see langword="true"/> if this <see cref="IWeapon"/> can fire.</returns>
		private bool AllowFire()
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

		/// <summary>
		/// Fetch and Init specified <see cref="IWeaponComponent"/>s of the <see cref="IWeapon"/>. 
		/// </summary>
		/// <param name="weaponComponents">The <see cref="IWeaponComponent"/>s of the <see cref="IWeapon"/></param>
		protected void FetchWeaponComponents(params IWeaponComponent[] weaponComponents)
		{
			foreach (IWeaponComponent component in weaponComponents)
			{
				component.Init(this);
				_weaponComponents.Add(component);
			}
		}

		/// <summary>
		/// Every frames logic of the <see cref="IWeapon"/>
		/// (If the <see cref="IWeapon"/> is currently used)
		/// </summary>
		public virtual void UpdateWeapon()
		{
			foreach (IWeaponComponent weaponComponent in _weaponComponents)
			{
				weaponComponent.UpdateComponent();
			}

			TryFire();
		}

		/// <summary>
		/// Check all the different conditions to fire as Inputs or IWeaponComponent states and call Fire if available.
		/// Called each UpdateWeapon().
		/// </summary>
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

			if (AllowFire())
			{
				if (_weaponFireEventHandler != null)
				{
					_weaponFireEventHandler.Invoke();
				}

				return true;
			}

			return false;
		}

		/// <summary>
		/// Contains the weapon when it have to fire.
		/// </summary>
		/// <param name="muzzle"></param>
		protected abstract void Fire(Transform muzzle);
		#endregion Methods
	}
}