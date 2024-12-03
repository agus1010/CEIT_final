using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Player.Events
{
	[CreateAssetMenu(fileName = "New Player Intent Events Channel", menuName = "CEIT/Events/Channels/Core/Player Intent")]
	public class PlayerIntentEventsChannel : ScriptableObject
	{
		[Header("Core Events:")]
		public UnityEvent<Vector2> LookTowards;
		public UnityEvent<Vector2> MoveInDirection;
		public UnityEvent<bool> Jump;
		public UnityEvent<bool> Sprint;
		public UnityEvent<bool> Primary;
		public UnityEvent<bool> Secondary;

		[Header("UI Events:")]
		public UnityEvent<bool> ShowInfo;
		public UnityEvent<bool> PauseMenu;
		public UnityEvent<bool> TabbedMenu;
		public UnityEvent<bool> InteractionWheel;

		[Header("Interactions-Related")]
		public UnityEvent<int> ChangeInteractionInDirection;
		public UnityEvent<int> MoveCurrentInteractionsPalette;


		public void FireLookTowards(Vector2 direction)
			=> LookTowards?.Invoke(direction);
		public void FireMoveInDirection(Vector2 direction)
			=> MoveInDirection?.Invoke(direction);
		public void FireJump(bool value)
			=> Jump?.Invoke(value);
		public void FireSprint(bool value)
			=> Sprint?.Invoke(value);
		public void FirePrimary(bool value)
			=> Primary?.Invoke(value);
		public void FireSecondary(bool value)
			=> Secondary?.Invoke(value);


		public void FireShowInfo(bool value)
			=> ShowInfo?.Invoke(value);
		public void FirePauseMenu(bool value)
			=> PauseMenu?.Invoke(value);
		public void FireTabbedMenu(bool value)
			=> TabbedMenu?.Invoke(value);
		public void FireInteractionWheel(bool value)
			=> InteractionWheel?.Invoke(value);


		public void FireChangeInteractionInDirection(int direction)
			=> ChangeInteractionInDirection?.Invoke(direction);
		public void FireMoveCurrentInteractionsPalette(int direction)
			=> MoveCurrentInteractionsPalette?.Invoke(direction);
	}
}