using UnityEngine;

using CEIT.Persistence;


namespace CEITUI.Elements
{
    public class SelectToggleFromCollection : MonoBehaviour
	{
		public ItemPalette palette;
		public TMPro.TextMeshProUGUI hoveredInteractionTitle;
		public UnityEngine.UI.Toggle[] toggles;

		public void PairWithPalette()
		{
			hoveredInteractionTitle.text = palette.Current.ItemName;
			var toggle = toggles[palette.Index];
			toggle.Select();
			toggle.SetIsOnWithoutNotify(true);
		}


		public void PairToIndex(int index)
		{
			for (int i = 0; i < toggles.Length; i++)
			{
				toggles[i].SetIsOnWithoutNotify(i <= index);
			}
		}

		public void PairToIndex(float index)
			=> PairToIndex((int)index);
	}
}