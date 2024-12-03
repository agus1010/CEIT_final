using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
	public class UnityMomentsEvents : MonoBehaviour
	{
		public UnityEvent onEnable;
		public UnityEvent onDisable;
		public UnityEvent awake;
		public UnityEvent start;
		public UnityEvent update;
		public UnityEvent fixedUpdate;
		public UnityEvent onApplicationQuit;

		private void OnEnable()
		{
			onEnable?.Invoke();
		}

		private void OnDisable()
		{
			onDisable?.Invoke();
		}

		private void Awake()
		{
			awake?.Invoke();
		}

		private void Start()
		{
			start?.Invoke();
		}

		private void Update()
		{
			if(update.GetPersistentEventCount() > 0)
				update?.Invoke();
		}

		private void FixedUpdate()
		{
			if(fixedUpdate.GetPersistentEventCount() > 0)
				fixedUpdate?.Invoke();
		}

		private void OnApplicationQuit()
		{
			onApplicationQuit?.Invoke();
		}
	}
}