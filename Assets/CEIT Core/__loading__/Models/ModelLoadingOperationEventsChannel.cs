using CEIT.Environment;
using System.IO;
using UnityEngine.Events;


namespace CEIT.Loading
{
	[UnityEngine.CreateAssetMenu(fileName = "New Model Loading Operation Events Channel", menuName = "CEIT/Events/Channels/Core/Model Loading Operation")]
	public class ModelLoadingOperationEventsChannel : CEITOperationEventsChannel
	{
		public UnityEvent<FileInfo> ModelSelected;
		public UnityEvent MeshesLoaded;
		public UnityEvent MaterialsLoaded;

		public void FireModelSelected(FileInfo fileInfo)
		{
			ModelSelected?.Invoke(fileInfo);
			FireStarted();
		}
		public void FireMeshesLoaded()
			=> MeshesLoaded?.Invoke();
		public void FireMaterialsLoaded()
			=> MaterialsLoaded?.Invoke();
	}
}