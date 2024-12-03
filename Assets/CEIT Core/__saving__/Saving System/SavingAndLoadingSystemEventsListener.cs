using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.SavingAndLoading
{
	public class SavingAndLoadingSystemEventsListener : EventsListener
	{
		public SavingAndLoadingSystemEventsChannel eventsChannel;

		public UnityEvent<string> OnLoadOperationRequested;
		public UnityEvent OnSaveOperationRequested;

		protected override object channel => eventsChannel;


		public override void Subscribe()
		{
			eventsChannel?.LoadOperationRequested.AddListener(onLoadOperationRequested);
			eventsChannel?.SaveOperationRequested.AddListener(onSaveOperationRequested);
		}

		public override void Unsubscribe()
		{
			eventsChannel?.LoadOperationRequested.RemoveListener(onLoadOperationRequested);
			eventsChannel?.SaveOperationRequested.RemoveListener(onSaveOperationRequested);
		}


		private void onLoadOperationRequested(string mapName)
			=> OnLoadOperationRequested?.Invoke(mapName);

		private void onSaveOperationRequested()
			=> OnSaveOperationRequested?.Invoke();
	}
}