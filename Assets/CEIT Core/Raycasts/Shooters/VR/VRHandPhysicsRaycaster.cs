using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


namespace CEIT.Raycasts
{
	public class VRHandPhysicsRaycaster : PhysicsRaycaster
	{
		[Header("Scene Elements:")]
		public XRRayInteractor xrRayInteractor;

		protected override float maxRayDistance => xrRayInteractor.maxRaycastDistance;

		
		protected override Ray makeRay()
			=> new Ray(xrRayInteractor.rayOriginTransform.position, xrRayInteractor.rayOriginTransform.forward);

		protected override bool tryGetRaycastHit(Ray ray, out RaycastHit rHit, ShotFilter shotFilter)
		{
			xrRayInteractor.raycastTriggerInteraction = triggersFilter(shotFilter);
			return xrRayInteractor.TryGetCurrent3DRaycastHit(out rHit);
		}


		private void Start()
		{
			xrRayInteractor.maxRaycastDistance = Stats.MaxReachDistance;
		}
	}
}