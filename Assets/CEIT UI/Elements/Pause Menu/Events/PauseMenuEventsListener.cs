using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEITUI.Assets.Events
{
    public class PauseMenuEventsListener : BaseGameObjectActiveStatusEventsListener
    {
        [SerializeField] private PauseMenuEventsChannel eventsChannel;

		[Space(5)]
		public UnityEvent OnResumeSelected;
		public UnityEvent OnControlsSelected;
		public UnityEvent OnManualSelected;
		public UnityEvent OnReturnToMapSelectionSelected;
		public UnityEvent OnQuitApplicationSelected;
		public UnityEvent OnMuteSoundSelected;

		protected override object channel => eventsChannel;


		public override void Subscribe()
		{
			base.Subscribe();
			eventsChannel.ResumeSelected.AddListener(onResumeSelected);
			eventsChannel.ControlsSelected.AddListener(onControlsSelected);
			eventsChannel.ManualSelected.AddListener(onManualSelected);
			eventsChannel.ReturnToMapSelectionSelected.AddListener(onReturnToMapSelectionSelected);
			eventsChannel.QuitApplicationSelected.AddListener(onQuitAppSelected);
			eventsChannel.MuteSoundSelected.AddListener(onMuteSoundSelected);
		}

		public override void Unsubscribe()
		{
			base.Unsubscribe();
			eventsChannel.ResumeSelected.RemoveListener(onResumeSelected);
			eventsChannel.ControlsSelected.RemoveListener(onControlsSelected);
			eventsChannel.ManualSelected.RemoveListener(onManualSelected);
			eventsChannel.ReturnToMapSelectionSelected.RemoveListener(onReturnToMapSelectionSelected);
			eventsChannel.QuitApplicationSelected.RemoveListener(onQuitAppSelected);
			eventsChannel.MuteSoundSelected.RemoveListener(onMuteSoundSelected);
		}


		private void onResumeSelected()
			=> OnResumeSelected?.Invoke();

		private void onControlsSelected()
			=> OnControlsSelected?.Invoke();

		private void onManualSelected()
			=> OnManualSelected?.Invoke();

		private void onReturnToMapSelectionSelected()
			=> OnReturnToMapSelectionSelected?.Invoke();

		private void onQuitAppSelected()
			=> OnQuitApplicationSelected?.Invoke();

		private void onMuteSoundSelected()
			=> OnMuteSoundSelected?.Invoke();
	}
}