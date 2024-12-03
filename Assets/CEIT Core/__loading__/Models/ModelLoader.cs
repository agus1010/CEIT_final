using System.IO;
using TriLibCore;
using UnityEngine;

using CEIT.Interactables;
using CEIT.Utils;
using CEIT.Loading.Models;
using CEIT.Loading.Models.Utils;


namespace CEIT.Loading
{
	public class ModelLoader : CEITLoaderBehaviour
	{
		public ModelUtils modelUtils;
		[Header("Loading Target:")]
		public GameObject loadingTarget;

		[Header("Parameters:")]
		public AssetLoaderOptions assetLoaderOptions;
		public ModelLoadingOperationParameters parameters;

		[Header("Channel:")]
		public ModelLoadingOperationEventsChannel channel;
		
		[Header("Optimizations:")]
		public bool RemoveNotRenderedMeshes = true;
		public bool AddSurfaceHistories = true;
		public bool SetCollidersAsTrigger = false;

		public float Progress { get; private set; } = -1f;
		public bool isBussy => context != null && !context.CancellationToken.IsCancellationRequested;

		private bool isLoaded => isBussy && Progress < 1f;
		private AssetLoaderContext context;
		private FileInfo fileInfo = null;
		private bool shouldRunUpdate = false;


		public void Cancel()
		{
			if (context != null && context.CancellationToken.CanBeCanceled && !context.CancellationToken.IsCancellationRequested)
			{
				if(debug)
					print($"Firering CANCELLATION of loading {fileInfo.FullName} in component: {transform.parent.name}.{name}->{this.GetType()}.");
				context.CancellationTokenSource.Cancel();
				fileInfo = null;
				modelUtils.Clear();
				channel?.FireCancelled();
			}
		}

		public override void PerformLoadingOperation()
			=> PerformLoadingOperation(parameters);

		public void PerformLoadingOperation(ModelLoadingOperationParameters parameters)
		{
			fireStarted();
			try
			{
				performLoad();
			}
			catch (System.Exception e)
			{
				fireCancelled();
				fireError(e);
			}
		}



		protected override void performLoad()
		{
			if (!isLoaded && fileInfo != null && parameters.mapFile.FullName == fileInfo.FullName)
			{
				fireFinished();
				return;
			}
			modelUtils.Clear();
			Progress = 0f;
			fileInfo = parameters.mapFile;
			if (debug)
				print($"Started loading {fileInfo.FullName}.");
			shouldRunUpdate = true;
			channel?.FireModelSelected(fileInfo);
			context = makeContext(fileInfo, assetLoaderOptions, loadingTarget);
		}

		protected void performLoad(ModelLoadingOperationParameters parameters)
		{

		}



		private void Reset()
		{
			modelUtils = GetComponent<ModelUtils>();
		}
		
		private void Awake()
		{
			Reset();
		}

		private float m_prevProgress = 0f;
		private void Update()
		{
			if (!shouldRunUpdate)
				return;
			if (!isBussy)
			{
				Progress = context.LoadingProgress;
				if (Progress - m_prevProgress > 0.5f)
				{
					m_prevProgress = Progress;
					fireProgress();
				}
			}
		}

		private void OnApplicationQuit()
		{
			Cancel();
		}


		private AssetLoaderContext makeContext(FileInfo fileInfo, AssetLoaderOptions options, GameObject parent)
		{ 
			AssetLoaderContext context = AssetLoader.LoadModelFromFile
			(
				path: fileInfo.FullName,
				onLoad: onMeshesLoaded,
				onMaterialsLoad: onMaterialsLoaded,
				onProgress: onProgress,
				onError: onError,
				wrapperGameObject: parent,
				assetLoaderOptions: options,
				customContextData: null,
				haltTask: false,
				onPreLoad: null,
				isZipFile: false
			);
			return context;
		}

		private void onMeshesLoaded(AssetLoaderContext context)
		{
			modelUtils.CalculateModelsBounds();
			if (debug)
				print($"Meshes of {fileInfo.FullName} loaded.");
			channel?.FireMeshesLoaded();
		}

		private void onMaterialsLoaded(AssetLoaderContext context)
		{
			if (RemoveNotRenderedMeshes)
				removeNotRenderedMeshes(context.WrapperGameObject);
			if (AddSurfaceHistories)
				addSurfaceHistories(context.WrapperGameObject);
			if (SetCollidersAsTrigger)
				setCollidersAsTrigger(context.WrapperGameObject);
			if (debug)
				print($"Materials of {fileInfo.FullName} loaded.");
			channel?.FireMaterialsLoaded();
		}

		private void onProgress(AssetLoaderContext context, float progress)
		{
			m_prevProgress = Progress;
			Progress = progress;
			if(Progress - m_prevProgress > 0.1f && Progress != 1f)
			{
				fireProgress();
			}
			if(Progress == 1f)
			{
				shouldRunUpdate = false;
				fireFinished();
			}
		}

		private void onError(IContextualizedError contextualizedError)
		{
			modelUtils.Clear();
			fireError(contextualizedError.GetInnerException());
		}


		private void addSurfaceHistories(GameObject parent)
		{
			foreach (var mr in parent.GetComponentsInChildren<MeshRenderer>())
			{
				mr.gameObject.AddComponent<SurfaceHistory>();
			}
		}

		private void removeNotRenderedMeshes(GameObject parent)
		{
			foreach (var mr in parent.GetComponentsInChildren<MeshRenderer>())
			{
				if (mr.materials.Length == 0 || mr.sharedMaterials.Length == 0)
					Destroy(mr.gameObject);
			}
		}

		private void setCollidersAsTrigger(GameObject parent)
		{
			foreach (var c in parent.GetComponentsInChildren<Collider>())
			{
				c.isTrigger = true;
			}
		}

		private void fireStarted()
		{
			if (debug)
				print($"STARTED LOADING operation in component: {transform.parent.name}.{name}->{this.GetType()}");
			channel?.FireStarted();
			eventsChannel?.FireStarted();
		}

		private void fireProgress()
		{
			if (debug)
				print($"Model Loading operation progress updated to: {Progress}");
			channel?.FireProgressMade(Progress);
			eventsChannel?.FireProgressMade(Progress);
		}

		private void fireFinished()
		{
			if (debug)
				print($"FINISHED LOADING model operation in component: {transform.parent.name}.{name}->{this.GetType()}");
			channel?.FireFinished();
			eventsChannel?.FireFinished();
		}

		private void fireCancelled()
		{
			if (debug)
				Debug.LogWarning($"LOADING operation CANCELLED in component {transform.parent.name}.{name}->{this.GetType()}");
			channel?.FireCancelled();
			eventsChannel?.FireCancelled();
		}

		private void fireError(System.Exception e)
		{
			if (debug)
				Debug.LogError($"An error ocurred while loading model {fileInfo.Name}: {e}");
			channel?.FireError(e);
			eventsChannel?.FireError(e);
		}
	}
}