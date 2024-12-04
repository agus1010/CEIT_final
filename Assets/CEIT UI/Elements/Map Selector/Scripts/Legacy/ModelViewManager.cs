using UnityEngine;

using CEIT.Loading;


namespace CEITUI.Elements
{
    public class ModelViewManager : MonoBehaviour
    {
        public ModelLoadingOperationParameters parameters;

        public GameObject ModelParent;
        public GameObject GroundParent;
        public GameObject ArrowParent;


        public void ResetView()
        {
            UpdateViews();
        }

		public void UpdateViews()
		{
			ModelParent.SetActive(parameters.mapFile != null);
			GroundParent.SetActive(parameters.addGround);
			ArrowParent.SetActive(false);
		}
	}
}