using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using CEIT.Extensions;


namespace CEITUI.Animations
{
	public class ExpandChildsNearTarget : MonoBehaviour
	{
		[Range(0f, 1024f)] public float MinSize = 100f;
		[Range(0f, 1024f)] public float TargetSize = 150f;
		public Transform TargetCenter;
		public float GrowingDistance = 125f;

		private IEnumerable<System.Tuple<RectTransform, Vector3>> elements;


		private void Reset()
		{
			Vector2 minSizeDelta = new Vector2(MinSize, MinSize);

			elements = transform.GetDirectChilds()
				.Select(t => new System.Tuple<RectTransform, Vector3>(t as RectTransform, minSizeDelta));
		}

		private void Start()
		{
			Reset();
			Update();
		}

		private void Update()
		{
			foreach (var rt in elements)
			{
				if (rt.Item1.DistanceTo(TargetCenter) < GrowingDistance)
				{
					lerpScale(rt.Item1, rt.Item2, distanceToCenterCoefficient(rt.Item1));
				}
				else
				{
					rt.Item1.sizeDelta = rt.Item2;
				}
			}
		}


		private void lerpScale(RectTransform rt, Vector3 minSize, float t)
			=> rt.sizeDelta = new Vector2
				(
					Mathf.Lerp(minSize.x, TargetSize, t),
					Mathf.Lerp(minSize.y, TargetSize, t)
				);

		private float distanceToCenterCoefficient(RectTransform rt)
			=> 1 - (rt.DistanceTo(TargetCenter) * 100 / GrowingDistance) / 100;


		private bool m_green = true;
		private void OnDrawGizmosSelected()
		{
			if (TargetCenter != null)
			{
				Reset();
				Update();
				foreach (var t in elements)
				{
					Gizmos.color = m_green ? Color.green : Color.yellow;
					m_green = !m_green;
					if (t.Item1.DistanceTo(TargetCenter) < GrowingDistance)
						Gizmos.DrawLine(t.Item1.position, TargetCenter.position);
				}
				m_green = true;
			}
		}
	}
}