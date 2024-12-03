using CEIT.Environment;
using UnityEngine.Events;


namespace CEIT.Loading
{
	public class SimulationLoadingOperationEventsChannel : CEITOperationEventsChannel
	{
		public UnityEvent ModelLoadingStarted;
		public UnityEvent CustomPropsLoadingStarted;
		public UnityEvent CEITPropsLoadingStarted;
		public UnityEvent CEITSurfacesLoadingStarted;
		
		public UnityEvent ModelLoadingFinished;
		public UnityEvent CustomPropsLoadingFinished;
		public UnityEvent CEITPropsLoadingFinished;
		public UnityEvent CEITSurfacesLoadingFinished;
	}
}