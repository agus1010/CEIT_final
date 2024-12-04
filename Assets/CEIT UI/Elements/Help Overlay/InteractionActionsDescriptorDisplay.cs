using UnityEngine;
using UnityEngine.UI;
using TMPro;

using CEIT.Persistence;


namespace CEITUI.Elements
{
	public class InteractionActionsDescriptorDisplay : MonoBehaviour
	{
		[Header("References:")]
		public GameObject primaryActionSection;
		public GameObject secondaryActionSection;
		public GameObject changeInteractionValueSection;
		public TextMeshProUGUI primaryTitle;
		public TextMeshProUGUI secondaryTitle;
		public TextMeshProUGUI changeInteractionValueTitle;
		public TextMeshProUGUI emptyTitle;
		public GameObject scrollIndication;

		[Header("Debug:")]
		[SerializeField] private Interaction interaction;
		[SerializeField] private bool _moreInfoSelected = false;

		public InteractionActionsDescriptor descriptor => interaction.actionsDescriptor ?? null;
		public bool moreInfoSelected
		{
			get => _moreInfoSelected;
			set
			{
				_moreInfoSelected = value;
				scrollIndication.SetActive(shouldShowScrollIndication(descriptor.ChangeInteractionValueDescription));
			}
		}



		public void SetInteraction(Interaction interaction)
		{
			if (this.interaction != null)
				unsubscribe();
			this.interaction = interaction;
			subscribe();
			updateGraphics();
		}

		public void SetInteraction(InteractionPalette interactionPalette)
			=> SetInteraction(interactionPalette.Current as Interaction);

		public void SetInteraction(InteractionPaletteProvider interactionPaletteProvider)
			=> SetInteraction(interactionPaletteProvider.CurrentPalette);



		private void Start()
		{
			if (interaction != null)
				SetInteraction(interaction);
		}


		private void subscribe()
		{
			descriptor.PrimaryActionDescriptionChanged.AddListener(updateGraphicsForPrimary);
			descriptor.SecondaryActionDescriptionChanged.AddListener(updateGraphicsForSecondary);
			descriptor.ChangeInteractionValueDescriptionChanged.AddListener(updateGraphicsForChangeInteractionValue);
		}

		private void unsubscribe()
		{
			descriptor.PrimaryActionDescriptionChanged.RemoveListener(updateGraphicsForPrimary);
			descriptor.SecondaryActionDescriptionChanged.RemoveListener(updateGraphicsForSecondary);
			descriptor.ChangeInteractionValueDescriptionChanged.RemoveListener(updateGraphicsForChangeInteractionValue);
		}

		private void updateGraphics()
		{
			updateGraphicsForPrimary(descriptor.PrimaryActionDescription);
			updateGraphicsForSecondary(descriptor.SecondaryActionDescription);
			updateGraphicsForChangeInteractionValue(descriptor.ChangeInteractionValueDescription);
			checkForEmpty();
		}

		private bool tryUpdateGraphicsAcordingWithText(GameObject section, TextMeshProUGUI title, string newDescription)
		{
			bool hasText = !string.IsNullOrEmpty(newDescription);
			if (title != null)
				title.text = newDescription;
			section?.SetActive(hasText);
			return hasText;
		}

		private void checkForEmpty()
		{
			emptyTitle?.gameObject.SetActive(!descriptor.HasAnyText);
			LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
		}

		private void updateGraphicsForPrimary(string newDescription)
		{
			tryUpdateGraphicsAcordingWithText(primaryActionSection, primaryTitle, newDescription);
			checkForEmpty();
		}
		
		private void updateGraphicsForSecondary(string newDescription)
		{
			tryUpdateGraphicsAcordingWithText(secondaryActionSection, secondaryTitle, newDescription);
			checkForEmpty();
		}

		private void updateGraphicsForChangeInteractionValue(string newDescription)
		{
			tryUpdateGraphicsAcordingWithText(changeInteractionValueSection, changeInteractionValueTitle, newDescription);
			scrollIndication?.SetActive(shouldShowScrollIndication(newDescription));
			checkForEmpty();
		}

		private bool shouldShowScrollIndication(string description)
			=> moreInfoSelected && interaction.UsesPalette && string.IsNullOrEmpty(description);
	}
}