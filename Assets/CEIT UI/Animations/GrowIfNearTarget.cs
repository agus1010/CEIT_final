using UnityEngine;


namespace CEITUI.Animations
{
	public class GrowIfNearTarget : MonoBehaviour
	{
		public float GrowingDistance = 40f;
		public Vector2 TargetSize;

		[SerializeField] private Transform target;

		public float DistanceToCenter => Vector3.Distance(transform.position, target.position);
		public float DistanceToCenterCoefficient => 1 - (DistanceToCenter * 100 / GrowingDistance) / 100;
		public RectTransform RectTransform => transform as RectTransform;


		
		private Vector2 _initialSize;


		private void Start()
		{
			_initialSize = RectTransform.rect.size;
		}

		private void Update()
		{
			if (DistanceToCenter < GrowingDistance)
			{
				_lerpPreferredValues(_initialSize, TargetSize, DistanceToCenterCoefficient);
			}
			else
			{
				RectTransform.sizeDelta = _initialSize;
			}
		}

		private void _lerpPreferredValues(Vector2 minSize, Vector2 maxSize, float t)
		{
			RectTransform.sizeDelta = new Vector2
				(
					Mathf.Lerp(minSize.x, maxSize.x, t),
					Mathf.Lerp(minSize.y, maxSize.y, t)
				);
		}


		private void OnDrawGizmosSelected()
		{
			if (target == null) return;
			if (DistanceToCenter < GrowingDistance)
				Gizmos.DrawLine(transform.position, target.position);
		}
	}
}