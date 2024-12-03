using UnityEngine;
using UnityEngine.InputSystem;


namespace CEIT.Player.Input
{
	public enum SetOfActions
	{
		SIMULATION,
		MAP_SELECTION
	}
	public class VRInputReader : CEITPlayerInputReader
	{
		public InputActionAsset vrPlayerInputActions;

		[Header("Config:")]
		[SerializeField] private SetOfActions _setOfActions = SetOfActions.SIMULATION;

		private InputActionMap m_leftHandInteractionMap = null;
		private InputActionMap m_rightHandInteractionMap = null;
		protected InputActionMap leftHandInteractionMap
		{
			get
			{
				if (m_leftHandInteractionMap == null)
					m_leftHandInteractionMap = vrPlayerInputActions.FindActionMap("XRI LeftHand Interaction");
				return m_leftHandInteractionMap;
			}
		}
		protected InputActionMap rightHandInteractionMap
		{
			get
			{
				if (m_rightHandInteractionMap == null)
					m_rightHandInteractionMap = vrPlayerInputActions.FindActionMap("XRI RightHand Interaction");
				return m_rightHandInteractionMap;
			}
		}

		public SetOfActions setOfActions
		{
			get => _setOfActions;
			set
			{
				_setOfActions = value;
				unsubscribe();
				subscribe();
			}
		}

		public override void SetHoldingPause(InputAction.CallbackContext callbackContext)
			=> SetHoldingPause(callbackContext.ReadValueAsButton());
		public override void SetHoldingTabbedMenu(InputAction.CallbackContext callbackContext)
			=> SetHoldingTabbedMenu(callbackContext.ReadValueAsButton());


		private void Start()
			=> subscribe();

		private void OnApplicationQuit()
			=> unsubscribe();

		private void OnEnable()
			=> vrPlayerInputActions.Enable();

		private void OnDisable()
			=> vrPlayerInputActions.Disable();


		private void subscribe()
		{
			leftHandInteractionMap.FindAction("Pause Menu").performed += SetHoldingPause;
			rightHandInteractionMap.FindAction("Primary").performed += SetHoldingPrimary;
			rightHandInteractionMap.FindAction("Secondary").performed += SetHoldingSecondary;
			if (_setOfActions == SetOfActions.SIMULATION)
			{
				leftHandInteractionMap.FindAction("Tabbed Menu").performed += SetHoldingTabbedMenu;
				leftHandInteractionMap.FindAction("Interaction Wheel").performed += SetHoldingInteractionWheel;
				leftHandInteractionMap.FindAction("ChangeInteraction").performed += SetChangingInteractionInDirection;
				rightHandInteractionMap.FindAction("ChangeInteractionPaletteValue").performed += SetMovingCurrentInteractionsPaletteInDirection;
			}
		}


		private void unsubscribe()
		{
			leftHandInteractionMap.FindAction("Pause Menu").performed -= SetHoldingPause;
			rightHandInteractionMap.FindAction("Primary").performed -= SetHoldingPrimary;
			rightHandInteractionMap.FindAction("Secondary").performed -= SetHoldingSecondary;
			if (_setOfActions == SetOfActions.SIMULATION)
			{
				leftHandInteractionMap.FindAction("Tabbed Menu").performed -= SetHoldingTabbedMenu;
				leftHandInteractionMap.FindAction("Interaction Wheel").performed -= SetHoldingInteractionWheel;
				leftHandInteractionMap.FindAction("ChangeInteraction").performed -= SetChangingInteractionInDirection;
				rightHandInteractionMap.FindAction("ChangeInteractionPaletteValue").performed -= SetMovingCurrentInteractionsPaletteInDirection;
			}
		}
	}
}