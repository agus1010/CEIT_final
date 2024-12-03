using UnityEngine;


namespace CEIT.Player
{
	public class PlayerSight : MonoBehaviour
	{
		[Header("Stats:")]
		public Stats.PlayerStatsProvider Stats;

		[Header("Events Channel:")]
		public Events.PlayerIntentEventsChannel intentsChannel;

		[Header("Player GO Parent:")]
		public GameObject PlayerParent;

		[Header("Cinemachine:")]
		[Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
		[SerializeField] private GameObject cinemachineCameraTarget;

		public float BottomClamp = -89f;
		public float TopClamp = 89f;

		private float _rotationVelocity;
		private float _cinemachineTargetPitch;

		private const float _threshold = 0.01f;

		private Transform fixedLookingTarget = null;

		private bool locked = false;
		public bool Locked
		{
			get => locked;
			set
			{
				if (!value)
					fixedLookingTarget = null;
				locked = value;
			}
		}
		public Vector2 LookDirection { get; set; } = Vector2.zero;

		
		public void LookAt(Transform target)
			=> LookAt(target.position);

		public void LookAt(Vector3 targetPosition)
			=> PlayerParent.transform.LookAt(targetPosition);

		public void LockSightTowards(Transform target)
		{
			Locked = true;
			fixedLookingTarget = target;
		}

		public void LookTowards(Vector2 direction)
		{
			if (direction.sqrMagnitude < _threshold)
				return;
			applyCameraRotation(direction);
		}

		public void ToggleLock()
			=> Locked = !Locked;


		private void Start()
		{
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void LateUpdate()
		{
			if (!Locked)
			{
				if (LookDirection.sqrMagnitude >= _threshold)
				{
					applyCameraRotation(LookDirection);
					LookDirection = Vector2.zero;
				}
			}
			else
			{
				if(fixedLookingTarget != null)
				{
					LookAt(fixedLookingTarget);
				}
			}
		}


		private void applyCameraRotation(Vector2 direction)
		{
			_cinemachineTargetPitch += direction.y * Stats.CameraSensitivity * Time.deltaTime;		//* Stats.RotationSpeed //* deltaTimeMultiplier;
			_rotationVelocity = direction.x * Stats.CameraSensitivity * Time.deltaTime;				//Stats.RotationSpeed//* deltaTimeMultiplier;

			// clamp our pitch rotation
			_cinemachineTargetPitch = MathUtils.ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

			// Update Cinemachine camera target pitch
			cinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

			// rotate this gameObject left and right
			PlayerParent.transform.Rotate(Vector3.up * _rotationVelocity);
		}
	}
}