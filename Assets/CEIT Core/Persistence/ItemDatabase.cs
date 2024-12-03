using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace CEIT.Persistence
{
	[Serializable]
	public class ItemIdProvider
	{
		[HideInInspector] [SerializeField] private int autoIncremental = -1;
		public string Generate(Item item)
		{
			autoIncremental += 1;
			string prefix1 = item.GetType().IsSubclassOf(typeof(Prop)) ? "P" : "M";
			string prefix2 = item.GetType().Name.ToUpper();
			string number = autoIncremental.ToString().PadLeft(4, '0');
			return string.Join('-', prefix1, prefix2, number);
		}
	}

	
	[Serializable]
	public class ItemIdProviderDictionary : IDictionary<Type, Tuple<ItemIdProvider, List<Item>>>
	{
		protected List<KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>>> simulatedDict;

		public ItemIdProviderDictionary()
		{
			simulatedDict = new List<KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>>>();
		}


		#region Implementation of IDictionary<Type, Tuple<ItemIdProvider, List<Item>>>
		public Tuple<ItemIdProvider, List<Item>> this[Type key]
		{
			get
			{
				var result = simulatedDict.Where(kvp => kvp.Key == key);
				if (result.Count() > 0)
					return result.First().Value;
				throw new KeyNotFoundException($"The key ({key.Name}) could not be found.");
			}
			set
			{
				if (ContainsKey(key))
					this[key] = value;
				else
					Add(key);
			}
		}

		public ICollection<Type> Keys => simulatedDict.Select(kvp => kvp.Key).ToList();
		public ICollection<Tuple<ItemIdProvider, List<Item>>> Values => simulatedDict.Select(kvp => kvp.Value).ToList();

		public int Count => simulatedDict.Sum(kvp => kvp.Value.Item2.Count);

		public bool IsReadOnly => true;


		// Add Level 1
		public void Add(Type type)
			=> Add(type, new ItemIdProvider(), new List<Item>());
		public void Add(Type type, ItemIdProvider idProvider)
			=> Add(type, idProvider, new List<Item>());
		public void Add(Type type, IEnumerable<Item> items)
			=> Add(type, new ItemIdProvider(), items);

		// Add Level 2
		public void Add(Type type, ItemIdProvider idProvider, IEnumerable<Item> items)
			=> Add(type, new Tuple<ItemIdProvider, List<Item>>(idProvider, items.ToList()));
		public void Add(KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>> kvp)
			=> Add(kvp.Key, kvp.Value);

		// Add Level 3 (Final)
		public void Add(Type key, Tuple<ItemIdProvider, List<Item>> value)
		{
			if (ContainsKey(key))
				throw new ArgumentException($"An item with the same key ({key.Name}) has already been added.");
			simulatedDict.Add(new KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>>(key, value));
			simulatedDict.Sort(new ItemIdProviderDictionaryElementsComparer());
		}
		// Additional
		public void Add(Item item)
		{
			Type key = item.GetType();
			if (ContainsKey(key))
			{
				if (ContainsItem(item))
					return;
			}
			else
			{
				Add(key);
			}
			this[key].Item2.Add(item);
		}


		public void Clear()
			=> simulatedDict.Clear();

		public bool Contains(Item item)
			=> IndexOf(item) >= 0;

		public bool Contains(KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>> item)
			=> simulatedDict.Contains(item);

		public bool ContainsKey(Type key)
			=> simulatedDict.Where(kvp => kvp.Key == key).Count() > 0;

		public void CopyTo(KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>>[] array, int arrayIndex)
			=> simulatedDict.CopyTo(array, arrayIndex);

		private int m_index;
		public int IndexOf(Item item)
		{
			m_index = 0;
			foreach (var i in ToItemEnumerable())
			{
				if (i == item)
					return m_index;
				m_index += 1;
			}
			return -1;
		}

		public IEnumerator<KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>>> GetEnumerator()
			=> simulatedDict.GetEnumerator();

		public bool Remove(Type key)
		{
			if (!ContainsKey(key))
				return false;
			foreach (var kvp in simulatedDict)
			{
				if(kvp.Key == key)
				{
					simulatedDict.Remove(kvp);
					break;
				}
			}
			return true;
		}

		public bool Remove(KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>> kvp)
			=> simulatedDict.Remove(kvp);

		public bool TryGetValue(Type key, out Tuple<ItemIdProvider, List<Item>> value)
		{
			try
			{
				value = this[key];
				return true;
			}
			catch(KeyNotFoundException)
			{
				value = null;
			}
			return false;
		}

		IEnumerator IEnumerable.GetEnumerator()
			=> simulatedDict.GetEnumerator();
		#endregion


		public IEnumerable<Item> ToItemEnumerable()
			=> simulatedDict.Select(kvp => kvp.Value.Item2).SelectMany(item => item);

		public List<Item> ToItemList()
			=> ToItemEnumerable().ToList();

		public bool ContainsItem(Item item)
			=> ToItemEnumerable().Where(i => i == item).Count() > 0;
	}


	[Serializable]
	public class ItemIdProviderDictionaryElementsComparer : IComparer<KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>>>
	{
		public int Compare(KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>> x, KeyValuePair<Type, Tuple<ItemIdProvider, List<Item>>> y)
			=> x.Key.Name.CompareTo(y.Key.Name);
	}


	[CreateAssetMenu(fileName = "New Item Database", menuName = "CEIT/Persistence/Item Database")]
	public class ItemDatabase : ScriptableObject, IEnumerable<Item>, ISerializationCallbackReceiver
	{
		[SerializeField, HideInInspector] private ItemIdProviderDictionary dictionary;
		[SerializeField] private List<Item> _items;

		public Item this[int index] => dictionary.ToItemList()[index];
		public Item this[string id] => dictionary.ToItemEnumerable().Where(item => item.UID == id).FirstOrDefault();
		public List<Item> this[Type type]
		{
			get
			{
				Tuple<ItemIdProvider, List<Item>> tuple = null;
				dictionary.TryGetValue(type, out tuple);
				return tuple.Item2;
			}
		}

		public int Count => dictionary.Count;
		public ICollection<Type> ItemTypes => dictionary.Keys;

		public void Add(Item item)
		{
			if(dictionary == null)
				dictionary = new ItemIdProviderDictionary();
			else
				if(dictionary.ContainsItem(item))
					return;
			dictionary.Add(item);
			ItemIdProvider idProv = dictionary[item.GetType()].Item1;
			item.UID = idProv.Generate(item);
		}

		public bool Contains(Item item)
			=> IndexOf(item) > -1;

		public int IndexOf(Item item)
			=> dictionary.IndexOf(item);

		public IEnumerable<Type> GetStoredTypes()
			=> dictionary.Keys;

		public IEnumerable<KeyValuePair<Type, List<Item>>> AsGroupedTypes()
			=> dictionary.Select(kvp => new KeyValuePair<Type, List<Item>>(kvp.Key, kvp.Value.Item2));

		public IEnumerator<Item> GetEnumerator()
			=> dictionary.ToItemEnumerable().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> dictionary.ToItemEnumerable().GetEnumerator();

		public void OnBeforeSerialize()
		{
			if (_items == null) _items = new List<Item>();
			_items.Clear();
			_items.AddRange(dictionary.ToItemEnumerable());
		}

		public void OnAfterDeserialize()
		{
			if (dictionary == null)
				dictionary = new ItemIdProviderDictionary();
			else
				dictionary.Clear();
			foreach (var item in _items)
			{
				Add(item);
			}
		}

		private void Reset()
		{
			if (dictionary == null)
				dictionary = new ItemIdProviderDictionary();
			else
				dictionary.Clear();
		}
	}
}