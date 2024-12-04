using UnityEngine;


namespace CEITUI.Elements
{
	[CreateAssetMenu(fileName = "New Icon Set", menuName = "CEIT/UI/Icon Set")]
	public class PlayerIntentIconSet : ScriptableObject, IPlayerIntentsIconsProvider
	{
		[Header("Config:")]
		[SerializeField] private Sprite _defaultIcon;

		[Header("Values:")]
		[SerializeField] private Sprite _primaryActionIcon;
		[SerializeField] private Sprite _secondaryActionIcon;
		[SerializeField] private Sprite _changeInteractionUpIcon;
		[SerializeField] private Sprite _changeInteractionDownIcon;
		[SerializeField] private Sprite _changeInteractionValueUpIcon;
		[SerializeField] private Sprite _changeInteractionValueDownIcon;
		[SerializeField] private Sprite _pauseMenu;
		[SerializeField] private Sprite _tabbedMenu;
		[SerializeField] private Sprite _interactionsWheel;
		[SerializeField] private Sprite _showInfo;


		public Sprite primaryActionIcon => _primaryActionIcon != null ? _primaryActionIcon : _defaultIcon;
		public Sprite secondaryActionIcon => _secondaryActionIcon != null ? _secondaryActionIcon : _defaultIcon;
		public Sprite changeInteractionUpIcon => _changeInteractionUpIcon != null ? _changeInteractionUpIcon : _defaultIcon;
		public Sprite changeInteractionDownIcon => _changeInteractionDownIcon != null ? _changeInteractionDownIcon : _defaultIcon;
		public Sprite changeInteractionValueUpIcon => _changeInteractionValueUpIcon != null ? _changeInteractionValueUpIcon : _defaultIcon;
		public Sprite changeInteractionValueDownIcon => _changeInteractionValueDownIcon != null ? _changeInteractionValueDownIcon : _defaultIcon;
		public Sprite pauseMenu => _pauseMenu != null ? _pauseMenu : _defaultIcon;
		public Sprite tabbedMenu => _tabbedMenu != null ? _tabbedMenu : _defaultIcon;
		public Sprite interactionsWheel => _interactionsWheel != null ? _interactionsWheel : _defaultIcon;
		public Sprite showInfo => _showInfo != null ? _showInfo : _defaultIcon;


		public Sprite GetIconForIntent(CEIT.Player.PlayerIntents playerIntent)
		{
			switch (playerIntent)
			{
				case CEIT.Player.PlayerIntents.PRIMARY:
					return primaryActionIcon;
				case CEIT.Player.PlayerIntents.SECONDARY:
					return secondaryActionIcon;
				case CEIT.Player.PlayerIntents.CHANGE_INTERACTION_UP:
					return changeInteractionUpIcon;
				case CEIT.Player.PlayerIntents.CHANGE_INTERACTION_DOWN:
					return changeInteractionDownIcon;
				case CEIT.Player.PlayerIntents.CHANGE_INTERACTION_VALUE_UP:
					return changeInteractionValueUpIcon;
				case CEIT.Player.PlayerIntents.CHANGE_INTERACTION_VALUE_DOWN:
					return changeInteractionValueDownIcon;
				case CEIT.Player.PlayerIntents.PAUSE_MENU:
					return pauseMenu;
				case CEIT.Player.PlayerIntents.TABBED_MENU:
					return tabbedMenu;
				case CEIT.Player.PlayerIntents.INTERACTIONS_WHEEL:
					return interactionsWheel;
				case CEIT.Player.PlayerIntents.SHOW_INFO:
					return showInfo;
				default:
					return _defaultIcon;
			}
		}
	}
}