using UnityEngine;

using CEIT.Environment;


namespace CEIT.Loading
{
	public enum JUMP_START_LOADING_SETTINGS
	{
		NONE, START, AWAKE, ON_ENABLE
	}


	public abstract class CEITLoaderBehaviour : MonoBehaviour, ICEITLoader
	{
		[Header("Settings:")]
		[SerializeField] private JUMP_START_LOADING_SETTINGS jumpStartLoadingSetting = JUMP_START_LOADING_SETTINGS.NONE;

		[Header("Environment Variables:")]
		public SavingAndLoadingRuntimeVariables runtimeVars;

		[Header("Events:")]
		public CEITOperationEventsChannel eventsChannel;

		[Header("Debug:")]
		[SerializeField] protected bool debug = false;


		public virtual void PerformLoadingOperation()
		{
			if (debug)
				print($"STARTED LOADING operation in component: {transform.parent.name}.{name}->{this.GetType()}");
			eventsChannel?.FireStarted();
			try
			{
				performLoad();
				eventsChannel?.FireFinished();
				if (debug)
					print($"FINISHED LOADING operation in component: {transform.parent.name}.{name}->{this.GetType()}");
			}
			catch (System.Exception e)
			{
				if (debug)
					Debug.LogWarning($"LOADING operation CANCELLED in component {transform.parent.name}.{name}->{this.GetType()}, because of error: {e}");
				eventsChannel?.FireError(e);
				eventsChannel?.FireCancelled();
			}
		}

		protected abstract void performLoad();


		private void OnEnable()
		{
			if (jumpStartLoadingSetting == JUMP_START_LOADING_SETTINGS.ON_ENABLE)
				PerformLoadingOperation();
		}

		private void Awake()
		{
			if (jumpStartLoadingSetting == JUMP_START_LOADING_SETTINGS.AWAKE)
				PerformLoadingOperation();
		}

		private void Start()
		{
			if (jumpStartLoadingSetting == JUMP_START_LOADING_SETTINGS.START)
				PerformLoadingOperation();
		}
	}
}