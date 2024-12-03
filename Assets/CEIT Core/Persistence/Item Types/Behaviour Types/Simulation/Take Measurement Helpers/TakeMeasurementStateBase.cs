using UnityEngine;


namespace CEIT.Persistence.Helpers
{
	public class TakeMeasurementStateBase
	{
		protected TakeMeasurementStateMachine stateMachine { get; private set; }
		protected TakeMeasurementBehaviour behaviour { get; private set; }
		protected TakeMeasurementInteractionActionsDescriptor actionsDescriptor { get; private set; }
		protected Player.PlayerPointer pointer => behaviour.pointer;

		public TakeMeasurementStateBase(TakeMeasurementStateMachine stateMachine, TakeMeasurementBehaviour behaviour, TakeMeasurementInteractionActionsDescriptor actionsDescriptor)
		{
			this.stateMachine = stateMachine;
			this.behaviour = behaviour;
			this.actionsDescriptor = actionsDescriptor;
		}

		public virtual void Initialize() => throw new System.NotImplementedException();
		public virtual void Primary(bool newPrimaryValue) => throw new System.NotImplementedException();
		public virtual void Secondary(bool newSecondaryValue) => throw new System.NotImplementedException();
	}
}