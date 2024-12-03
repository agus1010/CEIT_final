using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
    public class VerticalSlide : MonoBehaviour
    {
        public float scale = .1f;

        public UnityEvent<float> OnValueChanged;

        public float yPosition { get; private set; } = 0f;


		public void Reset()
		{
            transform.localPosition = new Vector3
                (
                    transform.localPosition.x,
                    0f,
                    transform.localPosition.z
                );
            yPosition = 0f;
            OnValueChanged?.Invoke(0f);
        }

		public void SlideVertically(float ammount)
		{
            transform.localPosition = new Vector3
                (
                    transform.localPosition.x,
                    ammount * scale,
                    transform.localPosition.z
                );
            yPosition += ammount;
            OnValueChanged?.Invoke(ammount);
        }
    }
}