namespace UI
{
	using System;
	using UnityEngine;

	[Obsolete]
	public abstract class IUI : MonoBehaviour
	{
		#region Methods
		public abstract void Init(UnityEngine.Object owner);

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
