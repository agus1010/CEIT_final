using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

using CEIT.TimeAndSpace;


namespace CEITUI.Elements.Radials
{
	public class RadialHandle : MonoBehaviour, IDragHandler
	{
		public GeoTranslationSystem geoTranslation;

		[Space(10)]
		[SerializeField] private float value = 0f;

		[Space(10)]
		public UnityEvent<float> OnValueChanged;


		public void SetValue(float value)
		{
			bool fireEvent = value != this.value;
			SetValueWithoutNotify(value);
			if (fireEvent)
				OnValueChanged?.Invoke(value);
		}

		public void SetValueWithoutNotify(float value)
		{
			this.value = value;
			transform.rotation = Quaternion.Euler(Vector3.forward * value);
		}

		private Vector3 center => transform.position;


		// Reference: https://www.youtube.com/watch?v=7c68z05vaX4
		public void OnDrag(PointerEventData eventData)
		{
			var angle = calcAngleFrom(eventData.position);
			SetValue(angle);
		}


		private void OnEnable()
		{
			SetValueWithoutNotify(geoTranslation.angle);
		}

		private float calcAngleFrom(Vector3 position)
		{
			var dirTowardsPosition = new Vector3
				(
					position.x - center.x,
					position.y - center.y,
					0f
				);
			float angle = Mathf.Atan2(dirTowardsPosition.y, dirTowardsPosition.x) * Mathf.Rad2Deg;
			angle -= 90f;
			if (angle < 0f) angle += 360f;
			return angle;
		}
	}
}