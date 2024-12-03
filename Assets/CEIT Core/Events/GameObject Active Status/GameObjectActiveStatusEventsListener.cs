using UnityEngine;


namespace CEIT.Events
{
	public class GameObjectActiveStatusEventsListener : BaseGameObjectActiveStatusEventsListener
	{
		[SerializeField] private GameObjectActiveStatusEventsChannel eventsChannel;

		protected override object channel => eventsChannel;
	}
}