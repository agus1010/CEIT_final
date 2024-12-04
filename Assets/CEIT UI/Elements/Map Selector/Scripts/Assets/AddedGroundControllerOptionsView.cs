using UnityEngine;
using UnityEngine.UI;

using CEIT.Loading.Models;
using CEIT.Loading.Models.Utils;


namespace CEITUI.Elements.MapSelector.Assets
{
	public struct SelectedGroundOptions
	{
		public bool addGround;
		public bool automaticSetup;
		public bool manualSetup;
		public float height;
	}
	public class AddedGroundControllerOptionsView : CEITUIView
	{
		[Header("References:")]
		[SerializeField] private ModelBehaviour modelBehaviour;
		[SerializeField] private AddedGroundController _addedGroundController;

		[Header("Toggles:")]
		[SerializeField] private Toggle addGroundToggle;
		[SerializeField] private Toggle automaticHeightSetupToggle;
		[SerializeField] private Toggle manualHeightSetupToggle;

		[Header("Slider:")]
		[SerializeField] private Slider manualHeightSetupSlider;

		[Header("Debug:")]
		[SerializeField] private bool _addGround = false;

		private bool _isVisible = true;
		private bool _setHeightManually = false;
		private float sliderInitialValue;


		public AddedGroundController addedGroundController => _addedGroundController;
		public float addedGroundHeight => addedGroundController.height;
		public SelectedGroundOptions selectedOptions => new SelectedGroundOptions()
		{
			addGround = _addGround,
			automaticSetup = !_setHeightManually,
			manualSetup = _setHeightManually,
			height = _addedGroundController.height
		};

		public bool addGround
		{
			get => _addGround;
			set
			{
				if (_addGround == value) return;
				_addGround = value;
				addedGroundController.isVisible = _addGround;
				manualHeightSetupSlider.gameObject.SetActive(_addGround && setHeightManually);
				if (_addGround && !setHeightManually)
					addedGroundController.SetGroundToLowerBoundaries(modelBehaviour.model.bounds);
			}
		}

		public bool isVisible
		{
			get => _isVisible;
			set
			{
				_isVisible = value;
				addedGroundController.isVisible = _isVisible && addGround;
				body.SetActive(_isVisible);
			}
		}


		public bool setHeightManually
		{
			get => addGround && _setHeightManually;
			set
			{
				_setHeightManually = value;
				if (_setHeightManually)
				{
					if (modelBehaviour.model != null)
					{
						manualHeightSetupSlider.minValue = -1 * addedGroundController.heightExtent;
						manualHeightSetupSlider.maxValue = addedGroundController.heightExtent;
					}
					addedGroundController.height = manualHeightSetupSlider.value;
				}
				else
					if(modelBehaviour.model != null)
						addedGroundController.SetGroundToLowerBoundaries(modelBehaviour.model.bounds);
			}
		}


		public void Reset()
		{
			addGround = false;
			setHeightManually = false;
			addGroundToggle.isOn = false;
			automaticHeightSetupToggle.SetIsOnWithoutNotify(!setHeightManually);
			manualHeightSetupToggle.SetIsOnWithoutNotify(setHeightManually);
			manualHeightSetupSlider.SetValueWithoutNotify(sliderInitialValue);
			isVisible = true;
		}


		private void Start()
		{
			sliderInitialValue = manualHeightSetupSlider.value;
		}
	}
}