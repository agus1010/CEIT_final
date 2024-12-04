using UnityEngine;


namespace CEITUI.Elements.Data
{
	[CreateAssetMenu(fileName = "New Map File View Data", menuName = "CEIT/UI/Views/Map File View Data")]
	public class MapFileViewData : ScriptableObject
	{
		public string Title;
		public GameObject ViewPrefab;
	}
}