namespace Spaceship
{
	using System;
	using UnityEngine;

	[Serializable]
	public class HeatComponent : IWeaponComponent
	{
		#region Events
		private Action _updateHeatEventHandler = null;
		public event Action UpdateHeat
		{
			add
			{
				_updateHeatEventHandler -= value;
				_updateHeatEventHandler += value;
			}
			remove
			{
				_updateHeatEventHandler -= value;
			}
		}
		#endregion Events

		#region Fields
		[SerializeField] private float _maxOverheat = 20.0f;
		[SerializeField] private float _overheatFactor = 3.0f;
		[SerializeField] private float _coolingFactor = 2.0f;

		private float _currentHeating = 0.0f;
		#endregion Fields

		#region Methods
		public override void UpdateComponent()
		{
			float lastValue = _currentHeating;
			_currentHeating = Mathf.Clamp(_currentHeating - Time.deltaTime * _coolingFactor, 0.0f, _maxOverheat);

			if (lastValue != _currentHeating && _updateHeatEventHandler != null)
			{
				_updateHeatEventHandler.Invoke();
			}
		}

		public override bool AllowFire()
		{
			return _currentHeating < _maxOverheat;
		}

		protected override void OnFire()
		{
			float lastValue = _currentHeating;
			_currentHeating = Mathf.Clamp(_currentHeating + Time.deltaTime * _overheatFactor, 0.0f, _maxOverheat);

			if (lastValue != _currentHeating && _updateHeatEventHandler != null)
			{
				_updateHeatEventHandler.Invoke();
			}
		}
		#endregion Methods
	}
}
