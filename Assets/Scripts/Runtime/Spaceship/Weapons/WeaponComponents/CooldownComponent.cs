namespace Spaceship
{
	using System;
	using UI;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Manage the logic the Cooldown of the <see cref="IWeapon"/>
	/// </summary>
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
		[SerializeField] private Sprite _cooldownBadge = null;
		[SerializeField] private Sprite _cooldownBadgeFull = null;

		private Image _cooldownFullImage = null;
		private float _currentCooldown = 0.0f;
		#endregion Fields

		#region Properties
		public float Cooldown { get => _cooldown; }
		public float CurrentCooldown { get => _currentCooldown; }
		#endregion Properties

		#region Methods
		/// <inheritdoc/>
		public override void GenerateUI(Image logoPrefab, Transform parent)
		{
			Image cooldownImage = Badge.Generate(_cooldownBadge, logoPrefab, parent);

			_cooldownFullImage = Badge.Generate(_cooldownBadgeFull, logoPrefab, parent);
			_cooldownFullImage.transform.SetParent(cooldownImage.transform);
			_cooldownFullImage.sprite = _cooldownBadgeFull;
			_cooldownFullImage.color = new Color(1, 1, 1, 0.7f);
			_cooldownFullImage.type = Image.Type.Filled;
			_cooldownFullImage.fillMethod = Image.FillMethod.Radial360;
			_cooldownFullImage.fillAmount = 0.0f;

			UpdateCooldown += OnUpdateCooldown;
		}

		/// <summary>
		/// return a <see cref="float"/> between 0 and 1 matching with cooldown porcent.
		/// </summary>
		public float GetCooldownLevel()
		{
			if (_cooldown == 0)
			{
				return 999;
			}

			return _currentCooldown / _cooldown;
		}

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public override bool AllowFire()
		{

			return _currentCooldown == 0.0f;
		}

		/// <inheritdoc/>
		protected override void OnFire()
		{

			_currentCooldown = _cooldown;
		}

		/// <summary>
		/// Logic when the _currentCooldown is modified.
		/// </summary>
		private void OnUpdateCooldown()
		{
			_cooldownFullImage.fillAmount = GetCooldownLevel();
		}
		#endregion Methods
	}
}
