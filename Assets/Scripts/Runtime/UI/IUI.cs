namespace UI
{
	using UnityEngine;

	public abstract class IUI : MonoBehaviour
	{
		#region Methods
		public abstract void Init(Object owner);

		public void Activate()
		{
			gameObject.SetActive(true);
		}

		public void Deactivate()
		{
			gameObject.SetActive(false);
		}
		#endregion Methods
	}
}
