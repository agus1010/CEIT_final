namespace CEIT.Player.Stats
{
	public interface IHeightProvider
	{
		public float Height { get; }
	}

	public interface IMovementStats
	{
		public float WalkingSpeed { get; }
		public float RunningSpeed { get; }
		public float TerminalVelocity { get; }
		public float FallTimeout { get; }
		public float SpeedChangeRate { get; }
		public float MaxJumpHeight { get; }
		public float JumpTimeout { get; }
		public float TerminalVelocityVertical { get; }
	}
	
	public interface IAccesibilityStats
	{
		public float MaxReachDistance { get; }
		public int CameraSensitivity { get; }
	}

    public interface IPlayerStats : IHeightProvider, IMovementStats, IAccesibilityStats { }
}