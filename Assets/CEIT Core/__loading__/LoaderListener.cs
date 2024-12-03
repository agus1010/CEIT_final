using CEIT.Environment;


namespace CEIT.Loading
{
	public class LoaderListener
	{
		public string name { get; private set; }
		public float progress { get; private set; }
		public CEIT_OPERATION_STATUS status { get; private set; }
		public System.Exception errorRaised { get; private set; }

		private CEITOperationEventsChannel hearedChannel;

		public LoaderListener(CEITOperationEventsChannel hearedChannel)
		{
			this.hearedChannel = hearedChannel;
			name = hearedChannel.name;
			status = CEIT_OPERATION_STATUS.IDLE;
			progress = 0f;
			errorRaised = null;
			subscribe();
		}

		public void Dispose()
			=> unsubscribe();


		private void subscribe()
		{
			hearedChannel.ProgressMade.AddListener(opMadeProgress);
			hearedChannel.Finished.AddListener(opFinished);
			hearedChannel.Cancelled.AddListener(opCancelled);
			hearedChannel.Error.AddListener(opCrashed);
		}

		private void unsubscribe()
		{
			hearedChannel.ProgressMade.RemoveListener(opMadeProgress);
			hearedChannel.Finished.RemoveListener(opFinished);
			hearedChannel.Cancelled.RemoveListener(opCancelled);
			hearedChannel.Error.RemoveListener(opCrashed);
		}

		private void opMadeProgress(float amount)
			=> progress += amount;

		private void opFinished()
			=> updateToDoneStatus(CEIT_OPERATION_STATUS.FINISHED);

		private void opCancelled()
			=> updateToDoneStatus(CEIT_OPERATION_STATUS.CANCELLED);

		private void opCrashed(System.Exception exception)
		{
			errorRaised = exception;
			updateToDoneStatus(CEIT_OPERATION_STATUS.CRASHED);
		}

		private void updateToDoneStatus(CEIT_OPERATION_STATUS newDoneStatus)
		{
			status = newDoneStatus;
			unsubscribe();
		}
	}
}