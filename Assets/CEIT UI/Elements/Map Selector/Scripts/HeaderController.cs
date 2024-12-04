using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace CEITUI.Elements.MapSelector
{
	public class HeaderController : MonoBehaviour
	{
		public TextMeshProUGUI title;
		public GameObject goBackButton;
		public Slider stepsSlider;
		public TextMeshProUGUI helpParagraph;

		public bool backtrackingAllowed
		{
			get => _backtrackingAllowed && step > 0;
			set
			{
				_backtrackingAllowed = value;
				updateGoBackButton();
			}
		}

		private bool _backtrackingAllowed = true;
		private int step = 0;


		public void UpdateGraphics(int step, MapSelectorViewHeaderData headerData)
		{
			this.step = step;
			stepsSlider.value = step;
			title.text = headerData.Title;
			helpParagraph.text = headerData.helpText;
			updateGoBackButton();
		}


		private void updateGoBackButton()
			=> goBackButton.SetActive(backtrackingAllowed);
	}
}