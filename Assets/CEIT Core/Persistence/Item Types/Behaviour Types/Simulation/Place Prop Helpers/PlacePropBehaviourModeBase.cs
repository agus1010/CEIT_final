using CEIT.Player;


namespace CEIT.Persistence.Helpers
{
	public class PlacePropBehaviourModeBase : IPlacePropBehaviourMode
	{
		public PlacePropBehaviourModesManager modesManager { get; protected set; }
		public PlacePropBehaviour behaviour { get; protected set; }
		public PlacePropInteractionActionsDescriptor actionsDescriptor { get; protected set; }
		public PlayerPointer pointer => behaviour.pointer;
		public ItemPalette palette => behaviour.palette;

		public PlacePropBehaviourModeBase(PlacePropBehaviourModesManager modesManager, PlacePropBehaviour behaviour, PlacePropInteractionActionsDescriptor actionsDescriptor)
		{
			this.modesManager = modesManager;
			this.behaviour = behaviour;
			this.actionsDescriptor = actionsDescriptor;
		}

		public virtual void Initialize() { }
		public virtual void Primary(bool newPrimaryValue) { }
		public virtual void Secondary(bool newSecondaryValue) { }
		public virtual void Stop() { }
	}
}