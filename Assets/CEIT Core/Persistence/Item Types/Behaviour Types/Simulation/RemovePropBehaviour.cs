using UnityEngine;

using CEIT.Player;
using CEIT.Interactables;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Remove Prop Behaviour", menuName = "CEIT/Persistence/Behaviours/Remove Prop")]
	public class RemovePropBehaviour : InteractionBehaviour
	{
		private PlayerPointer pointer;
		public float delayBetweenDestroys = 0.1f;

		private bool currentSecondaryValue = false;
		private float timeToWaitToDestroy = 0f;


		public override void Initialize(Interaction interaction, PlayerPointer pointer)
		{
			this.pointer = pointer;
			this.pointer.ShotMode = Raycasts.ShotFilter.ALL;
		}

		public override void TryPerformPrimary(bool newPrimaryValue)
		{
			if(newPrimaryValue)
			{
				tryDestroyPointersTarget();
			}
		}

		public override void TryPerformSecondary(bool newSecondaryValue)
		{
			currentSecondaryValue = newSecondaryValue;
			timeToWaitToDestroy = 0f;
		}

		public override void OnUpdate()
		{
			if(currentSecondaryValue)
			{
				timeToWaitToDestroy -= Time.deltaTime;
				if (timeToWaitToDestroy <= 0f)
				{
					tryDestroyPointersTarget();
					timeToWaitToDestroy = delayBetweenDestroys;
				}
			}
		}


		private void tryDestroyPointersTarget()
		{
			if (pointer.CurrentPhysicsShot.Hit && !pointer.IsLookingAtGraphics)
			{
				bool targetIsProp = pointer.ClosestTarget.TryGetComponent(out PropBehaviour propBehaviour);
				bool targetIsPropPart = pointer.ClosestTarget.TryGetComponent(out PropPartBehaviour propPartBehaviour);
				if (targetIsProp || targetIsPropPart)
				{
					var target = targetIsProp ? propBehaviour : propPartBehaviour.propBehaviour;
					Destroy(target.gameObject);
				}
			}
		}
	}
}