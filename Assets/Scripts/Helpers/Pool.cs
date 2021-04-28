using System.Collections.Generic;
using UnityEngine;

public class Pool<TElement> where TElement : MonoBehaviour, IPoolable
{
	#region Fields
	private TElement _firstElement = null;
	private List<TElement> _poolableElements = new List<TElement>();
	#endregion Fields

	#region Constructor
	public Pool(TElement prefab)
	{
		_firstElement = prefab;
	}
	#endregion Constructor

	#region Methods
	public TElement GetElement()
	{
		foreach (TElement element in _poolableElements)
		{
			if (element.IsFree() == true)
			{
				return element;
			}
		}

		TElement newElement = Object.Instantiate(_firstElement);
		_poolableElements.Add(newElement);

		return newElement;
	}
	#endregion Methods
}