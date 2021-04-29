namespace UI
{
	using UnityEngine;
	using UnityEngine.UI;

	/// <summary>
	/// Little badge in the right corner of an UI element.
	/// </summary>
	public class Badge
	{
		/// <summary>
		/// [PRETTY HARDCODED]
		/// </summary>
		public static Image Generate(Sprite sprite, Image imagePrefab, Transform parent)
		{
			Image image = Object.Instantiate(imagePrefab, parent);

			image.rectTransform.localPosition = new Vector2(25, -25);
			image.rectTransform.sizeDelta = new Vector2(50, 50);
			image.sprite = sprite;

			return image;
		}
	}
}
