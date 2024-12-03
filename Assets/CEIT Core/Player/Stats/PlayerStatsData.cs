using UnityEngine;


namespace CEIT.Player.Stats
{
	[CreateAssetMenu(fileName = "New Player Stats Data", menuName = "CEIT/Player/Stats Data")]
	public class PlayerStatsData : ScriptableObject, IPlayerStats
	{
		[SerializeField] private float height;
		[SerializeField] private float walkingSpeed;
		[SerializeField] private float runningSpeed;
		[SerializeField] private float terminalVelocity;
		[SerializeField] private float fallTimeout;
		[SerializeField] private float speedChangeRate;
		[SerializeField] private float maxJumpHeight;
		[SerializeField] private float jumpTimeout;
		[SerializeField] private float terminalVelocityVertical;
		[SerializeField] private float maxReachDistance;
		[SerializeField] private int cameraSensitivity;

		public float Height { get => height; set => height = value; }
		public float WalkingSpeed { get => walkingSpeed; set => walkingSpeed = value; }
		public float RunningSpeed { get => runningSpeed; set => runningSpeed = value; }
		public float TerminalVelocity { get => terminalVelocity; set => terminalVelocity = value; }
		public float FallTimeout { get => fallTimeout; set => fallTimeout = value; }
		public float SpeedChangeRate { get => speedChangeRate; set => speedChangeRate = value; }
		public float MaxJumpHeight { get => maxJumpHeight; set => maxJumpHeight = value; }
		public float JumpTimeout { get => jumpTimeout; set => jumpTimeout = value; }
		public float TerminalVelocityVertical { get => terminalVelocityVertical; set => terminalVelocityVertical = value; }
		public float MaxReachDistance { get => maxReachDistance; set => maxReachDistance = value; }
		public int CameraSensitivity { get => cameraSensitivity; set => cameraSensitivity  = value; }
	}
}