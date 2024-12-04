using UnityEngine;


namespace CEITUI.Elements
{
	public class IconSetProvider : MonoBehaviour, IPlayerIntentsIconsProvider
	{
		public CEIT.__booting__.HardwareTypeProvider hardwareTypeProvider;

		[Header("Icon Sets:")]
		public PlayerIntentIconSet mouseAndKeyboardIconSet;
		public PlayerIntentIconSet vrIconSet;

		public PlayerIntentIconSet currentIconSet => _currentIconSet;

		public Sprite primaryActionIcon => currentIconSet.primaryActionIcon;
		public Sprite secondaryActionIcon => currentIconSet.secondaryActionIcon;
		public Sprite changeInteractionUpIcon => currentIconSet.changeInteractionUpIcon;
		public Sprite changeInteractionDownIcon => currentIconSet.changeInteractionDownIcon;
		public Sprite changeInteractionValueUpIcon => currentIconSet.changeInteractionValueUpIcon;
		public Sprite changeInteractionValueDownIcon => currentIconSet.changeInteractionValueDownIcon;
		public Sprite pauseMenu => currentIconSet.pauseMenu;
		public Sprite tabbedMenu => currentIconSet.tabbedMenu;
		public Sprite interactionsWheel => currentIconSet.interactionsWheel;
		public Sprite showInfo => currentIconSet.showInfo;

		[Header("Debug:")]
		[SerializeField] private PlayerIntentIconSet _currentIconSet;


		public Sprite GetIconForIntent(CEIT.Player.PlayerIntents playerIntent)
			=> currentIconSet.GetIconForIntent(playerIntent);


		private void Awake()
		{
			switch (hardwareTypeProvider.hardwareType)
			{
				case CEIT.__booting__.HardwareType.PC:
					_currentIconSet = mouseAndKeyboardIconSet;
					break;
				case CEIT.__booting__.HardwareType.VR:
					_currentIconSet = vrIconSet;
					break;
				default:
					_currentIconSet = mouseAndKeyboardIconSet;
					break;
			}
		}
	}
}