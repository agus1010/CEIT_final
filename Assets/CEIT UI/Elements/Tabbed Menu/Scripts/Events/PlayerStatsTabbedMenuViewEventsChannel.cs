using UnityEngine;
using UnityEngine.Events;


namespace CEITUI.Elements.Events
{
	[CreateAssetMenu(fileName = "New Player Stats (Tabbed Menu View) Events Channel", menuName = "CEIT/Events/Channels/UI/Player Stats View")]
	public class PlayerStatsTabbedMenuViewEventsChannel : ScriptableObject
	{
		public UnityEvent<float> WalkingSpeedUpdated;
		public UnityEvent<float> RunningSpeedUpdated;
		public UnityEvent<int> CameraSensitivityUpdated;
		public UnityEvent<float> HeightUpdated;
		public UnityEvent<float> ReachUpdated;

		public void FireWalkingSpeedUpdated(float value)
			=> WalkingSpeedUpdated?.Invoke(value);

		public void FireRunningSpeedUpdated(float value)
			=> RunningSpeedUpdated?.Invoke(value);

		public void FireCameraSensitivityUpdated(float value)
			=> FireCameraSensitivityUpdated((int)value);

		public void FireCameraSensitivityUpdated(int value)
			=> CameraSensitivityUpdated?.Invoke(value);

		public void FireHeightUpdated(float value)
			=> HeightUpdated?.Invoke(value);

		public void FireReachUpdated(float value)
			=> ReachUpdated?.Invoke(value);
	}
}