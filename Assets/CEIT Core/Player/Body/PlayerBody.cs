using UnityEngine;


namespace CEIT.Player
{
	public class PlayerBody : MonoBehaviour
	{
		[Header("Scene References:")]
		public Stats.PlayerStatsProvider Stats;
		public CharacterController characterController;
		public HorizontalMovementProvider horizontalMovementProvider;

		[Header("Config:")]
		[SerializeField] private float gravity = Physics.gravity.y;
		[SerializeField] private bool locked = false;

		public bool IsSprinting => horizontalMovementProvider.isSprinting;
		public float Gravity { get => gravity; set => gravity = value; }
		public bool Locked { get => locked; set => locked = value; }
		public float RotationVelocity { get; private set; }
		public float VerticalVelocity { get; private set; }
		
		protected float targetSpeed => Stats != null ? (IsSprinting? Stats.RunningSpeed : Stats.WalkingSpeed) : (IsSprinting? 6f : 4f);

		private Vector2 horizontalMovementInput = Vector2.zero;
		private float verticalMovementInput = 0f;
		private float _jumpTimeoutDelta;
		private float _fallTimeoutDelta;


		public void Jump(bool value)
		{
			if (Locked) return;
			verticalMovementInput = 1;
		}

		public void Sprint(bool value)
		{
			horizontalMovementProvider.isSprinting = value;
		}

		public void MoveTowards(Vector2 horizontalDirection)
		{
			if (Locked) return;
			horizontalMovementInput = horizontalDirection;
		}

		public void StopMoving()
		{
			horizontalMovementInput = Vector2.zero;
			horizontalMovementProvider.isSprinting = false;
		}

		public void Togglelock()
		{
			Locked = !Locked;
			if(Locked)
			{
				horizontalMovementInput = Vector2.zero;
				verticalMovementInput = 0f;
			}
		}


		private Vector3 m_playerMovement = Vector3.zero;
		private void Update()
		{
			if (!Locked)
			{
				var horMove = horizontalMovementProvider.CalculateHorizontalMovement(horizontalMovementInput);
				m_playerMovement.x = horMove.x;
				m_playerMovement.y = calcVerticalMovement() * Time.deltaTime;
				m_playerMovement.z = horMove.y;
			}
			else
			{
				m_playerMovement = Vector3.zero;
			}
			characterController.Move(m_playerMovement);
		}

		private void OnApplicationFocus(bool focus)
		{
			if (!focus)
				StopMoving();
		}

		private float calcVerticalMovement()
		{
			if (characterController.isGrounded)
			{
				_fallTimeoutDelta = Stats.FallTimeout;
				if (VerticalVelocity < 0.0f)
				{
					VerticalVelocity = -2f;
				}
				if (verticalMovementInput != 0.0f && _jumpTimeoutDelta <= 0.0f)
				{
					VerticalVelocity = Mathf.Sqrt(Stats.MaxJumpHeight * -2f * gravity);
				}
				if (_jumpTimeoutDelta >= 0.0f)
				{
					_jumpTimeoutDelta -= Time.deltaTime;
				}
			}
			else
			{
				_jumpTimeoutDelta = Stats.JumpTimeout;
				if (_fallTimeoutDelta >= 0.0f)
				{
					_fallTimeoutDelta -= Time.deltaTime;
				}
				verticalMovementInput = 0f;
			}
			if (VerticalVelocity < Stats.TerminalVelocity)
			{
				VerticalVelocity += gravity * Time.deltaTime;
			}
			return VerticalVelocity;
		}
	}
}