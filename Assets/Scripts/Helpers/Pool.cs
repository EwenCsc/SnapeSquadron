using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Pool is a List which contains <see cref="IPoolable"/> elements.
/// It allow to not use too much memory by reuse element which are not active instead of destroying them and instantiate a new one later.
/// </summary>
public class Pool<TElement> where TElement : MonoBehaviour, IPoolable
{
	#region Fields
	private TElement _firstElement = null;
	private List<TElement> _poolableElements = new List<TElement>();
	#endregion Fields

	#region Constructor
	/// <summary>
	/// Initiate the container.
	/// </summary>
	/// <param name="prefab">It's the prefab which will be clone when we need a new one</param>
	public Pool(TElement prefab)
	{
		_firstElement = prefab;
	}
	#endregion Constructor

	#region Methods
	/// <summary>
	/// Return a <see cref="TElement"/> object if one of them is available in the container otherwise instantiate a new one.
	/// </summary>
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