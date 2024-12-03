using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace CEIT.Extensions
{
	public static class TransformExtensions
	{
		public static IEnumerable<Transform> GetDirectChilds(this Transform t)
			=> new Range(0, t.childCount).Select(i => t.GetChild(i));

		public static IEnumerable<RectTransform> GetDirectChilds(this RectTransform rt)
			=> GetDirectChilds(rt as Transform).Cast<RectTransform>();

		public static float DistanceTo(this Transform obj, Transform t)
			=> Vector3.Distance(obj.position, t.position);

		public static Vector3 DirectionTowards(this Transform obj, Transform t)
			=> DirectionTowards(obj, t.position);

		public static Vector3 DirectionTowards(this Transform obj, Vector3 position)
			=> DirectionTowards(obj.position, position);

		public static Vector3 DirectionTowards(this Vector3 obj, Vector3 position)
			=> new Vector3
			(
				position.x - obj.x,
				position.y - obj.y,
				position.z - obj.z
			);
	}
}