using UnityEngine;
using UnityEngine.Events;

using CEIT.Persistence;


namespace CEIT.Assets.Events
{
    public class NewInteractionDetectedEventMultiplexor : NewItemDetectedEventMultiplexor
    {
		public UnityEvent<Color> NewInteractionColorDetected;
        public UnityEvent<ItemPalette> NewItemPaletteDetected;


		public override void FireAllEventsWith(Item item)
		{
			base.FireAllEventsWith(item);
			var interaction = item as Interaction;
			NewInteractionColorDetected?.Invoke(interaction.BaseColor);
			NewItemPaletteDetected?.Invoke(interaction.Palette);
		}
	}
}