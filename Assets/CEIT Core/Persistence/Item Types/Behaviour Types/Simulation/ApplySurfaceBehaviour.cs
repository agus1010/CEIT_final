using UnityEngine;

using CEIT.Interactables;
using CEIT.Player;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Apply Surface Behaviour", menuName = "CEIT/Persistence/Behaviours/Apply Surface")]
	public class ApplySurfaceBehaviour : InteractionBehaviour
	{
		public ItemPalette palette;
		
		private PlayerPointer pointer;

		private bool primaryState = false;
		private bool secondaryState = false;


		public override void Initialize(Interaction interaction, PlayerPointer pointer)
		{
			palette = interaction.Palette;
			this.pointer = pointer;
			this.pointer.ShotMode = Raycasts.ShotFilter.ALL;
			primaryState = false;
			secondaryState = false;
		}

		public override void Stop()
		{
			palette = null;
			pointer = null;
		}

		public override void TryPerformPrimary(bool newPrimaryValue)
		{
			primaryState = newPrimaryValue && !primaryState;
			if (primaryState)
				performAction(paint);
		}

		public override void TryPerformSecondary(bool newSecondaryValue)
		{
			secondaryState = newSecondaryValue && !secondaryState;
			if (secondaryState)
				performAction(undo);
		}



		private SurfaceHistory m_extractedHistory;
		private void performAction(System.Action<SurfaceHistory> action)
		{
			if(!pointer.IsLookingAtGraphics && pointer.ClosestTarget != null)
			{
				if (pointer.CurrentPhysicsShot.Collider.TryGetComponent(out m_extractedHistory))
				{
					action(m_extractedHistory);
				}
			}
		}

		private void paint(SurfaceHistory history)
			=> history.Paint(palette.Current as Surface);

		private void undo(SurfaceHistory history)
			=> history.Undo();
	}
}