using UnityEngine;

using CEIT.Persistence;
using CEITUI.Elements;


namespace CEITUI.Palettes
{
	public class PaletteViewItemPreviewer : ToggableItemPreviewer
	{
		[SerializeField] private GameObject titleContainer;
		[SerializeField] private TMPro.TextMeshProUGUI title;


		public override void SetItem(Item Item)
		{
			base.SetItem(Item);
			if (titleContainer != null)
				title.text = Item.ItemName;
		}

		private void Start()
		{
			titleContainer?.SetActive(false);
		}
	}
}