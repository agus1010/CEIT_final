namespace CEIT.Persistence.Helpers
{
	public class TakeMeasurementStateMachine
	{
		public TakeMeasurementStateBase currentState { get; private set; }
		
		public TakeMeasurementStateIdle idleState { get; private set; }
		public TakeMeasurementStateMeasuring measuringState { get; private set; }


		public TakeMeasurementStateMachine(TakeMeasurementBehaviour behaviour, TakeMeasurementInteractionActionsDescriptor actionsDescriptor)
		{
			idleState = new TakeMeasurementStateIdle(this, behaviour, actionsDescriptor);
			measuringState = new TakeMeasurementStateMeasuring(this,behaviour, actionsDescriptor);
			MoveToState(idleState);
		}

		public void Primary(bool newPrimaryValue)
		{
			currentState.Primary(newPrimaryValue);
		}

		public void Secondary(bool newSecondaryValue)
		{
			currentState.Secondary(newSecondaryValue);
		}

		public void MoveToState(TakeMeasurementStateBase state)
		{
			currentState = state;
			currentState.Initialize();
		}
	}
}