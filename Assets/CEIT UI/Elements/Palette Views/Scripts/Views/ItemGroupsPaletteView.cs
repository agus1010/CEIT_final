using System.Collections.Generic;
using UnityEngine;

using CEIT.Persistence;
using CEITUI.Elements;


namespace CEITUI.Palettes
{
	public class ItemGroupsPaletteView : PaletteView
	{
		[SerializeField] private GameObject itemGroupPrefab;
		[SerializeField] private Transform groupsParent;
		[SerializeField] private ItemDatabase defaultsDB;
		
		public Dictionary<System.Type, ItemGroupPreviewer> Groups;


		protected override void onPrePopulate()
		{
			Groups = new Dictionary<System.Type, ItemGroupPreviewer>();
		}

		private ItemGroupPreviewer instantiateGroupPreviewer(Item item)
		{
			ItemGroupPreviewer prev = Instantiate(itemGroupPrefab, groupsParent).GetComponent<ItemGroupPreviewer>();
			var itemDefault = defaultsDB[item.GetType()][0];
			if(itemDefault != null)
			{
				prev.Title = itemDefault.ItemName;
				prev.Thumbnail = itemDefault.Thumbnail;
			}
			prev.ItemType = item.GetType();
			return prev;
		}

		public override ToggableItemPreviewer InstantiatePreviewer(Item item)
		{
			if(! Groups.ContainsKey(item.GetType()))
			{
				Groups[item.GetType()] = instantiateGroupPreviewer(item);
			}
			var prev = Groups[item.GetType()].AddItem(item);
			prev.Toggle.group = this.ToggleGroup;
			prev.Toggle.isOn = false;
			return prev;
		}
	}
}