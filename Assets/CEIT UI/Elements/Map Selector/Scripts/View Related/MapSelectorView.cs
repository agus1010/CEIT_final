using UnityEngine;


namespace CEITUI.Elements.MapSelector
{
	public class MapSelectorView : CEITUIView
	{
		[Header("Mandatory:")]
		public MapSelectorViewHeaderData headerData;
		[Header("Base Events:")]
		public UnityEngine.Events.UnityEvent<bool> OnActiveValueChaned;
		public UnityEngine.Events.UnityEvent OnBecomesActive;
		public UnityEngine.Events.UnityEvent OnBecomesInactive;


		private bool _isCurrentlyActive = true;
		public virtual bool isCurrentlyActive
		{
			get => _isCurrentlyActive;
			set
			{
				if (_isCurrentlyActive == value) return;
				_isCurrentlyActive = value;

				UpdateGraphics();

				fireEvents();
			}
		}

		public virtual void UpdateGraphics()
			=> body.SetActive(isCurrentlyActive);

		private void fireEvents()
		{
			OnActiveValueChaned?.Invoke(isCurrentlyActive);
			(isCurrentlyActive ? OnBecomesActive : OnBecomesInactive)?.Invoke();
		}
	}
}