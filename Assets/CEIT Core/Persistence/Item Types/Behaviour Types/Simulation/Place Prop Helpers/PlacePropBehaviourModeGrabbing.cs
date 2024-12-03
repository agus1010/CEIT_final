namespace CEIT.Persistence.Helpers
{
	public class PlacePropBehaviourModeGrabbing : PlacePropBehaviourModeBase
	{
		public PlacePropBehaviourModeGrabbing(PlacePropBehaviourModesManager modesManager, PlacePropBehaviour behaviour, PlacePropInteractionActionsDescriptor actionsDescriptor) : base(modesManager, behaviour, actionsDescriptor) { }

		private bool isHoldingAProp => behaviour.isHoldingAProp;

		public override void Initialize()
		{
			if (behaviour.currentPropWasSpawned)
				actionsDescriptor.SetGrabbingSpawnedMode();
			else
				actionsDescriptor.SetGrabbingExistentMode();
		}

		public override void Primary(bool newPrimaryValue)
		{
			if (newPrimaryValue)
				onPrimaryValueIsTrue();
			else
				onPrimaryValueIsFalse();
		}

		public override void Secondary(bool newSecondaryValue)
		{
			if (newSecondaryValue)
				onSecondaryValueIsTrue();
			else
				onSecondaryValueIsFalse();
		}

		public override void Stop()
		{
			behaviour.ReleaseProp();
		}


		private void onPrimaryValueIsTrue()
		{
			if(behaviour.currentPropWasSpawned)
			{
				palette.MovementLocked = true;
				behaviour.GrabProp();
			}
			else
			{
				behaviour.RemoveProp();
			}
		}

		private void onPrimaryValueIsFalse()
		{
			if(isHoldingAProp)
			{
				if(behaviour.currentPropWasSpawned)
				{
					behaviour.ReleaseProp();
				}
			}
			modesManager.MoveToMode(modesManager.idleMode);
		}

		private void onSecondaryValueIsTrue()
		{
			if(isHoldingAProp)
			{
				behaviour.CancelSpawning();
			}
			else
			{
				behaviour.GrabProp();
				palette.MovementLocked = true;
			}
		}

		private void onSecondaryValueIsFalse()
		{
			behaviour.ReleaseProp();
			modesManager.MoveToMode(modesManager.idleMode);
		}
	}
}