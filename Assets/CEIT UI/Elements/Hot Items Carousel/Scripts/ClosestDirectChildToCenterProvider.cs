using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CEIT.Extensions;


namespace CEITUI.Utils
{
	public class ClosestDirectChildToCenterProvider : MonoBehaviour
	{
		public Transform TargetCenter;

		public IEnumerable<Transform> Childs { get; private set; }

		public float ClosestDistanceToCenter { get; private set; }
		public Transform ClosestChildToCenter { get; private set; }

		public bool IsToTheRight { get; private set; } = false;

		private void Reset()
		{
			Childs = transform.GetDirectChilds();
		}

		private void OnEnable()
		{
			Reset();
			Update();
		}

		private float m_tempDistance;
		private void Update()
		{
			ClosestDistanceToCenter = float.MaxValue;
			foreach (var t in Childs)
			{
				m_tempDistance = t.DistanceTo(TargetCenter);
				if(m_tempDistance < ClosestDistanceToCenter)
				{
					ClosestDistanceToCenter = m_tempDistance;
					ClosestChildToCenter = t;
					IsToTheRight = isToTheRightOfCenter(t);
				}
			}
		}

		private bool isToTheRightOfCenter(Transform t)
			=> Vector3.Dot(TargetCenter.right, t.DirectionTowards(TargetCenter)) <= 0;
	}
}