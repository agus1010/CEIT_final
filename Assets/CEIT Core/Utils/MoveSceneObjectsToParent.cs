using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CEIT.Utils
{
	public class MoveSceneObjectsToParent : MonoBehaviour
	{
		public GameObject targetParent;
		public GameObject[] rootRejects;

		public void DoTheMovement()
		{
			Scene s = gameObject.scene;
			foreach (var go in s.GetRootGameObjects())
			{
				if(!rootRejects.Contains(go))
				{
					go.transform.SetParent(targetParent.transform);
				}
			}
		}
	}
}