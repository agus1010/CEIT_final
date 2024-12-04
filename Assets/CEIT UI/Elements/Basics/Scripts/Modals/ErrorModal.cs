using TMPro;

using CEITUI.Elements.MapSelector;


namespace CEITUI.Elements.Modals
{
	public class ErrorModal : CEITUIView
	{
		public TextMeshProUGUI title;
		public TextMeshProUGUI description;

		private bool _isVisible = true;
		public bool isVisible
		{
			get => _isVisible;
			set
			{
				_isVisible = value;
				body.SetActive(_isVisible);
			}
		}


		public void InsertException(System.Exception exception)
		{
			if (title != null)
				title.text = exception.Source;
			if (description != null)
				description.text = exception.Message;
		}
	}
}