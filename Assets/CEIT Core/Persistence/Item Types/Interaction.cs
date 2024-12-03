using UnityEngine;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Interaction", menuName = "CEIT/Item/Interaction")]
	public class Interaction : Item
	{
		public Color BaseColor;
		public ItemPalette Palette;
		public InteractionBehaviour Behaviour;
		public bool UsesPalette => Palette != null;

		[Header("Actions Descriptor:")]
		public InteractionActionsDescriptor actionsDescriptor;
	}
}