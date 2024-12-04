using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEITUI.Elements
{
	[CreateAssetMenu(fileName = "New Map Selector View Events Channel", menuName = "CEIT/Events/Channels/UI/Map Selector View")]
	public class MapSelectorViewEventsChannel : GameObjectActiveStatusEventsChannel
	{
		public UnityEvent ContinuePressed;
		public UnityEvent GoBackPressed;
		public UnityEvent<System.IO.FileInfo> FileSelected;
		public UnityEvent LoadingStarted;
		public UnityEvent LoadingFinished;
		public UnityEvent LoadingCancelled;


		public void FireContinuePressed()
			=> ContinuePressed?.Invoke();
		public void FireGoBackPressed()
			=> GoBackPressed?.Invoke();
		public void FireFileSelected(System.IO.FileInfo fileInfo)
			=> FileSelected?.Invoke(fileInfo);
		public void FireLoadingStarted()
			=> LoadingStarted?.Invoke();
		public void FireLoadingFinished()
			=> LoadingFinished?.Invoke();
		public void FireLoadingCancelled()
			=> LoadingCancelled?.Invoke();
	}
}