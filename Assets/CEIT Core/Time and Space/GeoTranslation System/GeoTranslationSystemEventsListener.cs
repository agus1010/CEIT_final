using UnityEngine;
using UnityEngine.Events;

using CEIT.Events;


namespace CEIT.TimeAndSpace
{
	public class GeoTranslationSystemEventsListener : EventsListener
	{
		public GeoTranslationSystemEventsChannel eventsChannel;

		public UnityEvent<float> OnGeoTranslationChanged;

		protected override object channel => eventsChannel;


		public override void Subscribe()
		{
			eventsChannel.GeoTranslationChanged.AddListener(onGeoTranslationChanged);
		}

		public override void Unsubscribe()
		{
			eventsChannel.GeoTranslationChanged.RemoveListener(onGeoTranslationChanged);
		}


		private void onGeoTranslationChanged(float angle)
			=> OnGeoTranslationChanged?.Invoke(angle);
	}
}