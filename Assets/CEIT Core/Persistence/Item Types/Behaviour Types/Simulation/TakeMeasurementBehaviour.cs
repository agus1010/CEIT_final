using UnityEngine;

using CEIT.Player;
using CEIT.Assets.Interactables;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Take Measurement Behaviour", menuName = "CEIT/Persistence/Behaviours/Take Measurement")]
	public class TakeMeasurementBehaviour : InteractionBehaviour
	{
		[SerializeField] private GameObject distanceMeasurementPrefab;

		public GameObject measurementPrefab => distanceMeasurementPrefab;
		public PlayerPointer pointer { get; private set; }
		public DistanceMeasurement current { get; private set; }
		public Helpers.TakeMeasurementStateMachine stateMachine { get; private set; }

		private bool isMeasuring => current != null;


		#region Interaction Behaviour methods
		public override void Initialize(Interaction interaction, PlayerPointer pointer)
		{
			this.pointer = pointer;
			current = null;
			stateMachine = new Helpers.TakeMeasurementStateMachine(this, interaction.actionsDescriptor as TakeMeasurementInteractionActionsDescriptor);
		}

		public override void Stop()
		{
			if (current != null)
				current.CancelMeasuring();
			pointer = null;
			current = null;
		}

		public override void TryPerformPrimary(bool newPrimaryValue)
		{
			stateMachine.Primary(newPrimaryValue);
		}

		public override void TryPerformSecondary(bool newSecondaryValue)
		{
			stateMachine.Secondary(newSecondaryValue);
		}

		public override void OnUpdate()
		{
			if(current != null)
			{
				current.UpdateMeasurement(calcPointerTarget());
			}
		}
		#endregion


		public bool StartMeasuring()
		{
			bool canStartMeasuring = current == null && pointer.ClosestTarget != null && !pointer.IsLookingAtGraphics;
			if (canStartMeasuring)
			{
				var newGo = Instantiate(measurementPrefab);
				var point = pointer.CurrentPhysicsShot.Point;
				newGo.transform.position = point;
				current = newGo.GetComponent<DistanceMeasurement>();
				current.StartMeasuring(point);
				pointer.ShotMode = Raycasts.ShotFilter.SOLIDS;
			}
			return canStartMeasuring;
		}

		public void StopMeasuring()
		{
			if (isMeasuring)
			{
				current.StopMeasuring(calcPointerTarget());
				current = null;
				pointer.ShotMode = Raycasts.ShotFilter.ALL;
			}
		}

		public void CancelMeasurement()
		{
			if (isMeasuring)
			{
				current.CancelMeasuring();
				current = null;
				pointer.ShotMode = Raycasts.ShotFilter.ALL;
			}
		}

		public void TryDestroyMeasurement()
		{
			if (pointer.IsLookingAtGraphics)
			{
				GameObject target = pointer.ClosestTarget;
				if (target.CompareTag("DistanceMeasurement"))
				{
					Utils.Destructor destructor;
					if (!target.TryGetComponent(out destructor))
					{
						destructor = target.GetComponentInParent<Utils.Destructor>();
					}
					destructor.CallDestroy();
				}
			}
		}



		private Vector3 calcPointerTarget()
		{
			return pointer.ClosestTarget == null || pointer.IsLookingAtGraphics ?
					new Ray(pointer.transform.position, pointer.transform.forward).GetPoint(pointer.CurrentPhysicsShot.MaxDistance) :
					pointer.CurrentPhysicsShot.Point;
		}
	}
}