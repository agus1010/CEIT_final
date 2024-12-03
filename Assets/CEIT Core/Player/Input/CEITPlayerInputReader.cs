using UnityEngine;
using UnityEngine.InputSystem;


namespace CEIT.Player.Input
{
	public class CEITPlayerInputReader : MonoBehaviour
	{
		[Header("Events Channel:")]
		public Events.PlayerIntentEventsChannel eventsChannel;

		[Header("Data:")]
		[Header("Player:")]
		[SerializeField] private bool jumping = false;
		[SerializeField] private bool sprinting = false;
		[SerializeField] private bool holdingPrimary = false;
		[SerializeField] private bool holdingSecondary = false;

		[Header("UI")]
		[SerializeField] private bool holdingShowInfo = false;
		[SerializeField] private bool holdingPause = false;
		[SerializeField] private bool holdingTabbedMenu = false;
		[SerializeField] private bool holdingInteractionWheel = false;

		[Header("Interaction-Related:")]
		[SerializeField] private int changingInteractionInDirection = 0;
		[SerializeField] private int movingCurrentInteractionsPaletteInDirection = 0;

		private InputDirectionHelper m_lookingInputDirectionHelper;
		private InputDirectionHelper m_horizontalMovementInputDirectionHelper;
		
		private Vector2 lookingInputDirection => m_lookingInputDirectionHelper.direction;
		private Vector2 horizontalMovementInputDirection => m_horizontalMovementInputDirectionHelper.direction;


		public void SetLookingDirection(Vector2 newLookingDirection)
		{
			if (m_lookingInputDirectionHelper.SetDirection(newLookingDirection))
				eventsChannel?.FireLookTowards(lookingInputDirection);
		}
		
		public void SetMovingDirection(Vector2 newMovingDirection)
		{
			if (m_horizontalMovementInputDirectionHelper.SetDirection(newMovingDirection))
				eventsChannel?.FireMoveInDirection(horizontalMovementInputDirection);
		}

		public void SetJumping(bool value)
		{
			if (jumping != value)
			{
				jumping = value;
				eventsChannel?.FireJump(jumping);
			}
		}

		public void SetSprinting(bool value)
		{
			if (sprinting != value)
			{
				sprinting = value;
				eventsChannel?.FireSprint(sprinting);
			}
		}

		public void SetHoldingPrimary(bool value)
		{
			if (holdingPrimary != value)
			{
				holdingPrimary = value;
				eventsChannel?.FirePrimary(holdingPrimary);
			}
		}


		public void SetHoldingSecondary(bool value)
		{
			if (holdingSecondary != value)
			{
				holdingSecondary = value;
				eventsChannel?.FireSecondary(holdingSecondary);
			}
		}



		public void SetHoldingShowInfo(bool value)
		{
			if (holdingShowInfo != value)
			{
				holdingShowInfo = value;
				eventsChannel?.FireShowInfo(holdingShowInfo);
			}
		}

		public void SetHoldingPause(bool value)
		{
			if(holdingPause != value)
			{
				holdingPause = value;
				eventsChannel?.FirePauseMenu(value);
			}
		}

		public void SetHoldingTabbedMenu(bool value)
		{
			if(holdingTabbedMenu != value)
			{
				holdingTabbedMenu = value;
				eventsChannel?.FireTabbedMenu(value);
			}
		}

		public void SetHoldingInteractionWheel(bool value)
		{
			if (holdingInteractionWheel != value)
			{
				holdingInteractionWheel = value;
				eventsChannel?.FireInteractionWheel(value);
			}
		}
		

		
		public void SetChangingInteractionInDirection(int direction)
		{
			changingInteractionInDirection = direction;
			if (changingInteractionInDirection != 0)
				eventsChannel?.FireChangeInteractionInDirection(changingInteractionInDirection);
		}
		
		public void SetMovingCurrentInteractionsPaletteInDirection(int direction)
		{
			movingCurrentInteractionsPaletteInDirection = direction;
			if (movingCurrentInteractionsPaletteInDirection != 0)
				eventsChannel?.FireMoveCurrentInteractionsPalette(movingCurrentInteractionsPaletteInDirection);
		}



		public virtual void SetLookingDirection(InputAction.CallbackContext callbackContext)
			=> SetLookingDirection(callbackContext.ReadValue<Vector2>());
		public virtual void SetMovingDirection(InputAction.CallbackContext callbackContext)
			=> SetMovingDirection(callbackContext.ReadValue<Vector2>());
		public virtual void SetJumping(InputAction.CallbackContext callbackContext)
			=> SetJumping(callbackContext.ReadValueAsButton());
		public virtual void SetSprinting(InputAction.CallbackContext callbackContext)
			=> SetSprinting(callbackContext.ReadValueAsButton());
		public virtual void SetHoldingPrimary(InputAction.CallbackContext callbackContext)
			=> SetHoldingPrimary(callbackContext.ReadValueAsButton());
		public virtual void SetHoldingSecondary(InputAction.CallbackContext callbackContext)
			=> SetHoldingSecondary(callbackContext.ReadValueAsButton());

		public virtual void SetHoldingShowInfo(InputAction.CallbackContext callbackContext)
			=> SetHoldingShowInfo(callbackContext.ReadValueAsButton());
		public virtual void SetHoldingPause(InputAction.CallbackContext callbackContext)
			=> SetHoldingPause(callbackContext.performed);
		public virtual void SetHoldingTabbedMenu(InputAction.CallbackContext callbackContext)
			=> SetHoldingTabbedMenu(callbackContext.performed);
		public virtual void SetHoldingInteractionWheel(InputAction.CallbackContext callbackContext)
			=> SetHoldingInteractionWheel(callbackContext.ReadValueAsButton());

		public void SetChangingInteractionInDirection(InputAction.CallbackContext callbackContext)
			=> SetChangingInteractionInDirection(getIntFromFloatCallbackContext(callbackContext));
		public void SetMovingCurrentInteractionsPaletteInDirection(InputAction.CallbackContext callbackContext)
			=> SetMovingCurrentInteractionsPaletteInDirection(getIntFromFloatCallbackContext(callbackContext));



		private void Awake()
			=> Reset();

		private void Reset()
		{
			if (m_lookingInputDirectionHelper == null)
				m_lookingInputDirectionHelper = new InputDirectionHelper();
			else
				m_lookingInputDirectionHelper.Reset();

			if (m_horizontalMovementInputDirectionHelper == null)
				m_horizontalMovementInputDirectionHelper = new InputDirectionHelper();
			else
				m_horizontalMovementInputDirectionHelper.Reset();

			jumping = false;
			sprinting = false;
			holdingPrimary = false;
			holdingSecondary = false;
		}


		private int getIntFromFloatCallbackContext(InputAction.CallbackContext callbackContext)
			=> Mathf.CeilToInt(callbackContext.ReadValue<float>());
	}


	public class InputDirectionHelper
	{
		public Vector2 direction { get; private set; } = Vector2.zero;
		
		private float m_directionMagnitude = 0f;


		public bool SetDirection(Vector2 newDirection)
		{
			float newDirMag = newDirection.magnitude;
			if (m_directionMagnitude == 0f && newDirMag == 0f)
				return false;
			direction = newDirection;
			m_directionMagnitude = newDirMag;
			return true;
		}

		public void Reset()
			=> direction = Vector2.zero;
	}
}