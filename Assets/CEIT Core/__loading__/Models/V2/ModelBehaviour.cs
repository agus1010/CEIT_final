using System.IO;
using TriLibCore;
using UnityEngine;


namespace CEIT.Loading.Models
{
	public class ModelBehaviour : MonoBehaviour
	{
		[Header("References:")]
		[SerializeField] private GameObject parent;
		[SerializeField] private AssetLoaderOptions loadingOptions;
		[SerializeField] private ModelLoadingOperationEventsChannel _eventsChannel;
		[SerializeField] private bool updateBounds = false;

		private bool _isVisible = true;
		public bool isVisible
		{
			get => _isVisible;
			set
			{
				_isVisible = value;
				parent.SetActive(_isVisible);
			}
		}

		private FileInfo _modelFile;
		public FileInfo modelFile
		{
			get => _modelFile;
			set
			{
				_modelFile = value;

				if (operation == null || operation.modelFile != _modelFile)
				{
					model?.Clear();
					if (operation != null)
					{
						operation.eventsChannel = null;
						operation.Abort();
					}
					operation = new ModelLoadingOperation(parent, _modelFile, loadingOptions, eventsChannel);
					followOperation = true;
					operation.Begin();
				}
			}
		}

		public CEITModel model { get; private set; }
		public ModelLoadingOperation operation { get; private set; } = null;
		public float loadingProgress => operation != null ? operation.progress : 0f;
		public ModelLoadingOperationEventsChannel eventsChannel => _eventsChannel;

		private bool followOperation = false;

		public void AddSurfaceHistories()
		{
			foreach (var mr in parent.GetComponentsInChildren<MeshRenderer>())
				mr.gameObject.AddComponent<Interactables.SurfaceHistory>();
		}

		public void ResetRotations()
			=> parent.transform.rotation = Quaternion.identity;

		public void RotateInX(float xAngle)
			=> setRotation(Vector3.right, xAngle);

		public void RotateInY(float yAngle)
			=> setRotation(Vector3.up, yAngle);

		public void RotateInZ(float zAngle)
			=> setRotation(Vector3.forward, zAngle);

		public void SetModelFileFromParameters(ModelLoadingOperationParameters parameters)
			=> modelFile = parameters.mapFile;


		private void setRotation(Vector3 axis, float angle)
			=> parent.transform.rotation = makeRotation(axis, angle);

		private Quaternion makeRotation(Vector3 axis, float angle)
		{
			Vector3 currentEulers = parent.transform.rotation.eulerAngles;
			Vector3 targetEulers = new Vector3
				(
					axis.x != 0 ? angle : currentEulers.x,
					axis.y != 0 ? angle : currentEulers.y,
					axis.z != 0 ? angle : currentEulers.z
				);
			return Quaternion.Euler(targetEulers);
		}


		// Unity Messages

		private void Awake()
		{
			model = new CEITModel(parent);
			followOperation = false;
		}

		private void Update()
		{
			if(followOperation && (int)operation.status >= 10)
			{
				followOperation = false;
				if ((int)operation.status == 10)
					model.UpdateData();
			}
		}

		private void OnApplicationQuit()
		{
			operation?.Abort();
		}


		private void OnDrawGizmos()
		{
			if(model != null)
			{
				Vector3 center = model.parent.transform.position;
				Gizmos.color = new Color(0, 0, 255, 0.25f);
				Gizmos.DrawCube(center, model.bounds.size);
				Gizmos.DrawWireCube(center, model.bounds.size);
			}
		}
	}
}