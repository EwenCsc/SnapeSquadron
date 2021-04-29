/// <summary>
/// <see cref="IPoolable"/> Elements are objects which can be stored in <see cref="Pool"/>
/// </summary>
public interface IPoolable
{
	/// <summary>
	/// Return <see langword="true"/> if the element is Free aka is Deactivate and can be reuse.
	/// </summary>
	bool IsFree();

	/// <summary>
	/// Reset the element's value at their origins.
	/// </summary>
	void Reset();

	/// <summary>
	/// Reactive the element. 
	/// So the element is no more Free and cannot be pick in a <see cref="Pool"/>.
	/// </summary>
	void Activate();

	/// <summary>
	/// Deactive the element. 
	/// So the element is no more Free and can be pick in a <see cref="Pool"/>.
	/// </summary>
	void Deactivate();
}