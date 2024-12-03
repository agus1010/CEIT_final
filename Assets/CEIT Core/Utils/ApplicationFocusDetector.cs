using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
	public class ApplicationFocusDetector : MonoBehaviour
	{
		public UnityEvent<bool> OnApplicationFocusChanged;
		public UnityEvent OnApplicationFocusGained;
		public UnityEvent OnApplicationFocusLost;

		public void OnApplicationFocus(bool focus)
		{
			OnApplicationFocusChanged?.Invoke(focus);
			if (focus)
				OnApplicationFocusGained?.Invoke();
			else
				OnApplicationFocusLost?.Invoke();
		}
	}
}