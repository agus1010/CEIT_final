using UnityEngine;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "Take Measurement Interaction Actions Descriptor", menuName = "CEIT/Persistence/Take Measurement Interaction Actions Descriptor")]
	public class TakeMeasurementInteractionActionsDescriptor : InteractionActionsDescriptor
	{
		[Header("Idle Mode")]
		[SerializeField] private string idleModePrimary;
		[SerializeField] private string idleModeSecondary;
		[SerializeField] private string idleModeChangeInteractionValue;

		[Header("Measuring Mode")]
		[SerializeField] private string measuringModePrimary;
		[SerializeField] private string measuringModeSecondary;
		[SerializeField] private string measuringModeChangeInteractionValue;


		public void SetIdleMode()
		{
			PrimaryActionDescription = idleModePrimary;
			SecondaryActionDescription = idleModeSecondary;
			ChangeInteractionValueDescription = idleModeChangeInteractionValue;
		}

		public void SetMeasuringMode()
		{
			PrimaryActionDescription = measuringModePrimary;
			SecondaryActionDescription = measuringModeSecondary;
			ChangeInteractionValueDescription = measuringModeChangeInteractionValue;
		}
	}
}