using UnityEngine;

using CEIT.Interactables;
using CEIT.Player;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Place Prop Behaviour", menuName = "CEIT/Persistence/Behaviours/Place Prop")]
	public class PlacePropBehaviour : InteractionBehaviour
	{
		public ItemPalette palette;
		public PlayerPointer pointer;

		public float rotationSpeed = 15f;

		public bool currentPropWasSpawned { get; protected set; } = false;
		public PropBehaviour heldProp { get; protected set; }

		public bool isHoldingAProp => heldProp != null;

		private Helpers.PlacePropBehaviourModesManager modesManager;


		public void CancelSpawning()
		{
			Destroy(heldProp.gameObject);
			heldProp = null;
			currentPropWasSpawned = false;
		}


		public void GrabProp()
		{
			if (!isHoldingAProp)
			{
				var closestTarget = pointer.ClosestTarget;
				if (!pointer.IsLookingAtGraphics && closestTarget != null)
				{
					if (closestTarget.TryGetComponent(out PropBehaviour propBehaviour))
					{
						propBehaviour.Accomodate();
						heldProp = propBehaviour;
					}
				}
			}
		}

		public void ReleaseProp()
		{
			if (isHoldingAProp)
			{
				heldProp.Place();
				heldProp = null;
				currentPropWasSpawned = false;
			}
		}

		public void RemoveProp()
		{
			if (isHoldingAProp)
			{
				Destroy(heldProp.gameObject);
				heldProp = null;
			}
			currentPropWasSpawned = false;
		}

		public void RotateProp(float value)
		{
			if(isHoldingAProp)
			{
				float direction = Mathf.Sign(value);
				heldProp.Rotate(Time.deltaTime + direction * rotationSpeed);
			}
		}

		public void SpawnCurrentProp()
		{
			var spawnedGO = Instantiate((palette.Current as Prop).prefab, pointer.CurrentPhysicsShot.Point, Quaternion.identity);
			heldProp = spawnedGO.GetComponent<PropBehaviour>();
			currentPropWasSpawned = true;
			heldProp.Snap(pointer.CurrentPhysicsShot.Point, pointer.CurrentPhysicsShot.Normal);
			heldProp.Align(pointer.CurrentPhysicsShot.Normal);
			heldProp.Accomodate();
		}



		#region behaviour messages
		public override void Initialize(Interaction interaction, PlayerPointer pointer)
		{
			palette = interaction.Palette;
			this.pointer = pointer;
			this.pointer.ShotMode = Raycasts.ShotFilter.ALL;
			modesManager = new Helpers.PlacePropBehaviourModesManager(this, interaction.actionsDescriptor as PlacePropInteractionActionsDescriptor);
			m_prevNormal = Vector3.zero;
		}

		public override void Stop()
		{
			modesManager.MoveToMode(modesManager.idleMode);
			heldProp = null;
			currentPropWasSpawned = false;
		}

		public override void TryPerformPrimary(bool newPrimaryValue)
		{
			modesManager.OnPrimary(newPrimaryValue);
		}

		public override void TryPerformSecondary(bool newSecondaryValue)
		{
			modesManager.OnSecondary(newSecondaryValue);
		}

		public override void TryPerformChangeInteractionValue(int direction)
		{
			RotateProp(direction * rotationSpeed);
		}

		public override void OnFixedUpdate()
		{
			if (isHoldingAProp)
			{
				updateHeldObjectPosition();
			}
		}
		#endregion


		private Vector3 m_prevNormal;
		private void updateHeldObjectPosition()
		{
			var solidsShot = pointer.CurrentPhysicsShot;
			if (solidsShot.Hit)
			{
				heldProp.Snap(solidsShot.Point, solidsShot.Normal);
				if(m_prevNormal != solidsShot.Normal)
				{
					m_prevNormal = solidsShot.Normal;
					heldProp.Align(solidsShot.Normal);
				}
			}
			else
			{
				var pointPosition = solidsShot.Ray.GetPoint(solidsShot.MaxDistance - 2f);
				Ray r = new Ray(pointPosition, Vector3.down);
				if (Physics.Raycast(r, out RaycastHit rh))
				{
					heldProp.Snap(rh.point, rh.normal);
				}
			}
		}
	}
}