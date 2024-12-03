using UnityEngine;


namespace CEIT.Utils
{
	public class ChildrenObjectsSwapper : MonoBehaviour
	{
		public GameObject fixedSrc;
		public GameObject fixedDst;


		public void Swap()
		{
			Swap(fixedSrc, fixedDst);
			var newSrc = fixedDst;
			var newDst = fixedSrc;
			fixedSrc = newSrc;
			fixedDst = newDst;
		}


		public void Swap(GameObject src, GameObject dst)
		{
			Transform child;
			for (int i = 0; i < src.transform.childCount; i++)
			{
				child = src.transform.GetChild(i);
				child.SetParent(dst.transform);
			}
		}
	}
}