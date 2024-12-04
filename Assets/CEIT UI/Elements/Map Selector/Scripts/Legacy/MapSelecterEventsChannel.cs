using UnityEngine;
using UnityEngine.Events;


namespace CEITUI.Elements
{
	[CreateAssetMenu(fileName = "New Map Selector Events Channel", menuName = "CEIT/Events/Channels/UI/Map Selector")]
	public class MapSelecterEventsChannel : ScriptableObject
	{
		public UnityEvent PreviousViewPressed;
		public UnityEvent NextViewPressed;


		public void FirePreviousViewPressed()
		{
			PreviousViewPressed?.Invoke();
		}

		public void FireNextViewPressed()
		{
			NextViewPressed?.Invoke();
		}
	}
}