using UnityEngine;
using UnityEngine.Events;


namespace CEIT.SavingAndLoading
{
	[CreateAssetMenu(fileName = "New Saving and Loading System Events Channel", menuName = "CEIT/Events/Channels/Core/Saving and Loading System")]
	public class SavingAndLoadingSystemEventsChannel : ScriptableObject
	{
		public UnityEvent<string> LoadOperationRequested;
		public UnityEvent SaveOperationRequested;

		public void FireLoadOperationRequested(string mapName)
			=> LoadOperationRequested?.Invoke(mapName);

		public void FireSaveOperationRequested()
			=> SaveOperationRequested?.Invoke();
	}
}