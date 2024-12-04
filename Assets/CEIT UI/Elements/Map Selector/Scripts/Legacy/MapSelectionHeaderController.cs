using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace CEITUI.Elements
{
    public class MapSelectionHeaderController : MonoBehaviour
    {
        public TextMeshProUGUI title;
        public GameObject goBackButton;
		public Slider stepsSlider;

        public string[] Titles;


        public void MoveToStep(int step)
		{
            title.text = Titles[step];
			stepsSlider.value = step;
            goBackButton.SetActive(step - 1 >= 0);
		}


		private void Start()
		{
			MoveToStep(0);
		}
	}
}