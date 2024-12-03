using UnityEngine;


namespace CEIT.Raycasts
{
	public class PhysicsShotResult : IPhysicsShotResult
	{
		public float Distance { get; private set; } = float.MaxValue;
		public bool Hit { get; private set; } = false;
		public GameObject Target { get; private set; } = null;
		public float MaxDistance { get; private set; }
		public Ray Ray { get; private set; }
		public Vector3 Point { get; private set; }
		public Vector3 Normal { get; private set; }
		public Collider Collider { get; private set; }


		public PhysicsShotResult() { }

		public PhysicsShotResult(Ray ray, float maxRayDistance)
		{
			Distance = maxRayDistance;
			Hit = false;
			Target = null;
			MaxDistance = maxRayDistance;
			Ray = ray;
			Point = Vector3.zero;
			Normal = Vector3.zero;
			Collider = null;
		}

		public PhysicsShotResult(Ray ray, float maxRayDistance, RaycastHit raycastHit)
		{
			Distance = raycastHit.distance;
			Hit = true;
			Target = raycastHit.transform.gameObject;
			MaxDistance = maxRayDistance;
			Ray = ray;
			Point = raycastHit.point;
			Normal = raycastHit.normal;
			Collider = raycastHit.collider;
		}
	}
}