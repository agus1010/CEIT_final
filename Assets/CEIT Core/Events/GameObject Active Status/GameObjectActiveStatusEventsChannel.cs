using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Events
{
	[CreateAssetMenu(fileName = "New Active Status Events Channel", menuName = "CEIT/Events/Channels/Utils/GameObject Active Status")]
	public class GameObjectActiveStatusEventsChannel : ScriptableObject
	{
		public UnityEvent<bool> ActiveStatusChanged;

		public void FireActiveStatusChanged(bool value)
			=> ActiveStatusChanged?.Invoke(value);
	}
}