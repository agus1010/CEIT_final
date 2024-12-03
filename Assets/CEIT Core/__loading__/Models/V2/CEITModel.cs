using UnityEngine;

using CEIT.Extensions;


namespace CEIT.Loading.Models
{
	public class CEITModel
	{
		public GameObject parent { get; private set; }
		public Bounds bounds { get; private set; }
		public MeshFilter[] meshFilters { get; private set; }


		public CEITModel(GameObject parent)
		{
			this.parent = parent;
			meshFilters = new MeshFilter[0];
			UpdateData();
		}

		public void Clear()
		{
			foreach (var t in parent.transform.GetDirectChilds())
				GameObject.Destroy(t.gameObject);
			UpdateData();
		}

		public void UpdateBounds()
			=> bounds = meshFilters.Length > 0 ?
					combineMeshes(meshFilters).bounds :
					new Bounds();

		public void UpdateData()
		{
			meshFilters = parent.GetComponentsInChildren<MeshFilter>();
			UpdateBounds();
		}



		private Mesh combineMeshes(MeshFilter[] meshFilters)
		{
			CombineInstance[] combine = new CombineInstance[meshFilters.Length];

			for (int i = 0; i < meshFilters.Length; i++)
			{
				combine[i].mesh = meshFilters[i].sharedMesh;
				combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
			}

			Mesh mesh = new Mesh();
			mesh.CombineMeshes(combine);
			mesh.RecalculateBounds();
			return mesh;
		}
	}
}