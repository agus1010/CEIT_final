using UnityEngine;


namespace CEITUI.Elements.MapSelector
{
	[CreateAssetMenu(fileName = "Empty Map Selector View Header Data", menuName = "CEIT/UI/Map Selector View Header Data")]
	public class MapSelectorViewHeaderData : ScriptableObject
	{
		public string Title = "SE VAN A LA B!";
		[TextArea(1, 50)] public string helpText = "ESTO ES UNA AYUDA. TE AYUDA? :D";
	}
}