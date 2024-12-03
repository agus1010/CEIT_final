using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Interaction Actions Descriptor", menuName = "CEIT/Persistence/Base Interaction Actions Descriptor")]
	public class InteractionActionsDescriptor : ScriptableObject
	{
		[SerializeField] protected string primaryActionDescription = "Primary Action";
		[SerializeField] protected string secondaryActionDescription = "Secondary Action";
		[SerializeField] protected string changeInteractionValueDescription = "Change Interaction Value";

		public UnityEvent<string> PrimaryActionDescriptionChanged;
		public UnityEvent<string> SecondaryActionDescriptionChanged;
		public UnityEvent<string> ChangeInteractionValueDescriptionChanged;

		public bool HasAnyText =>
			!(string.IsNullOrEmpty(PrimaryActionDescription) &&
			string.IsNullOrEmpty(SecondaryActionDescription) &&
			string.IsNullOrEmpty(ChangeInteractionValueDescription));


		public string PrimaryActionDescription
		{
			get => primaryActionDescription;
			set
			{
				primaryActionDescription = value;
				PrimaryActionDescriptionChanged?.Invoke(primaryActionDescription);
			}
		}

		public string SecondaryActionDescription
		{
			get => secondaryActionDescription;
			set
			{
				secondaryActionDescription = value;
				SecondaryActionDescriptionChanged?.Invoke(secondaryActionDescription);
			}
		}

		public string ChangeInteractionValueDescription
		{
			get => changeInteractionValueDescription;
			set
			{
				changeInteractionValueDescription = value;
				ChangeInteractionValueDescriptionChanged?.Invoke(changeInteractionValueDescription);
			}
		}
	}
}