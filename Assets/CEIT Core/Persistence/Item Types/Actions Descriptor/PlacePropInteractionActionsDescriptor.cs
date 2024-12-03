using UnityEngine;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "Place Prop Interaction Actions Descriptor", menuName = "CEIT/Persistence/Place Prop Interaction Actions Descriptor")]
	public class PlacePropInteractionActionsDescriptor : InteractionActionsDescriptor
	{
		[Header("Idle Mode")]
		[SerializeField] private string idleModePrimary;
		[SerializeField] private string idleModeSecondary;
		[SerializeField] private string idleModeChangeInteractionValue;

		[Header("Grabbing Spawend Mode")]
		[SerializeField] private string spawnedModePrimary;
		[SerializeField] private string spawnedModeSecondary;
		[SerializeField] private string spawnedModeChangeInteractionValue;

		[Header("Grabbing Existent Mode")]
		[SerializeField] private string grabModePrimary;
		[SerializeField] private string grabModeSecondary;
		[SerializeField] private string grabModeChangeInteractionValue;


		public void SetIdleMode()
		{
			PrimaryActionDescription = idleModePrimary;
			SecondaryActionDescription = idleModeSecondary;
			ChangeInteractionValueDescription = idleModeChangeInteractionValue;
		}

		public void SetGrabbingSpawnedMode()
		{
			PrimaryActionDescription = spawnedModePrimary;
			SecondaryActionDescription = spawnedModeSecondary;
			ChangeInteractionValueDescription = spawnedModeChangeInteractionValue;
		}

		public void SetGrabbingExistentMode()
		{
			PrimaryActionDescription = grabModePrimary;
			SecondaryActionDescription = grabModeSecondary;
			ChangeInteractionValueDescription = grabModeChangeInteractionValue;
		}
	}
}