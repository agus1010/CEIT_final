using UnityEngine;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Surface", menuName = "CEIT/Item/Surface")]
	public class Surface : Item
	{
		public Material[] materials;
	}
}