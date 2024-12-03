namespace CEIT.Persistence.Helpers
{
	public class PlacePropBehaviourSpawn : PlacePropBehaviourModeBase
	{
		public PlacePropBehaviourSpawn(PlacePropBehaviourModesManager modesManager, PlacePropBehaviour behaviour, PlacePropInteractionActionsDescriptor actionsDescriptor) : base(modesManager, behaviour, actionsDescriptor) { }

		public override void Initialize()
		{
			behaviour.SpawnCurrentProp();
			modesManager.MoveToMode(mode: modesManager.grabMode, triggeredFromPrimary: true);
		}
	}
}