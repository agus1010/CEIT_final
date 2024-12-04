using UnityEngine.Events;

using CEIT.Events;


namespace CEITUI.Elements
{
	public class MapSelectorViewEventsListener : BaseGameObjectActiveStatusEventsListener
	{
		public MapSelectorViewEventsChannel eventsChannel;
		protected override object channel => eventsChannel;

		public UnityEvent OnContinuePressed;
		public UnityEvent OnGoBackPressed;
		public UnityEvent<System.IO.FileInfo> OnFileSelected;
		public UnityEvent OnLoadingStarted;
		public UnityEvent OnLoadingFinished;
		public UnityEvent OnLoadingCancelled;


		public override void Subscribe()
		{
			base.Subscribe();
			eventsChannel.ContinuePressed.AddListener(onContinuePressed);
			eventsChannel.GoBackPressed.AddListener(onGoBackPressed);
			eventsChannel.FileSelected.AddListener(onFileSelected);
			eventsChannel.LoadingStarted.AddListener(onLoadingStarted);
			eventsChannel.LoadingFinished.AddListener(onLoadingFinished);
			eventsChannel.LoadingCancelled.AddListener(onLoadingCancelled);
		}

		public override void Unsubscribe()
		{
			base.Unsubscribe();
			eventsChannel.ContinuePressed.RemoveListener(onContinuePressed);
			eventsChannel.GoBackPressed.RemoveListener(onGoBackPressed);
			eventsChannel.FileSelected.RemoveListener(onFileSelected);
			eventsChannel.LoadingStarted.RemoveListener(onLoadingStarted);
			eventsChannel.LoadingFinished.RemoveListener(onLoadingFinished);
			eventsChannel.LoadingCancelled.RemoveListener(onLoadingCancelled);
		}

		
		private void onContinuePressed()
			=> OnContinuePressed?.Invoke();
		
		private void onGoBackPressed()
			=> OnGoBackPressed?.Invoke();
		
		private void onFileSelected(System.IO.FileInfo fileInfo)
			=> OnFileSelected?.Invoke(fileInfo);
		
		private void onLoadingStarted()
			=> OnLoadingStarted?.Invoke();
		
		private void onLoadingFinished()
			=> OnLoadingFinished?.Invoke();
		
		private void onLoadingCancelled()
			=> OnLoadingCancelled?.Invoke();
	}
}