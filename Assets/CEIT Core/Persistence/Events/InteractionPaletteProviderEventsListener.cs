using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;
using CEIT.Persistence;


namespace CEIT.Assets.Events
{
	public class InteractionPaletteProviderEventsListener : EventsListener
	{
		[SerializeField] private InteractionPaletteProviderEventsChannel eventsChannel;
		protected override object channel => eventsChannel;

		public UnityEvent<InteractionPalette> OnInteractionPaletteChanged;


		public override void Subscribe()
			=> eventsChannel?.InteractionPaletteChanged.AddListener(onInteractionPaletteChanged);

		public override void Unsubscribe()
			=> eventsChannel?.InteractionPaletteChanged.RemoveListener(onInteractionPaletteChanged);


		private void onInteractionPaletteChanged(InteractionPalette interactionPalette)
			=> OnInteractionPaletteChanged?.Invoke(interactionPalette);
	}
}