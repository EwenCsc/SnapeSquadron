namespace Spaceship
{
	using System;
	using UI;
	using UnityEngine;

	[RequireComponent(typeof(Rigidbody))]
	public class SpaceshipController : MonoBehaviour
	{
		#region Events
		private Action<IWeapon> _weaponSetEventHandler = null;
		public event Action<IWeapon> WeaponSet
		{
			add
			{
				_weaponSetEventHandler -= value;
				_weaponSetEventHandler += value;
			}
			remove
			{
				_weaponSetEventHandler -= value;
			}
		}
		#endregion Events

		#region Fields
		[SerializeField] private bool _isPlayer = false;

		[Header("Speed Parameters : ")]
		[SerializeField] private float _speed = 25.0f;
		[SerializeField, Range(0.1f, 10.0f)] private float _maxAccelerationTime = 1.25f;
		[SerializeField] private float _slowdownForce = 2.0f;
		[SerializeField] private AnimationCurve _accelerationCurve = null;
		[SerializeField] private AnimationCurve _slowdownCurve = null;

		[Header("Rotation Parameters : ")]
		[SerializeField] private float _yawRotationSpeed = 0.0005f;
		[SerializeField] private float _pitchRotationSpeed = 0.0005f;
		[SerializeField] private float _rollRotationSpeed = 0.25f;

		[Header("Camera : ")]
		[SerializeField] private float _cameraSmooth = 4.0f;

		private float _currentAccelerationTime = 0.0f;

		private Rigidbody _rigidbody = null;

		private Vector2 _mouseOffset = Vector3.zero;
		private Quaternion _rotation = Quaternion.identity;

		private IWeapon[] _weapons = null;
		private IWeapon _currentWeapon = null;
		private int _currentWeaponIndex = 0;

		private SpaceshipUI _spaceshipUI = null;
		#endregion Fields

		#region Properties
		public bool IsPlayer { get => _isPlayer; }
		public float Speed { get => _speed; }
		public Rigidbody Rigidbody { get => _rigidbody; }
		public IWeapon[] Weapons { get => _weapons; }
		#endregion Properties

		#region Methods
		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;

			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.useGravity = false;

			_weapons = GetComponentsInChildren<IWeapon>();

			foreach (IWeapon weapon in _weapons)
			{
				weapon.Init(this);
			}

			_spaceshipUI = GetComponentInChildren<SpaceshipUI>();

			if (_spaceshipUI != null)
			{
				_spaceshipUI.Init(this);
			}

			SetWeapon(_currentWeaponIndex);
		}

		private void SetWeapon(int weaponIndex)
		{
			IWeapon weapon = _weapons[weaponIndex];

			if (weapon != _currentWeapon)
			{
				_currentWeapon = weapon;
				if (_weaponSetEventHandler != null)
				{
					_weaponSetEventHandler.Invoke(_currentWeapon);
				}
			}
		}

		private void FixedUpdate()
		{
			UpdateControls();
			UpdateWeapon();
			SwitchWeaponControl();
		}

		private void UpdateControls()
		{
			// Rotation
			_mouseOffset.x = Mathf.Lerp(_mouseOffset.x, Input.GetAxis("MouseX") * _yawRotationSpeed, Time.deltaTime * _cameraSmooth);
			_mouseOffset.y = Mathf.Lerp(_mouseOffset.y, -Input.GetAxis("MouseY") * _pitchRotationSpeed, Time.deltaTime * _cameraSmooth);

			Quaternion currentRotation = Quaternion.Euler(_mouseOffset.y, _mouseOffset.x, -Input.GetAxisRaw("Roll") * _rollRotationSpeed * Time.deltaTime);
			_rotation = _rotation * currentRotation;
			transform.rotation = _rotation;

			// Acceleration
			if (Input.GetAxisRaw("Accelerate") != 0)
			{
				if (Input.GetAxisRaw("Accelerate") > 0)
				{
					_currentAccelerationTime += Time.deltaTime;
				}
				else if (Input.GetAxisRaw("Accelerate") < 0)
				{
					_currentAccelerationTime -= Time.deltaTime * _slowdownForce;
				}
			}
			else
			{
				_currentAccelerationTime -= Time.deltaTime;
			}

			_currentAccelerationTime = Mathf.Clamp(_currentAccelerationTime, 0.0f, _maxAccelerationTime);

			if (Input.GetAxisRaw("Accelerate") > 0)
			{
				_rigidbody.velocity = transform.forward * _accelerationCurve.Evaluate(_currentAccelerationTime / _maxAccelerationTime) * _speed;
			}
			else
			{
				_rigidbody.velocity = transform.forward * _slowdownCurve.Evaluate(1 - (_currentAccelerationTime / _maxAccelerationTime)) * _speed;
			}
		}

		private void UpdateWeapon()
		{
			_currentWeapon.UpdateWeapon();
		}

		private void SwitchWeaponControl()
		{
			int scroll = (int)Input.GetAxisRaw("Switch Weapon") / 1000;
			int lastWeaponIndex = _currentWeaponIndex;

			// Bof essaye de trouver la formule pour faire ça en une ligne
			if (scroll != 0)
			{
				_currentWeaponIndex += scroll;

				if (_currentWeaponIndex < 0)
				{
					_currentWeaponIndex = _weapons.Length - 1;
				}
				else if (_currentWeaponIndex >= _weapons.Length)
				{
					_currentWeaponIndex = 0;
				}
			}

			if (lastWeaponIndex != _currentWeaponIndex)
			{
				SetWeapon(_currentWeaponIndex);
			}
		}
		#endregion Methods
	}
}