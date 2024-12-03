using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Assets.Events
{
	[CreateAssetMenu(fileName = "New Item Palette Events Channel", menuName = "CEIT/Events/Channels/Core/Item Palette")]
	public class ItemPaletteEventsChannel : ScriptableObject
	{
		public UnityEvent<Persistence.Item> CurrentItemChanged;
		public UnityEvent<int> CurrentIndexChanged;
		public UnityEvent<int> SequentialMovementPerformed;

		public void FireCurrentItemChanged(Persistence.Item item)
			=> CurrentItemChanged?.Invoke(item);

		public void FireCurrentIndexChanged(int index)
			=> CurrentIndexChanged?.Invoke(index);

		public void FireSequentialMovementPerformed(int positions)
			=> SequentialMovementPerformed?.Invoke(positions);
	}
}