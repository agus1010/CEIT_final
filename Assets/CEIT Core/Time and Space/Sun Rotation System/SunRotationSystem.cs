using UnityEngine;


namespace CEIT.TimeAndSpace
{
	[CreateAssetMenu(fileName = "New Sun Rotation System", menuName = "CEIT/Systems/Sun Rotation System")]
	public class SunRotationSystem : ScriptableObject
	{
		public TimeSystem timeSystem;
		public GeoTranslationSystem geoTranslationSystem;

		public SunRotationSystemEventsChannel eventsChannel;

		public Quaternion CurrentSunRotation { get; private set; } = Quaternion.identity;

		public float xRotation { get; private set; } = 0f;
		public float yRotation { get; private set; } = 0f;


		public void Reset()
		{
			Vector3 eulerRot = new Vector3
				(
					MathUtils.SecondsToAngle(timeSystem.totalSeconds),
					geoTranslationSystem.angle,
					0f
				);
			CurrentSunRotation = Quaternion.Euler(eulerRot);
		}


		public void SetConfig(Transform light)
		{
			xRotation = light.eulerAngles.x;
			yRotation = light.eulerAngles.y;
		}

		public void SetConfig(Quaternion rotation)
		{
			xRotation = rotation.eulerAngles.x;
			yRotation = rotation.eulerAngles.y;
		}


		public void RotateDelta(float xAngle, float yAngle)
		{
			if(xAngle != 0f || yAngle != 0f)
			{
				RotateDeltaWithoutNotify(xAngle, yAngle);
				eventsChannel.FireRotationChanged(CurrentSunRotation);
			}
		}

		public void RotateDelta(Vector3 deltaEulerAngles)
			=> RotateDelta(deltaEulerAngles.x, deltaEulerAngles.y);

		public void RotateDelta(float deltaAngle, Vector3 axis)
			=> RotateDelta(axis * deltaAngle);

		public void RotateDelta(Quaternion deltaRotation)
			=> RotateDelta(deltaRotation.eulerAngles);


		public void RotateDeltaWithoutNotify(float xDeltaAngle, float yDeltaAngle)
		{
			xRotation += xDeltaAngle;
			yRotation += yDeltaAngle;
			Quaternion rot = calcTargetRot(xRotation, yRotation);
			CurrentSunRotation = rot;
		}

		public void RotateDeltaWithoutNotify(Vector3 deltaEulerAngles)
			=> RotateDeltaWithoutNotify(deltaEulerAngles.x, deltaEulerAngles.y);

		public void RotateDeltaWithoutNotify(float deltaAngle, Vector3 axis)
			=> RotateDeltaWithoutNotify(axis * deltaAngle);

		public void RotateDeltaWithoutNotify(Quaternion deltaRotation)
			=> RotateDeltaWithoutNotify(deltaRotation.eulerAngles);


		public void SetAngleInAxis(float angle, Vector3 axis)
		{
			SetAngleInAxisWithoutNotify(angle, axis);
			eventsChannel?.FireRotationChanged(CurrentSunRotation);
		}
		
		public void SetAngleInAxisWithoutNotify(float angle, Vector3 axis)
		{
			xRotation = axis.x != 0 ? angle : xRotation;
			yRotation = axis.y != 0 ? angle : yRotation;
			Quaternion rot = calcTargetRot(xRotation, yRotation);
			CurrentSunRotation = rot;
		}



		public void RotateTo(Quaternion targetRotation)
		{
			if(targetRotation != CurrentSunRotation)
			{
				xRotation = targetRotation.eulerAngles.x;
				yRotation = targetRotation.eulerAngles.y;
				setRotation(targetRotation);
			}
		}



		private Quaternion calcTargetRot(float xAngle, float yAngle)
		{
			Quaternion rot = Quaternion.AngleAxis(yAngle, Vector3.up);
			rot *= Quaternion.AngleAxis(xAngle, Vector3.right);
			return rot;
		}
		private void setRotation(Quaternion rotation)
		{
			CurrentSunRotation = rotation;
			eventsChannel.FireRotationChanged(CurrentSunRotation);
		}
	}
}