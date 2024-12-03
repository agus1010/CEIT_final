namespace CEIT.Persistence
{
	[UnityEngine.CreateAssetMenu(fileName = "Empty Interaction Palette", menuName = "CEIT/Persistence/Interaction Palette")]
    public class InteractionPalette : ItemPalette
    {
		[UnityEngine.HideInInspector][UnityEngine.SerializeField] private bool innerPalettesLocked = false;
		public bool InnerPalettesLocked
		{
			get => innerPalettesLocked;
			set
			{
				if (value == innerPalettesLocked) return;
				innerPalettesLocked = value;
				foreach (var interaction in this)
				{
					if((interaction as Interaction).UsesPalette)
					{
						(interaction as Interaction).Palette.Locked = value;
					}
				}
			}
		}

		private Interaction currentInteraction => Current as Interaction;

		public void MoveInteractionPaletteToFixedPosition(int position)
			=> MoveInteractionPaletteToFixedPosition(position, triggerEvents: true);

		public void MoveInteractionPaletteToFixedPosition(int position, bool triggerEvents)
		{
			if (!currentInteraction.UsesPalette) return;
			updateInteractionPaletteCurrent(position, triggerEvents);
		}


		public void MoveInteractionsPalettePositions(int offset)
			=> MoveInteractionsPalettePositions(offset, triggerEvents: true);
		
		public void MoveInteractionsPalettePositions(int offset, bool triggerEvents)
		{
			if (!currentInteraction.UsesPalette) return;
			updateInteractionPaletteCurrent(offset, triggerEvents);
		}


		private void updateInteractionPaletteCurrent(int offset, bool triggerEvents)
		{
			if (InnerPalettesLocked) return;
			currentInteraction.Palette.MovePositions(offset);
		}
	}
}