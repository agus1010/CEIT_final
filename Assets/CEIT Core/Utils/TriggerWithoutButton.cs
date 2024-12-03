using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
	public class TriggerWithoutButton : MonoBehaviour
	{
		public bool doIt = false;

		public UnityEvent trigger;

		void Update()
		{
			if(doIt)
			{
				doIt = false;
				trigger?.Invoke();
			}
		}
	}
}