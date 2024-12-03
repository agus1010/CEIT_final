namespace CEIT.Persistence.Helpers
{
	public class TakeMeasurementStateMeasuring : TakeMeasurementStateBase
	{
		public TakeMeasurementStateMeasuring(TakeMeasurementStateMachine stateMachine, TakeMeasurementBehaviour behaviour, TakeMeasurementInteractionActionsDescriptor actionsDescriptor) : base(stateMachine, behaviour, actionsDescriptor) { }


		public override void Initialize()
		{
			actionsDescriptor.SetMeasuringMode();
		}

		public override void Primary(bool newPrimaryValue)
		{
			if (!newPrimaryValue)
			{
				behaviour.StopMeasuring();
				stateMachine.MoveToState(stateMachine.idleState);
			}
		}

		public override void Secondary(bool newSecondaryValue)
		{
			if (newSecondaryValue)
			{
				behaviour.CancelMeasurement();
				stateMachine.MoveToState(stateMachine.idleState);
			}
		}
	}
}