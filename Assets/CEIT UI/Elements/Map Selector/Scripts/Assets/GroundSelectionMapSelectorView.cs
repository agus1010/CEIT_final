using CEIT.Loading;
using UnityEngine;


namespace CEITUI.Elements.MapSelector.Assets
{
	public class GroundSelectionMapSelectorView : SubmittableMapSelectorView
	{
		[Header("Scene References:")]
		[SerializeField] private FlowController flowController;
		[SerializeField] private HeaderController headerController;
		[SerializeField] private ModelBehaviourView modelBehaviourView;
		[SerializeField] private AddedGroundControllerOptionsView addedGroundControllerOptionsView;
		[SerializeField] private GameObject modelRotationOverlay;
		[SerializeField] private ModelLoadingOperationParameters parameters;

		public AddedGroundControllerOptionsView optionsView => addedGroundControllerOptionsView;

		public override void UpdateGraphics()
		{
			modelBehaviourView.isVisible = isCurrentlyActive;
			if (isCurrentlyActive)
			{
				if (modelBehaviourView.modelBehaviour.operation.error != null)
					return;
				bool modelIsVisible = modelBehaviourView.modelIsVisible;
				updateBodyAndGroundVisibility(modelIsVisible);
				headerController.backtrackingAllowed = modelIsVisible;
				modelRotationOverlay.SetActive(modelIsVisible && optionsView.addGround && optionsView.setHeightManually);
				modelRotationOverlay.GetComponentInChildren<UnityEngine.UI.Slider>().SetValueWithoutNotify(0f);
				if (!modelIsVisible)
				{
					addedGroundControllerOptionsView.Reset();
					modelBehaviourView.ResetRotations();
				}
			}
			else
			{
				bool consumedWasSubmitted = wasSubmitted;
				parameters.addGround = consumedWasSubmitted && addedGroundControllerOptionsView.addGround;
				parameters.groundHeight = addedGroundControllerOptionsView.addedGroundController.normalizedHeight; //addedGroundControllerOptionsView.addedGroundController.CalculateFinalGroundHeight();
				updateBodyAndGroundVisibility(false);
				modelRotationOverlay.SetActive(false);
			}
		}

		public void LoadingCancelled()
		{
			updateBodyAndGroundVisibility(false);
			headerController.backtrackingAllowed = true;
			flowController.PreviousStep();
		}

		public void LoadingCrashed(System.Exception exception)
		{
			LoadingCrashed();
		}

		public void LoadingCrashed()
		{
			updateBodyAndGroundVisibility(false);
			modelBehaviourView.isVisible = true;
			headerController.backtrackingAllowed = true;
		}


		protected void Start()
		{
			UpdateGraphics();
		}


		private void updateBodyAndGroundVisibility(bool visibility)
		{
			body.SetActive(visibility);
			addedGroundControllerOptionsView.isVisible = visibility;
		}
	}
}