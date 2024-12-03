using UnityEngine;


namespace CEIT.Raycasts
{
	public class CameraBasedPhysicsRaycaster : PhysicsRaycaster
	{
		[Header("Scene Elements:")]
		public Camera Origin;
		public LayerMask TargetLayers;

		protected override float maxRayDistance => Stats.MaxReachDistance;

		private static Vector3 viewportCenter = new Vector3(.5f, .5f, .5f);


		protected override Ray makeRay()
			=> Origin.ViewportPointToRay(viewportCenter);

		protected override bool tryGetRaycastHit(Ray ray, out RaycastHit rHit, ShotFilter shotFilter)
			=> Physics.Raycast(ray, out rHit, Stats.MaxReachDistance, TargetLayers, triggersFilter(shotFilter));
	}
}