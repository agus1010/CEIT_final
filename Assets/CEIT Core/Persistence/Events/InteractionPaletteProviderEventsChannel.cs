using UnityEngine;
using UnityEngine.Events;

using CEIT.Persistence;


namespace CEIT.Assets.Events
{
	[CreateAssetMenu(fileName = "New Interaction Palette Provider Events Channel", menuName = "CEIT/Events/Channels/Core/Interaction Palette Provider")]
	public class InteractionPaletteProviderEventsChannel : ScriptableObject
	{
		public UnityEvent<InteractionPalette> InteractionPaletteChanged;


		public void FireInteractionPaletteChanged(InteractionPalette interactionPalette)
			=> InteractionPaletteChanged?.Invoke(interactionPalette);
	}
}