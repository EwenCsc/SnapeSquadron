namespace UI
{
	using Spaceship;
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.UI;

	public class SpaceshipUI : MonoBehaviour
	{
		#region Fields
		[SerializeField] private Transform _logosPanelAnchor = null;
		[SerializeField] private Image _logoPrefab = null;
		[SerializeField] private Sprite _selectionForegroundSprite = null;
		[SerializeField] private float _spacing = 10.0f;

		private Dictionary<IWeapon, Image> _weaponsLogos = null;
		private IWeapon _currentWeapon = null;

		private Image _selectionForeground = null;
		#endregion Fields

		#region Methods
		public void Init(SpaceshipController spaceship)
		{
			_weaponsLogos = new Dictionary<IWeapon, Image>();

			IWeapon[] weapons = spaceship.Weapons;
			float width = _logoPrefab.rectTransform.rect.width + _spacing;
			float y = _logosPanelAnchor.transform.position.y;

			for (int i = 0; i < weapons.Length; i++)
			{
				IWeapon weapon = weapons[i];
				float x = _logosPanelAnchor.position.x;
				x += (i + 0.5f - (weapons.Length / 2.0f)) * width;

				Vector2 position = new Vector2(x, y);
				Image image = Instantiate(_logoPrefab, position, Quaternion.identity, _logosPanelAnchor);

				image.sprite = weapon.WeaponLogo;

				_weaponsLogos.Add(weapon, image);

				List<IWeaponComponent> components = weapon.WeaponComponents;

				foreach(IWeaponComponent component in components)
				{
					component.GenerateBadge(_logoPrefab, image.transform);
				}
			}

			_selectionForeground = Instantiate(_logoPrefab, _logosPanelAnchor.position, Quaternion.identity, _logosPanelAnchor);
			_selectionForeground.sprite = _selectionForegroundSprite;

			spaceship.WeaponSet += OnWeaponSet;
		}

		public void OnWeaponSet(IWeapon weapon)
		{
			if (weapon != _currentWeapon)
			{
				_currentWeapon = weapon;
				_selectionForeground.rectTransform.position = _weaponsLogos[weapon].rectTransform.position;
			}
		}
		#endregion Methods
	}
}