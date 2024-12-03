using CEIT.Interactables;

namespace CEIT.Persistence.Helpers
{
	public class PlacePropBehaviourIdle : PlacePropBehaviourModeBase
	{
		public PlacePropBehaviourIdle(PlacePropBehaviourModesManager modesManager, PlacePropBehaviour behaviour, PlacePropInteractionActionsDescriptor actionsDescriptor) : base(modesManager, behaviour, actionsDescriptor) { }

		public override void Initialize()
		{
			palette.MovementLocked = false;
			actionsDescriptor.SetIdleMode();
		}

		public override void Primary(bool newPrimaryValue)
		{
			if (newPrimaryValue)
			{
				if(!pointer.IsLookingAtGraphics)
					modesManager.MoveToMode(mode: modesManager.spawnMode, triggeredFromPrimary: true);
			}
		}

		public override void Secondary(bool newSecondaryValue)
		{
			if (newSecondaryValue)
			{
				if (!pointer.IsLookingAtGraphics && pointer.ClosestTarget.TryGetComponent(out PropBehaviour pb))
					modesManager.MoveToMode(mode: modesManager.grabMode, triggeredFromSecondary: true);
			}
		}
	}
}