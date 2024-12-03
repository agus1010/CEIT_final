using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Environment
{
	[CreateAssetMenu(fileName = "New CEIT Operation Events Channel", menuName = "CEIT/Events/Channels/Core/CEIT Operation")]
	public class CEITOperationEventsChannel : ScriptableObject
	{
		public UnityEvent Started;
		public UnityEvent Finished;
		public UnityEvent<float> ProgressMade;
		public UnityEvent<System.Exception> Error;
		public UnityEvent Cancelled;

		public void FireStarted()
			=> Started?.Invoke();
		public void FireFinished()
			=> Finished?.Invoke();
		public void FireProgressMade(float value)
			=> ProgressMade?.Invoke(value);
		public void FireError(System.Exception error)
			=> Error?.Invoke(error);
		public void FireCancelled()
			=> Cancelled?.Invoke();
	}
}