using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.Player.Events
{
	public class PlayerIntentEventsListener : EventsListener
	{
		[SerializeField] private PlayerIntentEventsChannel eventsChannel;
		
		[Header("Core Events:")]
		public UnityEvent<Vector2> OnLookTowards;
		public UnityEvent<Vector2> OnMoveInDirection;
		public UnityEvent<bool> OnJump;
		public UnityEvent<bool> OnSprint;
		public UnityEvent<bool> OnPrimary;
		public UnityEvent<bool> OnSecondary;

		[Header("UI Events:")]
		public UnityEvent<bool> OnShowInfo;
		public UnityEvent<bool> OnPauseMenu;
		public UnityEvent<bool> OnTabbedMenu;
		public UnityEvent<bool> OnInteractionWheel;

		[Header("Interactions-Related:")]
		public UnityEvent<int> OnChangeInteractionInDirection;
		public UnityEvent<int> OnMoveCurrentInteractionsPalette;


		protected override object channel => eventsChannel;


		public override void Subscribe()
		{
			eventsChannel.MoveInDirection.AddListener(onMoveInDirection);
			eventsChannel.Jump.AddListener(onJump);
			eventsChannel.Sprint.AddListener(onSprint);
			eventsChannel.LookTowards.AddListener(onLookTowards);
			eventsChannel.Primary.AddListener(onPrimary);
			eventsChannel.Secondary.AddListener(onSecondary);
			eventsChannel.ShowInfo.AddListener(onShowInfo);
			eventsChannel.PauseMenu.AddListener(onPauseMenu);
			eventsChannel.TabbedMenu.AddListener(onTabbedMenu);
			eventsChannel.InteractionWheel.AddListener(onInteractionWheel);
			eventsChannel.ChangeInteractionInDirection.AddListener(onChangeInteractionInDirection);
			eventsChannel.MoveCurrentInteractionsPalette.AddListener(onMoveCurrentInteractionsPalette);
		}

		public override void Unsubscribe()
		{
			eventsChannel.MoveInDirection.RemoveListener(onMoveInDirection);
			eventsChannel.Jump.RemoveListener(onJump);
			eventsChannel.Sprint.RemoveListener(onSprint);
			eventsChannel.LookTowards.RemoveListener(onLookTowards);
			eventsChannel.Primary.RemoveListener(onPrimary);
			eventsChannel.Secondary.RemoveListener(onSecondary);
			eventsChannel.ShowInfo.RemoveListener(onShowInfo);
			eventsChannel.PauseMenu.RemoveListener(onPauseMenu);
			eventsChannel.TabbedMenu.RemoveListener(onTabbedMenu);
			eventsChannel.InteractionWheel.RemoveListener(onInteractionWheel);
			eventsChannel.ChangeInteractionInDirection.RemoveListener(onChangeInteractionInDirection);
			eventsChannel.MoveCurrentInteractionsPalette.RemoveListener(onMoveCurrentInteractionsPalette);
		}


		private void onLookTowards(Vector2 newDirection)
			=> OnLookTowards?.Invoke(newDirection);
		private void onMoveInDirection(Vector2 newDirection)
			=> OnMoveInDirection?.Invoke(newDirection);
		private void onJump(bool newValue)
			=> OnJump?.Invoke(newValue);
		private void onSprint(bool newValue)
			=> OnSprint?.Invoke(newValue);
		private void onPrimary(bool newValue)
			=> OnPrimary?.Invoke(newValue);
		private void onSecondary(bool newValue)
			=> OnSecondary?.Invoke(newValue);

		private void onShowInfo(bool newValue)
			=> OnShowInfo?.Invoke(newValue);
		private void onPauseMenu(bool newValue)
			=> OnPauseMenu?.Invoke(newValue);
		private void onTabbedMenu(bool newValue)
			=> OnTabbedMenu?.Invoke(newValue);
		private void onInteractionWheel(bool newValue)
			=> OnInteractionWheel?.Invoke(newValue);
		
		private void onChangeInteractionInDirection(int direction)
			=> OnChangeInteractionInDirection?.Invoke(direction);
		private void onMoveCurrentInteractionsPalette(int direction)
			=> OnMoveCurrentInteractionsPalette?.Invoke(direction);
	}




}