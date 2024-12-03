using UnityEngine;

using CEIT.Extensions;
using Unity.VisualScripting;

namespace CEIT.TimeAndSpace
{
	public enum CardinalPoint
	{
		East,
		NorthEast,
		North,
		NorthWest,
		West,
		SouthWest,
		South,
		SouthEast
	}

	[CreateAssetMenu(fileName = "New Geo Translation System", menuName = "CEIT/Systems/Geo Translation System")]
	public class GeoTranslationSystem : ScriptableObject
	{
		public GeoTranslationSystemEventsChannel eventsChannel;

		public SunRotationSystem sunRotationSystem;

		public float angle { get; private set; } = 0;
		public CardinalPoint orientedTowards
		{
			get
			{
				float proportion = 360 * (1 / angle);

				if (proportion <= 22.4f && (360 - proportion) >= 337.5f)
					return CardinalPoint.East;
				if (proportion.BetweenOrEqual(22.5f, 67.4f))
					return CardinalPoint.NorthEast;
				if (proportion.BetweenOrEqual(67.5f, 112.4f))
					return CardinalPoint.North;
				if (proportion.BetweenOrEqual(112.5f, 157.4f))
					return CardinalPoint.NorthWest;
				if (proportion.BetweenOrEqual(157.5f, 202.4f))
					return CardinalPoint.West;
				if (proportion.BetweenOrEqual(202.5f, 247.4f))
					return CardinalPoint.SouthWest;
				if (proportion.BetweenOrEqual(247.5f, 292.4f))
					return CardinalPoint.South;

				return CardinalPoint.SouthEast;
			}
		}


		public void Reset()
		{
			angle = 0;
		}

		public void SetTranslation(float targetAngle)
		{
			if (targetAngle != angle)
			{
				SetTranslationWithoutNotify(targetAngle);
				eventsChannel.FireGeoTranslationChanged(targetAngle);
			}
		}
		public void SetTranslation(Vector3 targetEulerAngles)
			=> SetTranslation(targetEulerAngles.y);
		public void SetTranslation(Quaternion targetRotation)
			=> SetTranslation(targetRotation.eulerAngles);

		public void SetTranslationWithoutNotify(float targetAngle)
		{
			float delta = targetAngle - angle;
			angle = targetAngle;
			sunRotationSystem.RotateDelta(0f, delta);
		}
		public void SetTranslationWithoutNotify(Vector3 targetEulerAngles)
			=> SetTranslationWithoutNotify(targetEulerAngles.y);
		public void SetTranslationWithoutNotify(Quaternion targetRotation)
			=> SetTranslationWithoutNotify(targetRotation.eulerAngles);

		public void TranslateDelta(float deltaAngle)
		{
			if(deltaAngle != 0f)
				SetTranslation(angle + deltaAngle);
		}
		public void TranslateDelta(Vector3 deltaEulerAngles)
			=> TranslateDelta(deltaEulerAngles.y);
		public void TranslateDelta(Quaternion deltaRotation)
			=> TranslateDelta(deltaRotation.eulerAngles);
	}
} 