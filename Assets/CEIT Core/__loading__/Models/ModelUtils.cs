using UnityEngine;

using CEIT.Loading;
using CEIT.Loading.Models.Utils;
using CEIT.Loading.Models;

namespace CEIT.Utils
{
    public class ModelUtils : MonoBehaviour
    {
        public GameObject modelParent;
        public GameObject floorParent;
        //public VerticalSlide floorLevelVerticalSlide;
        public AddedGroundController addedGroundController;

        public Bounds bounds { get; private set; }


        public void CalculateModelsBounds()
		{
            MeshFilter[] meshFilters = modelParent.GetComponentsInChildren<MeshFilter>();
            CombineInstance[] combine = new CombineInstance[meshFilters.Length];

            MeshFilter currMF;
			for (int i = 0; i < meshFilters.Length; i++)
			{
                currMF = meshFilters[i];
                combine[i].mesh = currMF.sharedMesh;
                combine[i].transform = currMF.transform.localToWorldMatrix;
            }

            Mesh mesh = new Mesh();
            mesh.CombineMeshes(combine);
            mesh.RecalculateBounds();
            bounds = mesh.bounds;
        }

        public void Clear()
        {
            int childCount = modelParent.transform.childCount;
            for (int i = 0; i < childCount; i++)
			{
                Destroy(modelParent.transform.GetChild(childCount - 1 -i).gameObject);
			}
        }

        public void SetModelElementsTag(string tag)
        {
            foreach (var mf in modelParent.GetComponentsInChildren<MeshFilter>())
            {
                mf.gameObject.tag = tag;
            }
		}

        public void SnapFloorLevelToParams(ModelLoadingOperationParameters parameters)
        {
            floorParent.SetActive(parameters.addGround);
            if (parameters.addGround)
            {
				//floorLevelVerticalSlide.scale = 1;
				//floorLevelVerticalSlide.SlideVertically(parameters.groundHeight);0
				m_ceitModel = new CEITModel(modelParent);
				m_ceitModel.UpdateData();
                //addedGroundController.ceitModel = m_ceitModel;
                float heightExtent = addedGroundController.heightExtent;
                addedGroundController.height = parameters.groundHeight * m_ceitModel.bounds.extents.y;//addedGroundController.heightExtent;
			}
        }
        
        public void SnapFloorLevelToBounds()
		{
            //floorLevelVerticalSlide.Reset();
            //floorLevelVerticalSlide.SlideVertically(bounds.min.y);
        }

        public void SetVisibility(bool visibility)
		{
            int childCount = modelParent.transform.childCount;
            for (int i = 0; i < childCount; i++)
            {
                modelParent.transform.GetChild(i).gameObject.SetActive(visibility);
            }
        }


		//private void Update()
		//{
        //    if(modelParent.GetComponentsInChildren<MeshFilter>().Length > 0 && m_ceitModel == null)
        //        m_ceitModel = new CEITModel(modelParent);
		//}

		private CEITModel m_ceitModel;
		private void OnDrawGizmosSelected()
		{
            print("TRYNNG TO DRAW..");
			if (m_ceitModel != null)
            {
                print("DRAWING!");
                Vector3 center = m_ceitModel.bounds.center;
			    Gizmos.color = new Color(255, 0, 0, 0.25f);
			    Gizmos.DrawCube(center, m_ceitModel.bounds.size);
			    Gizmos.DrawWireCube(center, m_ceitModel.bounds.size);
            }
		}
	}
}