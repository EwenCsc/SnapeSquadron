namespace Spaceship
{
	using UnityEngine;

	public class CameraController : MonoBehaviour
	{
		#region Fields
		[SerializeField] private float _minFov = 60.0f;
		[SerializeField] private float _maxFov = 90.0f;

		private Camera _camera = null;
		private SpaceshipController _controller = null;
		#endregion Fields

		#region Methods
		private void Start()
		{
			_camera = GetComponentInChildren<Camera>();

			if (_camera == null)
			{
				Debug.LogError("CameraController::No Camera founded");
			}

			_controller = GetComponent<SpaceshipController>();

			if (_controller == null)
			{
				Debug.LogError("CameraController::No Spaceship::Controller founded");
			}
		}

		private void Update()
		{
			_camera.fieldOfView = Mathf.Lerp(_minFov, _maxFov, _controller.Rigidbody.velocity.magnitude / _controller.Speed);
		}
		#endregion Methods
	}
}
