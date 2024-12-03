using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
	public class ActiveStatusDetector : MonoBehaviour
	{
		public UnityEvent<bool> OnActiveStatusChanged;
		public UnityEvent OnActiveStatusON;
		public UnityEvent OnActiveStatusOFF;

		public void OnEnable()
		{
			OnActiveStatusChanged?.Invoke(true);
			OnActiveStatusON?.Invoke();
		}

		public void OnDisable()
		{
			OnActiveStatusChanged?.Invoke(false);
			OnActiveStatusOFF?.Invoke();
		}
	}
}