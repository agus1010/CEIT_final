using UnityEngine;
using UnityEngine.EventSystems;

using CEIT.Extensions;
using CEIT.TimeAndSpace;


namespace CEITUI.Elements.Radials
{
	public class RadialSlider : MonoBehaviour, IDragHandler
	{
		public float Radius = 100f;

		[SerializeField] private RectTransform handle;

		private float value;

		public CardinalPoints pointingTowards;
		public float Value
		{
			get => value;
			set
			{
				this.value = value;
				setCardinalPoint(value);
			}
		}

		public Vector3 center => transform.position;
		public Vector3 HandleCenter => handle.position;

		private Vector2 upwards => transform.DirectionTowards(center + Vector3.up * Radius);
		private Vector2 rightwards => transform.DirectionTowards(center + Vector3.right * Radius);


		public void Set(float value)
		{
			Value = value;
		}

		public void OnDrag(PointerEventData eventData)
		{
			if (handle == null) return;

			var handleDir = limitPositionToRadius(eventData.position);
			handle.position = handleDir;


			var angle = Vector2.SignedAngle(rightwards, directionFromCenter(handle.position));

			if (angle < 0)
				angle += 360;

			Value = angle;
		}


		private void Start()
		{
			handle.position = limitPositionToRadius(handle.position);
			setCardinalPoint(Value);
		}

		private void setCardinalPoint(float angle)
		{
			if (angle >= 315 || angle < 45)
			{
				pointingTowards = CardinalPoints.East;
			}
			else
			{
				if (angle >= 45 && angle < 135)
				{
					pointingTowards = CardinalPoints.North;
				}
				else
				{
					if (angle >= 135 && angle < 225)
					{
						pointingTowards = CardinalPoints.West;
					}
					else
					{
						pointingTowards = CardinalPoints.South;
					}
				}
			}
		}

		private Vector3 directionFromCenter(Vector3 position)
			=> transform.DirectionTowards(position);

		private Vector3 limitPositionToRadius(Vector3 position)
			=> new Ray(center, directionFromCenter(position)).GetPoint(Radius);
	}
}