using System;
using UnityEngine;
using TMPro;


namespace CEITUI.Elements.Sliders
{
	[RequireComponent(typeof(TextMeshProUGUI))]
	public class PartsOfDayTitle : MonoBehaviour
	{
		public CEIT.TimeAndSpace.TimeSystem timeSystem;
		public string titleTemplate = "Hora del día: {0}:{1}  ({2})";

		private TextMeshProUGUI title;
		private CEIT.TimeAndSpace.PartsOfTheDay partsOfTheDay;


		public void UpdateToTime(float seconds)
			=> UpdateToTime(TimeSpan.FromSeconds(seconds));

		public void UpdateToTime(TimeSpan timeSpan)
		{
			Reset();
			title.text = string.Format(titleTemplate, timeSpan.Hours.ToString("00"), timeSpan.Minutes.ToString("00"), partsOfTheDay[timeSpan.Hours].ToString());
		}


		private void Reset()
		{
			if(title == null)
				title = GetComponent<TextMeshProUGUI>();
			if(partsOfTheDay == null)
				partsOfTheDay = new CEIT.TimeAndSpace.PartsOfTheDay();
		}

		private void OnEnable()
		{
			Reset();
			UpdateToTime(timeSystem.now);
		}
	}
}