using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.TimeAndSpace
{
	public class TimeSystemEventsListener : EventsListener
	{
		public TimeSystemEventsChannel eventsChannel;

		public UnityEvent<System.TimeSpan> OnTimeValueChanged;

		protected override object channel => eventsChannel;


		public override void Subscribe()
		{
			eventsChannel?.TimeValueChanged.AddListener(onTimeValueChanged);
		}

		public override void Unsubscribe()
		{
			eventsChannel?.TimeValueChanged.RemoveListener(onTimeValueChanged);
		}


		private void onTimeValueChanged(System.TimeSpan timeSpan)
			=> OnTimeValueChanged?.Invoke(timeSpan);
	}
}