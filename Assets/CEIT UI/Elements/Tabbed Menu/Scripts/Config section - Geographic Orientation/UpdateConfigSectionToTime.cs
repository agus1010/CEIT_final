using UnityEngine;

using CEIT.TimeAndSpace;
using CEITUI.Elements.Utils;


namespace CEITUI.Elements.Sliders
{
    public class UpdateConfigSectionToTime : MonoBehaviour
    {
        [Header("Systems:")]
        public TimeSystem timeSystem;

        [Header("UI Elements")]
        public PartsOfDayTitle title;
        public PartsOfTheDaySlider slider;
        public PartsOfDayTogglesManager autoSelector;


        public void UpdateElementsWithoutNotify()
        {
            title.UpdateToTime(timeSystem.totalSeconds);
            slider.SetValueWithoutNotify(timeSystem.totalSeconds);
            autoSelector.SelectAToggleWithoutOtherUpdates(timeSystem.totalSeconds);
        }
	}
}