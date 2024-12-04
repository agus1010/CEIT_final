using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using CEIT.Loading;
using CEIT.Loading.Models;


namespace CEITUI.Elements.MapSelector
{
	public class ModelBehaviourView : CEITUIView
	{
		[Header("Config:")]
		[SerializeField] private bool canBeCancelled = true;
		[SerializeField] private ModelLoadingOperationStatus minStatusToShowModel = ModelLoadingOperationStatus.MESHES_LOADED;

		[Header("Model Loading References:")]
		[SerializeField] private ModelLoadingOperationParameters parameters;
		[SerializeField] private ModelBehaviour _modelBehaviour;

		[Header("UI References:")]
		[SerializeField] private Modals.ErrorModal errorModal;
		[SerializeField] private LoadingBarBehaviour loadingBarBehaviour;
		[SerializeField] private Button cancelButton;

		[Header("Events:")]
		public UnityEvent OnLoadingCancelled;


		public ModelBehaviour modelBehaviour => _modelBehaviour;

		public bool isVisible
		{
			get => uiIsVisible || modelIsVisible;
			set
			{
				if(value)       // isVisible = true;
				{
					_modelBehaviour.modelFile = parameters.mapFile;
					tryShowModel(_modelBehaviour.operation.status);
				}

				else			// isVisible = false;
					updateVisibilities(false, false);
			}
		}

		public bool uiIsVisible => body.activeSelf;
		public bool modelIsVisible => _modelBehaviour.isVisible;


		public void ErrorRaised(System.Exception exception)
		{
			updateLoadingBarVisibilty(false);
			errorModal.InsertException(exception);
			tryShowModel(ModelLoadingOperationStatus.ABORTED_BY_ERROR);
		}

		public void ModelStartedLoading()
		{
			updateLoadingBarVisibilty(true);
			loadingBarBehaviour.SetAmmount(0f);
			loadingBarBehaviour.TitleText = parameters.mapFile.Name;
			tryShowModel(ModelLoadingOperationStatus.STARTED);
		}

		public void ModelMeshesLoaded()
		{
			tryShowModel(ModelLoadingOperationStatus.MESHES_LOADED);
		}

		public void ModelMaterialsLoaded()
		{
			tryShowModel(ModelLoadingOperationStatus.MATERIALS_LOADED);
		}

		public void ModelFinishedLoading()
		{
			tryShowModel(ModelLoadingOperationStatus.COMPLETED);
		}

		public void OnCancelPressed()
		{
			_modelBehaviour.operation.Abort();
			OnLoadingCancelled?.Invoke();
		}

		public void ProgressMade(float amount)
		{
			loadingBarBehaviour.SetAmmount(amount);
		}

		public void ResetRotations()
			=> modelBehaviour.ResetRotations();



		private void Start()
		{
			loadingBarBehaviour.gameObject.SetActive(true);
			errorModal.isVisible = false;
			cancelButton.gameObject.SetActive(canBeCancelled);
		}


		private void updateLoadingBarVisibilty(bool newVisibilityValue)
		{
			loadingBarBehaviour.gameObject.SetActive(newVisibilityValue);
			cancelButton.gameObject.SetActive(newVisibilityValue);
			errorModal.isVisible = !newVisibilityValue;
		}
		private void tryShowModel(ModelLoadingOperationStatus currentStatus)
		{
			bool mustShowModel = currentStatus >= minStatusToShowModel && (int)currentStatus < 20;
			updateVisibilities(
				bodyVisibility: !mustShowModel,
				modelVisibility: mustShowModel
			);
		}

		private void updateVisibilities(bool bodyVisibility, bool modelVisibility)
		{
			body.SetActive(bodyVisibility);
			_modelBehaviour.isVisible = modelVisibility;
		}
	}
}