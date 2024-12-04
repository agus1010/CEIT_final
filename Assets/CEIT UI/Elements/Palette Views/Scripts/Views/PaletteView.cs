using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using CEIT.Persistence;
using CEITUI.Elements;


namespace CEITUI.Palettes
{
	[RequireComponent(typeof(CustomToggleGroup))]
	public abstract class PaletteView : MonoBehaviour
	{
		[SerializeField] private ItemPalette palette;

		public UnityEvent<Item> OnItemSelected;

		private ToggableItemPreviewer[] _previewers;
		private ToggleGroup _toggleGroup;

		public ItemPalette Palette => palette;
		public ToggableItemPreviewer[] Previewers => _previewers;
		public ToggleGroup ToggleGroup
		{
			get
			{
				if(_toggleGroup == null) _toggleGroup = GetComponent<ToggleGroup>();
				return _toggleGroup;
			}
		}

		private bool m_initialized = false;

		public virtual void Populate()
		{
			onPrePopulate();
			_previewers = new ToggableItemPreviewer[Palette.Count];
			Item item;
			ToggableItemPreviewer previewer;
			for (int i = 0; i < Palette.Count; i++)
			{
				item = Palette[i, isFixedPosition: true];
				previewer = InstantiatePreviewer(item);
				_toggleGroup.RegisterToggle(previewer.Toggle);
				Previewers[i] = previewer;
				previewer.ItemSelected.AddListener(fireOnItemSelected);
				previewer.name = item.ItemName + " - Toggable Previewer";
			}
			onPostPopulate();
		}

		private void fireOnItemSelected(Item item)
			=> OnItemSelected?.Invoke(item);

		public abstract ToggableItemPreviewer InstantiatePreviewer(Item item);
		public void UpdateToggles()
		{
			Toggle toggle = Previewers[Palette.Index].Toggle;
			toggle.group.allowSwitchOff = true;
			foreach (var p in Previewers)
			{
				p.Toggle.isOn = false;
			}
			toggle.isOn = true;
			toggle.group.allowSwitchOff = false;
		}

		protected virtual void onPrePopulate() { }
		protected virtual void onPostPopulate() { }

		private void Reset()
		{
			_toggleGroup = GetComponent<ToggleGroup>();
		}
		private void OnEnable()
		{
			if(m_initialized)
				UpdateToggles();
		}
		private void Start()
		{
			Populate();
			UpdateToggles();
			m_initialized = true;
		}
	}
}