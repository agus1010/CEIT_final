using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.TimeAndSpace
{
	public class SunRotationSystemEventsListener : EventsListener
	{
		public SunRotationSystemEventsChannel eventsChannel;

		protected override object channel => eventsChannel;

		public UnityEvent<UnityEngine.Quaternion> OnRotationChanged;


		public override void Subscribe()
		{
			eventsChannel.RotationChanged.AddListener(onRotationChanged);
		}

		public override void Unsubscribe()
		{
			eventsChannel.RotationChanged.RemoveListener(onRotationChanged);
		}


		private void onRotationChanged(UnityEngine.Quaternion newRot)
		{
			OnRotationChanged?.Invoke(newRot);
		}
	}
}