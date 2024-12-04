using UnityEngine;

using CEIT.Persistence;
using CEIT.Events;


namespace CEITUI.Assets.Events
{
	[CreateAssetMenu(fileName = "New Tabbed Menu Events Channel", menuName = "CEIT/Events/Channels/UI/Tabbed Menu")]
	public class TabbedMenuEventsChannel : GameObjectActiveStatusEventsChannel
	{
		public System.Action<Item> ItemSelected;
		public System.Action<Prop> PropSelected;
		public System.Action<Surface> SurfaceSelected;

		public void FireItemSelected(Item item)
			=> ItemSelected?.Invoke(item);

		public void FirePropSelected(Prop prop)
			=> PropSelected?.Invoke(prop);

		public void FireSurfaceSelected(Surface surface)
			=> SurfaceSelected?.Invoke(surface);
	}
}