using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
    public class AutomaticResetter : MonoBehaviour
    {
		public bool ResetOnStart = false;
		public bool ResetOnEnable = true;
		public bool ResetOnReset = false;

        public UnityEvent OnResettingTriggered;

		private void OnEnable()
		{
			if (ResetOnEnable)
				triggerReset();
		}

		private void Start()
		{
			if (ResetOnStart)
				triggerReset();
		}

		private void Reset()
		{
			if(ResetOnReset)
				triggerReset();
		}


		private void triggerReset()
			=> OnResettingTriggered?.Invoke();
	}
}