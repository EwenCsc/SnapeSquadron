public interface IPoolable
{
	bool IsFree();

	void Reset();

	void Activate();

	void Deactivate();
}