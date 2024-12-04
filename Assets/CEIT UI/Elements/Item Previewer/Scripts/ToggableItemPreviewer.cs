using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace CEITUI.Elements
{
	[RequireComponent(typeof(Toggle))]
	public class ToggableItemPreviewer : ItemPreviewer, IPointerEnterHandler, IPointerExitHandler
	{
		public UnityEvent<CEIT.Persistence.Item> ItemSelected;
		public UnityEvent PointerEnter;
		public UnityEvent PointerExit;

		private Toggle _toggle;
		public Toggle Toggle
		{
			get
			{
				if(_toggle == null) _toggle = GetComponent<Toggle>();
				return _toggle;
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			PointerEnter?.Invoke();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			PointerExit?.Invoke();
		}

		public void ValueChanged(bool newValue)
		{
			if (newValue) ItemSelected?.Invoke(this.Item);
		}

		private void Reset()
		{
			_toggle = GetComponent<Toggle>();
		}
	}
}