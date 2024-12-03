using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Events
{
	public abstract class BaseGameObjectActiveStatusEventsListener : EventsListener
	{
		[Header("Active Status events:")]
		public UnityEvent<bool> OnActiveStatusChanged;
		public UnityEvent OnActiveStatusToggledON;
		public UnityEvent OnActiveStatusToggledOFF;

		public override void Subscribe()
		{
			(channel as GameObjectActiveStatusEventsChannel).ActiveStatusChanged.AddListener(onActiveStatusChanged);
		}

		public override void Unsubscribe()
		{
			(channel as GameObjectActiveStatusEventsChannel).ActiveStatusChanged.RemoveListener(onActiveStatusChanged);
		}


		private void onActiveStatusChanged(bool value)
		{
			OnActiveStatusChanged?.Invoke(value);
			if (value)
				OnActiveStatusToggledON?.Invoke();
			else
				OnActiveStatusToggledOFF?.Invoke();
		}
	}
}