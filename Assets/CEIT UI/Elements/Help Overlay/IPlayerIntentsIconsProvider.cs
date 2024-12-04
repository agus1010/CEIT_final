using UnityEngine;


namespace CEITUI.Elements
{
	public interface IPlayerIntentsIconsProvider
	{
		public Sprite primaryActionIcon { get; }
		public Sprite secondaryActionIcon { get; }
		public Sprite changeInteractionUpIcon { get; }
		public Sprite changeInteractionDownIcon { get; }
		public Sprite changeInteractionValueUpIcon { get; }
		public Sprite changeInteractionValueDownIcon { get; }
		public Sprite pauseMenu { get; }
		public Sprite tabbedMenu { get; }
		public Sprite interactionsWheel { get; }
		public Sprite showInfo { get; }

		public Sprite GetIconForIntent(CEIT.Player.PlayerIntents playerIntent);
	}
}