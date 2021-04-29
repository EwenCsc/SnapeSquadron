namespace Spaceship
{
	using System;
	using UI;
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// <see cref="IWeaponComponent"/> which manage ammunitions logic.
	/// </summary>
	[Serializable]
	public class AmmunitionComponent : IWeaponComponent
	{
		#region Events
		private Action _ammunitionUpdatingEventHandler = null;
		public event Action AmmunitionUpdating
		{
			add
			{
				_ammunitionUpdatingEventHandler -= value;
				_ammunitionUpdatingEventHandler += value;
			}
			remove
			{
				_ammunitionUpdatingEventHandler -= value;
			}
		}

		#endregion Events

		#region Fields
		[SerializeField] private uint _ammunitionCapacity = 0;
		[SerializeField] private Sprite _emptyBadge = null;
		[SerializeField] private Text _text = null; // Temp
		
		private Text _label; // Temp

		private uint _currentAmmunitions = 0;
		#endregion Fields

		#region Methods
		/// <inheritdoc/>
		public override void Init(IWeapon owner)
		{
			base.Init(owner);

			_currentAmmunitions = _ammunitionCapacity;
			AmmunitionUpdating += OnAmmunitionUpdate;
		}

		/// <inheritdoc/>
		public override bool AllowFire()
		{
			return _currentAmmunitions > 0;
		}

		/// <summary>
		/// Fullfill the loader.
		/// </summary>
		public void Reload()
		{
			_currentAmmunitions = _ammunitionCapacity;

			if (_ammunitionUpdatingEventHandler != null)
			{
				_ammunitionUpdatingEventHandler.Invoke();
			}
		}

		/// <summary>
		/// Add one ammunition to the loader.
		/// </summary>
		public void ReloadOneAmmunition()
		{
			_currentAmmunitions++;

			if (_ammunitionUpdatingEventHandler != null)
			{
				_ammunitionUpdatingEventHandler.Invoke();
			}
		}

		/// <inheritdoc/>
		public override void GenerateUI(Image logoPrefab, Transform parent)
		{
			Image badge = Badge.Generate(_emptyBadge, logoPrefab, parent);

			_label = GameObject.Instantiate(_text, badge.transform.position, Quaternion.identity, badge.transform);
			_label.text = _currentAmmunitions.ToString();
			_label.rectTransform.sizeDelta = new Vector2(30, 30);

			//Label label = new Label("0");
			//label.style.width = 30;
			//label.style.height = 30;
			//label.transform.position = badge.transform.position;
		}

		/// <inheritdoc/>
		protected override void OnFire()
		{
			_currentAmmunitions--;

			if (_ammunitionUpdatingEventHandler != null)
			{
				_ammunitionUpdatingEventHandler.Invoke();
			}
		}

		/// <summary>
		/// Logic when the loader is updated.
		/// </summary>
		private void OnAmmunitionUpdate()
		{
			_label.text = _currentAmmunitions.ToString();
		}
		#endregion Methods
	}
}
