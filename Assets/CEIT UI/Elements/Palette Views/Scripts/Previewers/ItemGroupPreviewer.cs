using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CEIT.Persistence;


namespace CEITUI.Elements
{
	public class ItemGroupPreviewer : MonoBehaviour
	{
		[Header("UI Elements")]
		[SerializeField] private TMPro.TextMeshProUGUI title;
		[SerializeField] private Image thumbnail;
		[Header("Previewer Prefab:")]
		[SerializeField] private GameObject toggablePreviewerPrefab;
		[SerializeField] private Transform previewersParent;
		

		public string Title
		{
			get => title.text;
			set => title.text = value;
		}

		public Sprite Thumbnail
		{
			get => thumbnail.sprite;
			set => thumbnail.sprite = value;
		}


		private System.Type _itemType;
		private List<ToggableItemPreviewer> _previewers;


		public System.Type ItemType
		{
			get => _itemType;
			set
			{
				_itemType = value;
				if(_previewers == null || _previewers.Count == 0)
				{
					if(_previewers == null)
						_previewers = new List<ToggableItemPreviewer>();
					return;
				}
				foreach (var prev in _previewers)
				{
					if (prev.Item.GetType() != _itemType)
					{
						_previewers.Remove(prev);
						Destroy(prev);
					}
				}
			}
		}

		public ToggableItemPreviewer AddItem(Item item)
		{
			if (!CanBeAdded(item)) throw new System.ArgumentException("Item is not of group's type.");

			if (ItemType == null) ItemType = item.GetType();

			ToggableItemPreviewer prev = Instantiate(toggablePreviewerPrefab, previewersParent).GetComponent<ToggableItemPreviewer>();
			prev.SetItem(item);
			_previewers.Add(prev);
			return prev;
		}

		public bool CanBeAdded(Item item) => ItemType == null || item.GetType() == ItemType;
	}
}