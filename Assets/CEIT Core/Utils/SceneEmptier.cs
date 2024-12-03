using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CEIT.Utils
{
	public class SceneEmptier : MonoBehaviour
	{
		public void EmptyScene(string name)
		{
			var scene = SceneManager.GetSceneByName(name);
			foreach (var go in scene.GetRootGameObjects().Skip(1))
			{
				Destroy(go);
			}
		}
	}
}