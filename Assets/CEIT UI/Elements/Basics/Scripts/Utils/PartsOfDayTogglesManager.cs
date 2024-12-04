using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using CEIT.TimeAndSpace;


namespace CEITUI.Elements.Utils
{
	public class PartsOfDayTogglesManager : MonoBehaviour
	{
		[Header("Toggles:")]
		public Toggle morning;
		public Toggle afternoon;
		public Toggle evening;
		public Toggle night;

		[Header("Events:")]
		public UnityEvent<float> OnUpdateUI;
		public UnityEvent<float> OnUpdateSystems;

		[Header("Config:")]
		public bool skipNextToggleSelection = false;



		public void HandleToggleSelected(int seconds)
		{
			if(!skipNextToggleSelection)
			{
				OnUpdateUI?.Invoke(seconds);
				OnUpdateSystems?.Invoke(seconds);
			}
			skipNextToggleSelection = false;
		}


		public void SelectAToggle(System.TimeSpan timeSpan)
		{
			SelectAToggleWithoutUpdatingSystems(timeSpan);
			OnUpdateSystems?.Invoke((int)timeSpan.TotalSeconds);
		}
		
		public void SelectAToggle(int seconds)
			=> SelectAToggle(System.TimeSpan.FromSeconds(seconds));


		public void SelectAToggleWithoutUpdatingSystems(System.TimeSpan timeSpan)
		{
			SelectAToggleWithoutOtherUpdates(timeSpan);
			OnUpdateUI?.Invoke((float)timeSpan.TotalSeconds);
		}
		
		public void SelectAToggleWithoutUpdatingSystems(int seconds)
			=> SelectAToggleWithoutUpdatingSystems(System.TimeSpan.FromSeconds(seconds));


		public void SelectAToggleWithoutOtherUpdates(System.TimeSpan timeSpan)
		{
			Toggle targetToggle = getTargetToggle(timeSpan);
			//if(!targetToggle.isOn)
			targetToggle.SetIsOnWithoutNotify(true);
		}

		public void SelectAToggleWithoutOtherUpdates(float seconds)
			=> SelectAToggleWithoutOtherUpdates(System.TimeSpan.FromSeconds(seconds));



		private PartsOfTheDay m_partsOfTheDay;
		private Toggle getTargetToggle(System.TimeSpan timeSpan)
		{
			if (m_partsOfTheDay == null)
				m_partsOfTheDay = new PartsOfTheDay();

			int hours = (int)timeSpan.TotalHours;
			Toggle targetToggle;

			switch (m_partsOfTheDay[hours])
			{
				case NamesOFPartsOfTheDay.Mañana:
					targetToggle = morning;
					break;
				case NamesOFPartsOfTheDay.Mediodía:
					targetToggle = afternoon;
					break;
				case NamesOFPartsOfTheDay.Tarde:
					targetToggle = evening;
					break;
				default:
					targetToggle = night;
					break;
			}
			return targetToggle;
		}
	}
}