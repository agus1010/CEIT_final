using UnityEngine;


namespace CEIT.Extensions
{
	public static class StructExtensions
	{
		public static float[] ToFloatArray(this Vector3 v)
			=> new float[] { v.x, v.y, v.z };

		public static float[] ToFloatArray(this Quaternion q)
			=> new float[] { q.x, q.y, q.z, q.w };

		public static Vector3 ToVector3(this float[] values)
			=> new Vector3(values[0], values[1], values[2]);

		public static Vector3 GetAPerpendicular(this Vector3 obj)
		{
			Vector3 perpendicular = new Vector3(obj.y, obj.z, obj.x);
			Vector3 cross = Vector3.Cross(obj, perpendicular);
			if (cross == Vector3.zero)
				perpendicular = Vector3.right;
			return perpendicular;
			//(1,0,0) -> (0,0,1)
			//(1,1,0) -> (1,0,1)
		}

		public static Quaternion Inverse(this Quaternion quaternion)
			=> Quaternion.Inverse(quaternion);

		public static Quaternion ToQuaternion(this float[] values)
			=> new Quaternion(values[0], values[1], values[2], values[3]);


		public static Quaternion GetRotationCancellationInAxis(this Quaternion q, Vector3 axis)
		{
			Vector3 eulers = q.eulerAngles;
			Vector3 cancelV = new Vector3(eulers.x * axis.x, eulers.y * axis.y, eulers.z * axis.z);
			Quaternion cancelQ = Quaternion.Inverse(Quaternion.Euler(cancelV));
			return cancelQ;
		}
	}
}