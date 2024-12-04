using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEITUI.Elements.Events
{
	public class PlayerStatsTabbedMenuViewEventsListener : EventsListener
	{
		[SerializeField] private PlayerStatsTabbedMenuViewEventsChannel eventsChannel;
		protected override object channel => eventsChannel;

		public UnityEvent<float> OnWalkingSpeedUpdated;
		public UnityEvent<float> OnRunningSpeedUpdated;
		public UnityEvent<int> OnCameraSensitivityUpdated;
		public UnityEvent<float> OnHeightUpdated;
		public UnityEvent<float> OnReachUpdated;


		public override void Subscribe()
		{
			eventsChannel.WalkingSpeedUpdated.AddListener(onWalkingSpeedUpdated);
			eventsChannel.RunningSpeedUpdated.AddListener(onRunningSpeedUpdated);
			eventsChannel.CameraSensitivityUpdated.AddListener(onCameraSensitivityUpdated);
			eventsChannel.HeightUpdated.AddListener(onHeightUpdated);
			eventsChannel.ReachUpdated.AddListener(onReachUpdated);
		}

		public override void Unsubscribe()
		{
			eventsChannel.WalkingSpeedUpdated.RemoveListener(onWalkingSpeedUpdated);
			eventsChannel.RunningSpeedUpdated.RemoveListener(onRunningSpeedUpdated);
			eventsChannel.CameraSensitivityUpdated.RemoveListener(onCameraSensitivityUpdated);
			eventsChannel.HeightUpdated.RemoveListener(onHeightUpdated);
			eventsChannel.ReachUpdated.RemoveListener(onReachUpdated);
		}


		private void onWalkingSpeedUpdated(float value)
			=> OnWalkingSpeedUpdated?.Invoke(value);

		private void onRunningSpeedUpdated(float value)
			=> OnRunningSpeedUpdated?.Invoke(value);

		private void onCameraSensitivityUpdated(int value)
			=> OnCameraSensitivityUpdated?.Invoke(value);

		private void onHeightUpdated(float value)
			=> OnHeightUpdated?.Invoke(value);

		private void onReachUpdated(float value)
			=> OnReachUpdated?.Invoke(value);
	}
}