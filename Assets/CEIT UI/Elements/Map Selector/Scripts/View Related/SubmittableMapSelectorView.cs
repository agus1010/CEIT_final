using UnityEngine.Events;


namespace CEITUI.Elements.MapSelector
{
	public class SubmittableMapSelectorView : MapSelectorView
	{
		private bool _wasSubmitted = false;
		public bool wasSubmitted
		{ 
			get
			{
				bool ogValue = _wasSubmitted;
				_wasSubmitted = false;
				return ogValue;
			}
			private set => _wasSubmitted = value;
		}

		public UnityEvent OnSubmitted;


		public virtual void FireOnSubmitted()
		{
			wasSubmitted = true;
			OnSubmitted?.Invoke();
		}
	}
}