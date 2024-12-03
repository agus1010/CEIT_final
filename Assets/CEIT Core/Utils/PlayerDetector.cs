using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
    public class PlayerDetector : MonoBehaviour
    {
		public UnityEvent<GameObject> OnPlayerEnter;
		public UnityEvent OnPlayerExit;

		[Header("Debug:")]
		[SerializeField] private bool debug = false;

		private void OnTriggerEnter(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				if (debug)
					Debug.Log("Player Enter detected.");
				OnPlayerEnter?.Invoke(other.gameObject);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if(other.CompareTag("Player"))
			{
				if (debug)
					Debug.Log("Player Exit detected.");
				OnPlayerExit?.Invoke();
			}
		}
	}
}