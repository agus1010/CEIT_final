using UnityEngine;
using UnityEngine.Events;


namespace CEIT.TimeAndSpace
{
	[CreateAssetMenu(fileName = "New Time System Events", menuName = "CEIT/Events/Channels/Core/Time System")]
	public class TimeSystemEventsChannel : ScriptableObject
	{
		public UnityEvent<System.TimeSpan> TimeValueChanged;

		public void FireTimeValueChanged(System.TimeSpan timeSpan)
			=> TimeValueChanged?.Invoke(timeSpan);
	}
}