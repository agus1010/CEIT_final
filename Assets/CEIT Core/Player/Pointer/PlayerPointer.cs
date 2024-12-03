using UnityEngine;

using CEIT.Raycasts;


namespace CEIT.Player
{
    public class PlayerPointer : MonoBehaviour
    {
		public PlayerPointerEventsChannel eventsChannel;

		public PlayerRotationIntentProvider rotationIntentProvider;

		public RaycastersSwitch physicsRaycastersSwitch;
		public RaycastersSwitch uiRaycastersSwitch;

		public GameObject ClosestTarget { get; private set; }
		public bool IsLookingAtGraphics { get; private set; } = false;
		public PhysicsShotResult CurrentPhysicsShot { get; private set; } = new PhysicsShotResult();
		public UIShotResult CurrentUIShot { get; private set; } = new UIShotResult();

		[SerializeField] private ShotFilter shotMode = ShotFilter.SOLIDS;
		public ShotFilter ShotMode { get => shotMode; set => shotMode = value; }

		[Header("Debug:")]
		[SerializeField] private bool debug = false;


		public void SetShotMode(int enumIndex)
		{
			shotMode = (ShotFilter)enumIndex;
		}


		private void Start()
		{
			shootAllRaycasters();
		}

		private void Update()
		{
			shootAllRaycasters();
		}


		private void shootAllRaycasters()
		{
			CurrentPhysicsShot = physicsRaycastersSwitch.Shoot(ShotMode) as PhysicsShotResult;
			CurrentUIShot = uiRaycastersSwitch.Shoot() as UIShotResult;
			IsLookingAtGraphics = isLookingAtGraphics(CurrentPhysicsShot, CurrentUIShot);
			ClosestTarget = IsLookingAtGraphics ? CurrentUIShot.Target : CurrentPhysicsShot.Target;
			eventsChannel.FireNewTargetDetected(ClosestTarget);
			if (IsLookingAtGraphics)
				eventsChannel.FireNewUITargetDetected(ClosestTarget);
			else
				if(CurrentPhysicsShot.Hit)
					eventsChannel.FireNewPhysicsTargetDetected(ClosestTarget);
			if (debug)
				print(ClosestTarget != null? ClosestTarget.name : "Nothing");
		}

		private bool isLookingAtGraphics(PhysicsShotResult physicsShot, UIShotResult uiShot)
		{
			if(uiShot.Hit)
			{
				if (!physicsShot.Hit)
					return true;
				return uiShot.Distance < physicsShot.Distance;
			}
			return false;
		}
	}
}