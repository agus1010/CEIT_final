using UnityEngine;
using UnityEngine.Events;


namespace CEIT.TimeAndSpace
{
	[CreateAssetMenu(fileName = "New Geo Translation System Events Channel", menuName = "CEIT/Events/Channels/Core/Geo Translation System")]
	public class GeoTranslationSystemEventsChannel : ScriptableObject
	{
		public UnityEvent<float> GeoTranslationChanged;

		public void FireGeoTranslationChanged(float translation)
			=> GeoTranslationChanged?.Invoke(translation);
	}
}