using UnityEngine;
using UnityEngine.UI;


namespace CEITUI.Elements
{
	public class ItemPreviewer : MonoBehaviour
	{
		[SerializeField] private CEIT.Persistence.Item _item;
		[SerializeField] private Image image;
		
		public CEIT.Persistence.Item Item => _item;


		public virtual void SetItem(CEIT.Persistence.Item Item)
		{
			_item = Item;
			image.sprite = _item.Thumbnail;
		}
	}
}