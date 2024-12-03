using UnityEngine;


namespace CEIT.Utils
{
	public class SphereOrbitter : MonoBehaviour
	{
		public Transform SphereCenter;
		public float SphereRadius;

		public Camera PlayerCamera;

		public Vector3 Offset = Vector3.zero;

		private void Update()
		{
			transform.position = (new Ray(SphereCenter.position + Offset, -1 * PlayerCamera.transform.forward).GetPoint(SphereRadius));
		}
	}
}