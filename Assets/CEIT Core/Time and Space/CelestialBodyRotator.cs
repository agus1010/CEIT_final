using UnityEngine;


namespace CEIT.TimeAndSpace
{
    public class CelestialBodyRotator : MonoBehaviour
    {
        public SunRotationSystem sunRotationSystem;

        public Transform celestialBody;

        private float rotAngle = 0f;
        private float transAngle = 0f;


        public void Rotate(float deltaAngle)
        {
            RotateTo(rotAngle + deltaAngle);
        }

        public void RotateAndTranslate(float deltaRotationAngle, float deltaTranslationAngle)
        {
            RotateAndTranslateTo(rotAngle + deltaRotationAngle, transAngle + deltaTranslationAngle);
        }

        public void RotateAndTranslateTo(float rotationAngle, float translationAngle)
        {
            rotAngle = rotationAngle;
            transAngle = translationAngle;
            Quaternion rot = Quaternion.Euler(rotAngle, transAngle, 0f);
            SetRotation(rot);
        }

        public void RotateTo(float angle)
        {
            RotateAndTranslateTo(angle, transAngle);
        }

        public void SetRotation(Quaternion rotation)
        {
            celestialBody.rotation = rotation;
        }

        public void Translate(float deltaAngle)
        {
            TranslateTo(transAngle + deltaAngle);
        }

        public void TranslateTo(float angle)
        {
            RotateAndTranslateTo(rotAngle, angle);
        }


		private void Start()
		{
            SetRotation(sunRotationSystem.CurrentSunRotation);
		}
	}
}