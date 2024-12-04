using UnityEngine;


namespace CEITUI.Elements.Titles
{
	[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
	public class InsertValueInTitle : MonoBehaviour
	{
		[SerializeField] private string titleTextFormat;

		private TMPro.TextMeshProUGUI title;


		public virtual void Insert(int value)
			=> formatTitleTextWith(value);

		public virtual void Insert(float value)
			=> formatTitleTextWith(value.ToString("0.##"));

		public virtual void Insert(string value)
			=> formatTitleTextWith(value);


		protected virtual void formatTitleTextWith(object value)
		{
			if (title != null)
				title.text = string.Format(titleTextFormat, value.ToString());
		}

		protected virtual void Reset()
			=> title = GetComponent<TMPro.TextMeshProUGUI>();

		protected virtual void Start()
			=> Reset();
	}
}