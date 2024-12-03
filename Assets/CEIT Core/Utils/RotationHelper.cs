using UnityEngine;


namespace CEIT.Utils
{
	public class RotationHelper : MonoBehaviour
	{
		public void SetRotationInX(float angle)
			=> setRotation(Vector3.right, angle);

		public void SetRotationInY(float angle)
			=> setRotation(Vector3.up, angle);


		private void setRotation(Vector3 axis, float angle)
			=> transform.rotation = makeRotation(axis, angle);


		private Quaternion makeRotation(Vector3 axis, float angle)
		{
			Vector3 currentEulers = transform.rotation.eulerAngles;
			Vector3 targetEulers = new Vector3
				(
					axis.x != 0 ? angle : currentEulers.x,
					axis.y != 0 ? angle : currentEulers.y,
					axis.z != 0 ? angle : currentEulers.z
				);
			return Quaternion.Euler(targetEulers);
		}
	}
}