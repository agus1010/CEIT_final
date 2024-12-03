namespace CEIT.Persistence.Helpers
{
	public class TakeMeasurementStateIdle : TakeMeasurementStateBase
	{
		public TakeMeasurementStateIdle(TakeMeasurementStateMachine stateMachine, TakeMeasurementBehaviour behaviour, TakeMeasurementInteractionActionsDescriptor actionsDescriptor) : base(stateMachine, behaviour, actionsDescriptor) { }


		public override void Initialize()
		{
			actionsDescriptor.SetIdleMode();
		}

		public override void Primary(bool newPrimaryValue)
		{
			if (newPrimaryValue && behaviour.StartMeasuring())
			{
				stateMachine.MoveToState(stateMachine.measuringState);
			}
		}

		public override void Secondary(bool newSecondaryValue)
		{
			if (newSecondaryValue)
			{
				behaviour.TryDestroyMeasurement();
			}
		}
	}
}