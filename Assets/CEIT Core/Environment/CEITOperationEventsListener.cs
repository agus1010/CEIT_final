using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.Environment
{
	public class CEITOperationEventsListener : EventsListener
	{
		public CEITOperationEventsChannel eventsChannel;
		protected override object channel => eventsChannel;

		public UnityEvent OnStarted;
		public UnityEvent OnFinished;
		public UnityEvent<float> OnProgressMade;
		public UnityEvent<System.Exception> OnError;
		public UnityEvent OnCancelled;


		public override void Subscribe()
		{
			eventsChannel.Started.AddListener(onStarted);
			eventsChannel.Finished.AddListener(onFinished);
			eventsChannel.ProgressMade.AddListener(onProgressMade);
			eventsChannel.Error.AddListener(onError);
			eventsChannel.Cancelled.AddListener(onCancelled);
		}

		public override void Unsubscribe()
		{
			eventsChannel.Started.RemoveListener(onStarted);
			eventsChannel.Finished.RemoveListener(onFinished);
			eventsChannel.ProgressMade.RemoveListener(onProgressMade);
			eventsChannel.Error.RemoveListener(onError);
			eventsChannel.Cancelled.RemoveListener(onCancelled);
		}


		private void onStarted()
			=> OnStarted?.Invoke();
		private void onFinished()
			=> OnFinished?.Invoke();
		private void onProgressMade(float value)
			=> OnProgressMade?.Invoke(value);
		private void onError(System.Exception error)
			=> OnError?.Invoke(error);
		private void onCancelled()
			=> OnCancelled?.Invoke();
	}
}