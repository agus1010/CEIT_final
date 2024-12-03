namespace CEIT.Persistence.Helpers
{
	public class PlacePropBehaviourModesManager
	{
		public PlacePropBehaviourIdle idleMode { get; protected set; }
		public PlacePropBehaviourModeGrabbing grabMode { get; protected set; }
		public PlacePropBehaviourSpawn spawnMode { get; protected set; }

		public IPlacePropBehaviourMode currentMode { get; protected set; }
		public IPlacePropBehaviourMode initialMode { get; protected set; }

		private bool currentPrimaryValue = false;
		private bool currentSecondaryValue = false;

		
		public PlacePropBehaviourModesManager(PlacePropBehaviour behaviour, PlacePropInteractionActionsDescriptor actionsDescriptor)
		{
			idleMode = new PlacePropBehaviourIdle(this, behaviour, actionsDescriptor);
			grabMode = new PlacePropBehaviourModeGrabbing(this, behaviour, actionsDescriptor);
			spawnMode = new PlacePropBehaviourSpawn(this, behaviour, actionsDescriptor);
			initialMode = idleMode;
			MoveToMode(initialMode);
		}

		public void MoveToMode(IPlacePropBehaviourMode mode, bool triggeredFromPrimary = false, bool triggeredFromSecondary = false)
		{
			currentMode?.Stop();
			currentMode = mode;
			currentMode.Initialize();
			if (triggeredFromPrimary)
				currentMode.Primary(currentPrimaryValue);
			if (triggeredFromSecondary)
				currentMode.Secondary(currentSecondaryValue);
		}

		public void Stop()
		{
			currentMode?.Stop();
			MoveToMode(initialMode);
		}

		public void OnPrimary(bool newPrimaryValue)
		{
			currentPrimaryValue = newPrimaryValue;
			currentMode.Primary(newPrimaryValue);
		}

		public void OnSecondary(bool newSecondaryValue)
		{
			currentSecondaryValue = newSecondaryValue;
			currentMode.Secondary(newSecondaryValue);
		}
	}
}