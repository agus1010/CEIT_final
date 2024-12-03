using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.Assets.Events
{
	public class ItemPaletteEventsListener : EventsListener
	{
		[SerializeField] private ItemPaletteEventsChannel eventsChannel;
		[Space(5)]
		public UnityEvent<Persistence.Item> OnCurrentItemChanged;
		public UnityEvent<int> OnCurrentIndexChanged;
		public UnityEvent<int> OnSequentialMovementPerformed;

		protected override object channel => eventsChannel;


		public override void Subscribe()
		{
			eventsChannel.CurrentItemChanged.AddListener(onCurrentItemChanged);
			eventsChannel.CurrentIndexChanged.AddListener(onCurrentIndexChanged);
			eventsChannel.SequentialMovementPerformed.AddListener(onSequentialMovementPerformed);
		}

		public override void Unsubscribe()
		{
			eventsChannel.CurrentItemChanged.RemoveListener(onCurrentItemChanged);
			eventsChannel.CurrentIndexChanged.RemoveListener(onCurrentIndexChanged);
			eventsChannel.SequentialMovementPerformed.RemoveListener(onSequentialMovementPerformed);
		}


		private void onCurrentItemChanged(Persistence.Item item)
			=> OnCurrentItemChanged?.Invoke(item);
		
		private void onCurrentIndexChanged(int index)
			=> OnCurrentIndexChanged?.Invoke(index);

		private void onSequentialMovementPerformed(int positions)
			=> OnSequentialMovementPerformed?.Invoke(positions);
	}
}