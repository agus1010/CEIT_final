using UnityEngine;

using CEIT.TimeAndSpace;
using CEIT.Player;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Move Sun Behaviour", menuName = "CEIT/Persistence/Behaviours/Move Sun")]
	public class MoveSunBehaviour : InteractionBehaviour
	{
		public float RotationSpeed = 1f;

		public SunRotationSystem sunRotationSystem;

		public bool PerformingRotation => performingSunTranslation || performingSunRotation;
		private Vector3 playerEulerAngles => pointer.rotationIntentProvider.IntendedRotations;
		
		private bool performingSunRotation = false;
		private bool performingSunTranslation = false;
		private Vector3 previousFrameEulerRotation;

		private PlayerPointer pointer;


		public override void Initialize(Interaction interaction, PlayerPointer pointer)
		{
			this.pointer = pointer;
		}

		public override void Stop()
		{
			pointer = null;
		}


		public override void TryPerformPrimary(bool newPrimaryValue)
		{
			performingSunRotation = newPrimaryValue && pointer.ClosestTarget == null;
			previousFrameEulerRotation = playerEulerAngles;
		}

		public override void TryPerformSecondary(bool newSecondaryValue)
		{
			performingSunTranslation = newSecondaryValue && pointer.ClosestTarget == null;
			previousFrameEulerRotation = playerEulerAngles;
		}


		public override void OnUpdate()
		{
			if (PerformingRotation)
			{
				float rotDelta = 0f;
				if (performingSunRotation)
					rotDelta = calcSunDeltaRotation();

				float transDelta = 0f;
				if (performingSunTranslation)
					transDelta = calcSunDeltaTranslation();
				
				previousFrameEulerRotation = playerEulerAngles;
				sunRotationSystem.RotateDelta(rotDelta, transDelta);
			}
		}

		private float calcSunDeltaRotation()
		{
			float difference = previousFrameEulerRotation.x - playerEulerAngles.x;
			float angle = difference * Mathf.Sign(playerEulerAngles.y);
			return angle;
		}

		private float calcSunDeltaTranslation()
		{
			float translationDelta = previousFrameEulerRotation.y - playerEulerAngles.y;
			return -1 * translationDelta;
		}
	}
}