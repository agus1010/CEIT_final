using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.Assets.Events
{
	public class InteractionPaletteEventsListener : EventsListener
	{
		[SerializeField] private ItemPaletteEventsChannel eventsChannel;
		[Space(5)]
		public UnityEvent<Persistence.Interaction> OnCurrentInteractionChanged;
		public UnityEvent<int> OnSequentialMovementPerformed;

		protected override object channel => eventsChannel;

		public override void Subscribe()
		{
			eventsChannel.CurrentItemChanged.AddListener(onCurrentInteractionChanged);
			eventsChannel.SequentialMovementPerformed.AddListener(onSequentialMovementPerformed);
		}

		public override void Unsubscribe()
		{
			eventsChannel.CurrentItemChanged.RemoveListener(onCurrentInteractionChanged);
			eventsChannel.SequentialMovementPerformed.RemoveListener(onSequentialMovementPerformed);
		}


		private void onCurrentInteractionChanged(Persistence.Item item)
		{
			var interaction = item as Persistence.Interaction;
			OnCurrentInteractionChanged?.Invoke(interaction);
		}

		private void onSequentialMovementPerformed(int positions)
			=> OnSequentialMovementPerformed?.Invoke(positions);
	}
}