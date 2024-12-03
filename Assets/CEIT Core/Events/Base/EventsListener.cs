using UnityEngine;

namespace CEIT.Events
{
	public abstract class EventsListener : MonoBehaviour
	{
		protected abstract object channel { get; }

		public bool HasEventsSource => channel != null;


		protected void Awake()
		{
			if (HasEventsSource)
			{
				Subscribe();
			}
		}

		protected void OnApplicationQuit()
		{
			if (HasEventsSource)
			{
				Unsubscribe();
			}
		}



		public abstract void Subscribe();
		public abstract void Unsubscribe();
	}
}