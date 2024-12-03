namespace CEIT.Persistence.Helpers
{
	public interface IPlacePropBehaviourMode
	{
		public PlacePropBehaviourModesManager modesManager { get; }
		public PlacePropBehaviour behaviour { get; }
		public Player.PlayerPointer pointer { get; }
		public ItemPalette palette { get; }

		public void Initialize();
		public void Primary(bool newPrimaryValue);
		public void Secondary(bool newSecondaryValue);
		public void Stop();
	}
}