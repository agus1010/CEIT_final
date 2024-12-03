using UnityEngine;

using CEIT.Player.Stats;


namespace CEIT.Player
{
	public enum MovementMode
	{
		Continuous,
		Instantaneous
	}

	public class HorizontalMovementProvider : MonoBehaviour
	{
		public PlayerStatsProvider playerStats;
		public CharacterController characterController;

		public MovementMode Mode = MovementMode.Continuous;

		public bool isSprinting { get; set; } = false;
		public float speed { get; protected set; } = 0f;

		protected float targetSpeed => playerStats != null ? (isSprinting ? playerStats.RunningSpeed : playerStats.WalkingSpeed) : (isSprinting ? 6f : 4f);


		public void ToggleSprinting()
			=> isSprinting = !isSprinting;


		public Vector2 CalculateHorizontalMovement(Vector2 horizontalDirection)
		{
			float currentHorizontalSpeed = new Vector3(characterController.velocity.x, 0.0f, characterController.velocity.z).magnitude;
			float desiredSpeed = horizontalDirection != Vector2.zero ? targetSpeed : 0.0f;
			float speedOffset = 0.1f;
			if (currentHorizontalSpeed < desiredSpeed - speedOffset || currentHorizontalSpeed > desiredSpeed + speedOffset)
			{
				if (Mode == MovementMode.Continuous)
				{
					if (horizontalDirection != Vector2.zero && currentHorizontalSpeed < playerStats.WalkingSpeed)
					{
						speed = Mathf.Lerp(currentHorizontalSpeed, desiredSpeed, Time.deltaTime + speedOffset);
					}
					else
					{
						speed = Mathf.Lerp(currentHorizontalSpeed, desiredSpeed, Time.deltaTime * playerStats.SpeedChangeRate);
					}
				}
				else    // Instantaneous
				{
					speed = Mathf.Round(desiredSpeed * 1000f) / 1000f;
				}
			}
			else
			{
				speed = targetSpeed;
			}
			Vector3 result = characterController.transform.right * horizontalDirection.x +
							 characterController.transform.forward * horizontalDirection.y;
			return new Vector2(result.x, result.z).normalized * (Time.deltaTime * speed);
		}
	}
}