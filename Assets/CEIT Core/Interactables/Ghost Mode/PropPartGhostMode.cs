using UnityEngine;


namespace CEIT.Interactables
{
	public class PropPartGhostMode : AutomaticGhostMode
	{
		public bool canActivate;

		private PropPartBehaviour propPart;


		public override void Activate()
		{
			if(canActivate)
				base.Activate();
		}

		public override void Deactivate()
		{
			surfaceHistory.Locked = propPart.surfaceHistoryShouldBeLocked;
			base.Deactivate();
		}


		protected override void Awake()
		{
			base.Awake();
			propPart = GetComponent<PropPartBehaviour>();
		}

		protected override void OnTriggerEnter(Collider other)
		{
			if(colliderIsAnotherProp(other))
			{
				base.OnTriggerEnter(other);
			}
		}

		protected override void OnTriggerExit(Collider other)
		{
			if (colliderIsAnotherProp(other))
			{
				base.OnTriggerExit(other);
			}
		}


		private bool colliderIsAnotherProp(Collider other)
		{
			bool isPropBehaviour = other.TryGetComponent(out PropBehaviour propBehaviour);
			bool isPropPartBehaviour = other.TryGetComponent(out PropPartBehaviour propPartBehaviour);

			if (!(isPropBehaviour || isPropPartBehaviour))
				return false;

			string uid = isPropBehaviour || isPropPartBehaviour ?
							isPropBehaviour ? propBehaviour.instanceUid : propPartBehaviour.propBehaviour.instanceUid :
							"None";
			
			return propPart.propBehaviour.instanceUid != uid;
		}
	}
}