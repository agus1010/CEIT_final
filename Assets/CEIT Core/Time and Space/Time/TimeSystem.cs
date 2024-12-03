using UnityEngine;


namespace CEIT.TimeAndSpace
{
	[CreateAssetMenu(fileName = "New Time System", menuName = "CEIT/Systems/Time System")]
	public class TimeSystem : ScriptableObject
	{
		[SerializeField] private SunRotationSystem sunRotationSystem;

		public TimeSystemEventsChannel eventsChannel;

		public const int SECONDS_IN_A_DAY = 24 * 60 * 60;
		public const float SECONDS_TO_DEGREES_RATIO = 360f / SECONDS_IN_A_DAY;
		public const float DEGREES_TO_SECONDS_RATIO = SECONDS_IN_A_DAY / 360f;

		[Range(0, 23)] public int startingHour = 9;

		public System.TimeSpan now => getNow();
		public int hours => now.Hours;
		public int minutes => now.Minutes;
		public int seconds => now.Seconds;
		public int totalSeconds => (int)now.TotalSeconds;


		public void SetTime(float seconds)
			=> SetTime((int)seconds);

		public void SetTime(int seconds)
		{
			float angle = secondsToAngle(seconds);
			sunRotationSystem.SetAngleInAxis(angle, Vector3.right);
			eventsChannel.FireTimeValueChanged(now);
		}


		// https://forum.unity.com/threads/day-and-night-cycle-rotation-problem.367485/
		private int angleToSeconds(float angle)
			=> Mathf.FloorToInt(DEGREES_TO_SECONDS_RATIO * (angle + 90f));

		private System.TimeSpan getNow()
			=> System.TimeSpan.FromSeconds(angleToSeconds(sunRotationSystem.xRotation));

		private int offsetSeconds(int seconds)
		{
			if (seconds < 21600)
				return seconds + 64800;
			return seconds - 21600;
		}

		private float secondsToAngle(int seconds)
		{
			int offsettedSeconds = offsetSeconds(seconds);
			return SECONDS_TO_DEGREES_RATIO * offsettedSeconds;
		}
	}
}