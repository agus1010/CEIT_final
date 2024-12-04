using UnityEngine.Events;

using CEIT.Events;


namespace CEITUI.Elements
{
	public class MapSelectorEventsListener : EventsListener
	{
		public MapSelecterEventsChannel eventsChannel;
		protected override object channel => eventsChannel;

		public UnityEvent OnPreviousViewPressed;
		public UnityEvent OnNextViewPressed;


		public override void Subscribe()
		{
			eventsChannel.PreviousViewPressed.AddListener(onPreviousViewPressed);
			eventsChannel.NextViewPressed.AddListener(onPreviousViewPressed);
		}

		public override void Unsubscribe()
		{
			eventsChannel.PreviousViewPressed.RemoveListener(onPreviousViewPressed);
			eventsChannel.NextViewPressed.RemoveListener(onPreviousViewPressed);
		}


		private void onPreviousViewPressed()
			=> OnPreviousViewPressed?.Invoke();

		private void onNextViewPressed()
			=> OnNextViewPressed?.Invoke();
	}
}