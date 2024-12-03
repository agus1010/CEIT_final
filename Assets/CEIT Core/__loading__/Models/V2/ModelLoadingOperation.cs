using TriLibCore;
using UnityEngine;

using CEIT.Extensions;


namespace CEIT.Loading.Models
{
	public class ModelLoadingOperation : ILoadingOperation
	{
		public float progress { get; private set; } = 0f;
		public System.IO.FileInfo modelFile { get; private set; } = null;
		public ModelLoadingOperationStatus status { get; private set; } = ModelLoadingOperationStatus.IDLE;
		public ModelLoadingOperationEventsChannel eventsChannel { get; set; }
		public System.Exception error { get; private set; } = null;

		private GameObject parent;
		private AssetLoaderOptions options;
		private AssetLoaderContext context;
		private float maxFileSizeInMB;

		private bool hasStarted => status != ModelLoadingOperationStatus.IDLE;


		public ModelLoadingOperation(GameObject parent, System.IO.FileInfo modelFile, AssetLoaderOptions options, ModelLoadingOperationEventsChannel eventsChannel = null, float maxFileSizeInMB = 250f)
		{
			this.parent = parent;
			this.modelFile = modelFile;
			this.options = options;
			this.eventsChannel = eventsChannel;
			this.maxFileSizeInMB = maxFileSizeInMB;
		}


		public void Abort()
		{
			if(hasStarted && status < ModelLoadingOperationStatus.COMPLETED)
			{
				var token = context.CancellationToken;
				if (token.CanBeCanceled && !token.IsCancellationRequested)
				{
					context.CancellationTokenSource.Cancel();
					status = ModelLoadingOperationStatus.ABORTED_MANUALLY;
					eventsChannel?.FireCancelled();
				}
			}
		}

		public void Begin()
		{
			if (hasStarted || modelFile == null) return;

			if (modelFile.LengthInMB() > maxFileSizeInMB)
			{
				fireError(new System.IO.FileLoadException($"El modelo que intenta cargar ({modelFile.Name}) supera el límite de tamaño permitido ({maxFileSizeInMB}MB)."));
				return;
			}

			options.ReadEnabled = true;
			context = AssetLoader.LoadModelFromFile
			(
				path: modelFile.FullName,
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
			status = ModelLoadingOperationStatus.STARTED;
			eventsChannel?.FireStarted();
		}


		private void onMeshesLoaded(AssetLoaderContext context)
			=> eventsChannel?.FireMeshesLoaded();

		private void onMaterialsLoaded(AssetLoaderContext context)
			=> eventsChannel?.FireMaterialsLoaded();

		private void onProgress(AssetLoaderContext context, float progress)
		{
			if (this.progress == progress) return;

			this.progress = progress;
			eventsChannel?.FireProgressMade(this.progress);
			
			if (this.progress == 1f)
			{
				status = ModelLoadingOperationStatus.COMPLETED;
				eventsChannel?.FireFinished();
			}
		}

		private void onError(IContextualizedError contextualizedError)
			=> fireError(contextualizedError.GetInnerException());

		private void fireError(System.Exception exception)
		{
			error = exception;
			status = ModelLoadingOperationStatus.ABORTED_BY_ERROR;
			eventsChannel?.FireError(error);
		}
	}
} 