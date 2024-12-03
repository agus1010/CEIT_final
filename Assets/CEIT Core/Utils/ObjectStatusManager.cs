using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.Utils
{
	public class ObjectStatusManager : MonoBehaviour
	{
		[SerializeField] private bool initialValue = true;
		public bool statusIsActive 
		{
			get => initialValue; 
			private set => initialValue = value; 
		}

		public bool lockCurrentState { get; set; } = false;

		[Header("Channel:")]
		public GameObjectActiveStatusEventsChannel eventsChannel;

		[Header("Local Events:")]
		public UnityEvent<bool> OnObjectStatusChanged;
		public UnityEvent OnObjectStatusChangedToON;
		public UnityEvent OnObjectStatusChangedToOFF;

		public void Set(bool value)
		{
			if(!lockCurrentState && value != statusIsActive)
			{
				statusIsActive = value;
				setActiveAndfireEvents();
			}
		}

		public void Toggle()
			=> Set(!statusIsActive);
		
		public void ToggleOnTrue(bool value)
		{
			if (value)
				Toggle();
		}



		private void setActiveAndfireEvents()
		{
			OnObjectStatusChanged?.Invoke(statusIsActive);
			if (statusIsActive)
				OnObjectStatusChangedToON?.Invoke();
			else
				OnObjectStatusChangedToOFF?.Invoke();
			eventsChannel?.FireActiveStatusChanged(statusIsActive);
		}
	}
}