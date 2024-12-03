using UnityEngine;

using CEIT.Persistence;


namespace CEIT.Loading
{
	public class ModelLoadingParametersSetter : MonoBehaviour
	{
		public SelectSpawnPointBehaviour spawnPointBehaviour;
        public Transform movablePoint;
        public ModelLoadingOperationParameters parameters;


		public Vector3 spawnPosition { get; private set; } = Vector3.zero;

		private Quaternion originalRot;
		private Vector3 originalScale;


		public void SetGroundHeight()
        {
			parameters.spawnPosition = spawnPosition;
		}


		private void Start()
		{
			originalRot = transform.rotation;
			originalScale = transform.localScale;
		}

		private void Update()
        {
            spawnPosition = calcMovableFinalWorldPos();
            reset();
		}


		private Vector3 calcMovableFinalWorldPos()
		{
			movablePoint.transform.position = spawnPointBehaviour.shotPosition;
			transform.rotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			return movablePoint.transform.localPosition;
		}

		private void reset()
        {
			transform.rotation = originalRot;
			transform.localScale = originalScale;
		}
    }

}