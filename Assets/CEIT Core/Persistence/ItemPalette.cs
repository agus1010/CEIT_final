using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "Empty Item Palette", menuName = "CEIT/Persistence/Item Palette")]
	public class ItemPalette : ScriptableObject, IEnumerable<Item>
	{
		[SerializeField] protected ItemDatabase database;
		[SerializeField] protected Assets.Events.ItemPaletteEventsChannel eventsChannel;

		public bool Locked { get; set; }
		public bool MovementLocked { get; set; }

		public Item this[int position, bool isFixedPosition = false]
			=> isFixedPosition ?
					GetItemAtFixedPosition(position) :
					database[MathUtils.CircularOffset(database.Count, Index, position)];
		public List<Item> this[System.Type type] => database[type];

		public ICollection<System.Type> ItemTypes => database.ItemTypes;


		public int Count => database.Count;
		public Item Current
		{
			get
			{
				if (_current == null)
					SetCurrent(GetItemAtFixedPosition(0), triggerEvents: false);
				return _current;
			}
			set => SetCurrent(value, triggerEvents: true);
		}
		public int Index
		{
			get => _index;
			protected set
			{
				if (database == null) _index = -1;
				if (value < 0 || value >= database.Count)
					throw new System.IndexOutOfRangeException($"Position ({value}) was outside of database's bounds.");
				_index = value;
			}
		}

		public Item Next => this[+1];
		public Item Previous => this[-1];


		private Item _current;
		private int _index;


		public bool Contains(Item item)
			=> IndexOf(item) > -1;

		public int IndexOf(Item item)
			=> database.IndexOf(item);

		public Item GetItemAtFixedPosition(int position)
			=> database[position];

		public void MovePositions(int offset)
			=> MovePositions(offset, triggerEvents: true);
		public void MovePositions(int offset, bool triggerEvents)
		{
			if (MovementLocked) return;
			doMovementOfCurrent(MathUtils.CircularOffset(database.Count, Index, offset), triggerEvents);
			eventsChannel?.FireSequentialMovementPerformed(offset);
		}

		public void MoveToFixedPosition(int position)
			=> MoveToFixedPosition(position, triggerEvents: true);
		public void MoveToFixedPosition(int position, bool triggerEvents)
			=> doMovementOfCurrent(position, triggerEvents);

		public void SetCurrent(Item item)
			=> SetCurrent(item, triggerEvents: true);

		public void SetCurrent(Item item, bool triggerEvents)
		{
			if (Locked) return;
			if (item == null) return;
			if (!Contains(item))
				throw new System.ArgumentOutOfRangeException($"Trying to assign as Current an Item ({item}) that is not registered in Database.");
			var tempIndex = IndexOf(item);
			if (_current != null && tempIndex == Index)
				return;
			Index = tempIndex;
			_current = item;
			if (triggerEvents)
			{
				eventsChannel?.FireCurrentItemChanged(_current);
				eventsChannel?.FireCurrentIndexChanged(Index);
			}
		}

		public void Reset()
		{
			MovementLocked = false;
			MoveToFixedPosition(position: 0, triggerEvents: false);
			eventsChannel?.FireCurrentItemChanged(_current);
		}

		public IEnumerator<Item> GetEnumerator()
			=> new ItemPaletteEnumerator(this);

		public IEnumerator<Item> GetDatabaseEnumerator()
			=> database.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> new ItemPaletteEnumerator(this);


		private Item doMovementOfCurrent(int fixedIndex, bool triggerEvents)
		{
			SetCurrent(this[fixedIndex, true], triggerEvents);
			return Current;
		}
	}


	/* 
	 * 
	 *				Enumerator
	 * 
	 */
	public class ItemPaletteEnumerator : IEnumerator<Item>
	{
		private ItemPalette itemPalette;
		private int visitedItems;

		public ItemPaletteEnumerator(ItemPalette itemPalette)
		{
			this.itemPalette = itemPalette;
			visitedItems = 0;
		}

		public Item Current => itemPalette[visitedItems];
		object IEnumerator.Current => itemPalette[visitedItems];

		public void Dispose() => itemPalette = null;

		public bool MoveNext()
		{
			if(visitedItems >= itemPalette.Count)
				return false;
			visitedItems += 1;
			return true;
		}

		public void Reset() => visitedItems = 0;
	}
}