using UnityEngine;


namespace CEIT.Player.Stats
{
	public class PlayerStatsProvider : MonoBehaviour, IPlayerStats
	{
		[Header("Config:")]
		[SerializeField] private bool _useDefaultStats = false;
		public bool useDefaultStats { get => _useDefaultStats; set => _useDefaultStats = value; }

		[Header("References:")]
		[SerializeField] private PlayerStatsData defaultStats;
		[SerializeField] private PlayerStatsData modifiedStats;
		[SerializeField] private PlayerHeightProvider heightProvider;

		private PlayerStatsData consumedData
			=> useDefaultStats ? defaultStats : modifiedStats;


		public float WalkingSpeed => consumedData.WalkingSpeed;
		public float RunningSpeed => consumedData.RunningSpeed;
		public float TerminalVelocity => consumedData.TerminalVelocity;
		public float FallTimeout => consumedData.FallTimeout;
		public float SpeedChangeRate => consumedData.SpeedChangeRate;
		public float Height => consumedData.Height;
		public float MaxJumpHeight => consumedData.MaxJumpHeight;
		public float JumpTimeout => consumedData.JumpTimeout;
		public float TerminalVelocityVertical => consumedData.TerminalVelocityVertical;
		public float MaxReachDistance => consumedData.MaxReachDistance;
		public int CameraSensitivity => consumedData.CameraSensitivity;
	}
}