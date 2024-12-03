using UnityEngine;
using UnityEngine.EventSystems;


namespace CEIT.Raycasts
{
	public class XRRaycastShotResult : IPhysicsShotResult, IUIShotResult
	{
		public float MaxDistance { get; private set; }
		public Ray Ray { get; private set; }
		public Vector3 Point { get; private set; }
		public Vector3 Normal { get; private set; }
		public RaycastHit RaycastHit { get; private set; }
		public float Distance { get; private set; }
		public bool Hit { get; private set; }
		public GameObject Target { get; private set; }
		public Collider Collider { get; private set; }
		public bool UIIsClosest = false;


		public XRRaycastShotResult() { }

		public XRRaycastShotResult(Transform origin, RaycastHit? raycastHit, int raycastHitIndex, RaycastResult? raycastResult, int raycastResultIndex, bool uiIsClosest)
		{
			Hit = true;
			Ray = new Ray(origin.position, origin.forward);
			if (raycastHit != null || raycastResult != null)
			{
				if (raycastResult == null)
				{
					RaycastHit rHit = raycastHit.Value;
					Target = rHit.collider.gameObject;
					Distance = rHit.distance;
					Point = rHit.point;
					Normal = rHit.normal;
					Collider = rHit.collider;
				}
				else
				{
					if (raycastHit == null)
					{
						Point = raycastResult.Value.worldPosition;
					}
					else
					{

					}
				}
			}

			Point = raycastHit.Value.point;
			Point = raycastResult.Value.worldPosition;
		}


		private void _setValuesRResultIsNull()
		{

		}

		private void _setValuesRHitIsNull()
		{

		}

		private void _setValuesNoneIsNull()
		{

		}
	}
}