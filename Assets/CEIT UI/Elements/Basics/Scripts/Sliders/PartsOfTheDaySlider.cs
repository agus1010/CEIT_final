using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace CEITUI.Elements.Sliders
{
	[RequireComponent(typeof(Slider))]
	public class PartsOfTheDaySlider : MonoBehaviour
	{
		[Header("Events:")]
		public UnityEvent<float> OnUpdateUI;
		public UnityEvent<float> OnUpdateSystems;

		private Slider slider;


		public void SetValueWithoutNotify(float value)
		{
			Start();
			slider.SetValueWithoutNotify(value);
		}


		public void HandleValueChanged(float value)
		{
			HandleValueChangedWithoutUpdateSystems(value);
			OnUpdateSystems?.Invoke(value);
		}

		public void HandleValueChangedWithoutUpdateSystems(float value)
		{
			OnUpdateUI?.Invoke(value);
		}


		private void Start()
		{
			if (slider == null)
				slider = GetComponent<Slider>();
		}
	}
}