using UnityEngine;

using CEIT.Persistence;
using CEITUI.Elements;


namespace CEITUI.Palettes
{
	public class GridPaletteView : PaletteView
	{
		[SerializeField] private GameObject toggablePreviewerPrefab;
		[SerializeField] private Transform previewersParent;

		public override ToggableItemPreviewer InstantiatePreviewer(Item item)
		{
			ToggableItemPreviewer prev = Instantiate(toggablePreviewerPrefab, previewersParent).GetComponent<ToggableItemPreviewer>();
			prev.SetItem(item);
			prev.Toggle.group = this.ToggleGroup;
			prev.Toggle.isOn = false;
			return prev;
		}
	}
}