using UnityEngine;
using UnityEngine.Events;

using CEIT.Persistence;
using CEIT.Events;


namespace CEITUI.Assets.Events
{
	public class TabbedMenuEventsListener : BaseGameObjectActiveStatusEventsListener
	{
		[SerializeField] private TabbedMenuEventsChannel eventsChannel;

		[Header("Tabbed Menu specifics:")]
		public UnityEvent<Item> OnItemSelected;
		public UnityEvent<Prop> OnPropSelected;
		public UnityEvent<Surface> OnSurfaceSelected;

		protected override object channel => eventsChannel;


		public override void Subscribe()
		{
			base.Subscribe();
			eventsChannel.ItemSelected += onItemSelected;
			eventsChannel.PropSelected += onPropSelected;
			eventsChannel.SurfaceSelected += onSurfaceSelected;
		}

		public override void Unsubscribe()
		{
			base.Unsubscribe();
			eventsChannel.ItemSelected -= onItemSelected;
			eventsChannel.PropSelected -= onPropSelected;
			eventsChannel.SurfaceSelected -= onSurfaceSelected;
		}

		private void onItemSelected(Item item)
			=> OnItemSelected?.Invoke(item);

		private void onPropSelected(Prop prop)
			=> OnPropSelected?.Invoke(prop);

		private void onSurfaceSelected(Surface surface)
			=> OnSurfaceSelected?.Invoke(surface);
	}
}