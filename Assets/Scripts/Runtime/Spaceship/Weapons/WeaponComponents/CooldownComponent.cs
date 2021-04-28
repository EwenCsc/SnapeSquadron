namespace Spaceship
{
	using System;
	using UnityEngine;
	using UnityEngine.UI;

	[Serializable]
	public class CooldownComponent : IWeaponComponent
	{
		#region Events
		private Action _updateCooldownEventHandler = null;
		public event Action UpdateCooldown
		{
			add
			{
				_updateCooldownEventHandler -= value;
				_updateCooldownEventHandler += value;
			}
			remove
			{
				_updateCooldownEventHandler -= value;
			}
		}
		#endregion Events

		#region Fields
		[SerializeField] private float _cooldown = 0.5f;
		//[SerializeField] private Sprite _cooldownBadge = null;
		[SerializeField] private Sprite _cooldownBadgeFull = null;

		private Image _cooldownFullImage = null;
		private float _currentCooldown = 0.0f;
		#endregion Fields

		#region Properties
		public float Cooldown { get => _cooldown; }
		public float CurrentCooldown { get => _currentCooldown; }
		#endregion Properties

		#region Methods
		public override void GenerateBadge(Image logoPrefab, Transform parent)
		{
			//Image cooldownImage = GameObject.Instantiate(logoPrefab, parent.position, Quaternion.identity, parent);
			//cooldownImage.sprite = _cooldownBadge;

			_cooldownFullImage = GameObject.Instantiate(logoPrefab, parent.position, Quaternion.identity, parent);
			_cooldownFullImage.sprite = _cooldownBadgeFull;
			_cooldownFullImage.color = new Color(1, 1, 1, 0.7f);
			_cooldownFullImage.type = Image.Type.Filled;
			_cooldownFullImage.fillMethod = Image.FillMethod.Radial360;
			_cooldownFullImage.fillAmount = 0.0f;

			UpdateCooldown += OnUpdateCooldown;
		}

		public float GetCooldownLevel()
		{
			if (_cooldown == 0)
			{
				return 999;
			}

			return _currentCooldown / _cooldown;
		}

		#region Interface
		public override void UpdateComponent()
		{
			if (_currentCooldown > 0)
			{
				_currentCooldown -= Time.deltaTime;
				_currentCooldown = Mathf.Clamp(_currentCooldown, 0.0f, _cooldown);

				if (_updateCooldownEventHandler != null)
				{
					_updateCooldownEventHandler.Invoke();
				}
			}
		}

		public override bool AllowFire()
		{

			return _currentCooldown == 0.0f;
		}

		protected override void OnFire()
		{

			_currentCooldown = _cooldown;
		}

		private void OnUpdateCooldown()
		{
			_cooldownFullImage.fillAmount = GetCooldownLevel();
		}
		#endregion Interface
		#endregion Methods
	}
}
