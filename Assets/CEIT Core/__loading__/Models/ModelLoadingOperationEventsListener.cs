using CEIT.Environment;
using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Loading
{
	public class ModelLoadingOperationEventsListener : CEITOperationEventsListener
	{
		public ModelLoadingOperationEventsChannel mapLoadEventsChannel;
		protected override object channel => mapLoadEventsChannel;

		public UnityEvent<System.IO.FileInfo> OnModelSelected;
		public UnityEvent OnMeshesLoaded;
		public UnityEvent OnMaterialsLoaded;


		public override void Subscribe()
		{
			base.Subscribe();
			mapLoadEventsChannel.ModelSelected.AddListener(onModelSelected);
			mapLoadEventsChannel.MeshesLoaded.AddListener(onMeshesLoaded);
			mapLoadEventsChannel.MaterialsLoaded.AddListener(onMaterialsLoaded);
		}

		public override void Unsubscribe()
		{
			base.Unsubscribe();
			mapLoadEventsChannel.ModelSelected.AddListener(onModelSelected);
			mapLoadEventsChannel.MeshesLoaded.AddListener(onMeshesLoaded);
			mapLoadEventsChannel.MaterialsLoaded.AddListener(onMaterialsLoaded);
		}


		private void onModelSelected(System.IO.FileInfo fileInfo)
			=> OnModelSelected?.Invoke(fileInfo);
		private void onMeshesLoaded()
			=> OnMeshesLoaded?.Invoke();
		private void onMaterialsLoaded()
			=> OnMaterialsLoaded?.Invoke();
	}
}