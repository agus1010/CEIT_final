using UnityEngine;

using CEIT.Assets.Events;


namespace CEIT.Persistence
{
    [CreateAssetMenu(fileName = "New Interaction Palette Provider", menuName = "CEIT/Persistence/Providers/Interaction Palette")]
    public class InteractionPaletteProvider : ScriptableObject
    {
        [SerializeField] private InteractionPalette interactionPalette;
        [SerializeField] private InteractionPaletteProviderEventsChannel eventsChannel;

        public InteractionPalette CurrentPalette
        {
            get => interactionPalette;
            private set => interactionPalette = value;
        }

        public void SetCurrentPalette(InteractionPalette interactionPalette)
        {
            CurrentPalette = interactionPalette;
            eventsChannel?.FireInteractionPaletteChanged(CurrentPalette);
        }

        public void HaveCurrentPaletteChangeInteractionInDirection(int direction)
        {
            CurrentPalette.MovePositions(direction);
        }

        public void HaveCurrentPaletteMoveInteractionsInDirection(int direction)
        {
            CurrentPalette.MoveInteractionsPalettePositions(direction);
        }
    }
}