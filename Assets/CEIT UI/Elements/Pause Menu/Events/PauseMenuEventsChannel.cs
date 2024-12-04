using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEITUI.Assets.Events
{
	[CreateAssetMenu(fileName = "New Pause Menu Events Channel", menuName = "CEIT/Events/Channels/UI/Pause Menu")]
	public class PauseMenuEventsChannel : GameObjectActiveStatusEventsChannel
	{
		public UnityEvent ResumeSelected;
		public UnityEvent ControlsSelected;
		public UnityEvent ManualSelected;
		public UnityEvent ReturnToMapSelectionSelected;
		public UnityEvent QuitApplicationSelected;
		public UnityEvent MuteSoundSelected;


		public void FireResumeSelected()
			=> ResumeSelected?.Invoke();

		public void FireControlsSelected()
			=> ControlsSelected?.Invoke();

		public void FireManualSelected()
			=> ManualSelected?.Invoke();

		public void FireReturnToMapSelectionSelected()
			=> ReturnToMapSelectionSelected?.Invoke();

		public void FireQuitApplicationSelected()
			=> QuitApplicationSelected?.Invoke();

		public void FireMuteSoundSelected()
			=> MuteSoundSelected?.Invoke();
	}
}