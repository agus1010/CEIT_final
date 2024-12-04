using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using CEIT.Loading;
using CEIT.Loading.Models;


namespace CEITUI.Elements.MapSelector.Assets
{
    public class SpawnSelectionMapSelectorView : SubmittableMapSelectorView
	{
		[SerializeField] private ModelBehaviour modelBehaviour;
		[SerializeField] private GroundSelectionMapSelectorView groundSelectionView;
		[SerializeField] private SpawnSelectionController spawnSelectionController;
		[SerializeField] private Button submitButton;
		[SerializeField] private ModelLoadingOperationParameters parameters;
		[SerializeField] private Animations.LerpToRotation rotAnimation;

		public UnityEvent<bool> OnActiveStatusChanged;

		private SelectedGroundOptions previousSelectedGroundOptions = new SelectedGroundOptions();

		private bool shouldResetSpawnSelectionController =>
			!spawnSelectionController.spawnPointAvailable || !previousSelectedGroundOptions.Equals(groundSelectionView.optionsView.selectedOptions);

		public override void FireOnSubmitted()
		{
			parameters.spawnPosition = spawnSelectionController.CalculateFinalWorldPosition();
			base.FireOnSubmitted();
		}



		public override void UpdateGraphics()
		{
			rotAnimation.Play(forward: isCurrentlyActive);

			submitButton.interactable = spawnSelectionController.spawnPointAvailable;
			
			spawnSelectionController.isVisible = isCurrentlyActive;

			if(shouldResetSpawnSelectionController)
			{
				spawnSelectionController.Reset();
				previousSelectedGroundOptions = groundSelectionView.optionsView.selectedOptions;
			}

			modelBehaviour.isVisible = isCurrentlyActive;

			var optionsView = groundSelectionView.optionsView;
			optionsView.addedGroundController.isVisible = optionsView.addGround;
			
			base.UpdateGraphics();

			OnActiveStatusChanged?.Invoke(isCurrentlyActive);
		}

		public void OnSpawnPointChanged()
		{
			submitButton.interactable = spawnSelectionController.spawnPointAvailable;
			//spawnSelectionController.CalculateFinalWorldPosition();
		}

		public void OnModelStartedLoading()
			=> spawnSelectionController.Reset();

	}
}
