using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace CEITUI.Elements
{
	public class FileListPopulator : MonoBehaviour
	{
		[Header("Data:")]
		public CEIT.Environment.SavingAndLoadingRuntimeVariables simulationSessionVariables;
		[Header("UI Elements:")]
		public GameObject potentialFilePrefab;
		public Transform potentialFilesParent;
		public ToggleGroup fileTogglesGroup;

		public UnityEvent<FileInfo> OnFileSelected;

		public string MapsFolder => simulationSessionVariables.targetUserFiles;
		public string[] SupportedExtensions => simulationSessionVariables.supportedFileFormats;


		public IEnumerable<FileInfo> AvailableMaps
		{
			get
			{
				Directory.CreateDirectory(MapsFolder);
				return Directory.GetFiles
					 (
						path: MapsFolder,
						searchPattern: "*.*",
						searchOption: SearchOption.AllDirectories
					 )
					 .Select(file_path => new FileInfo(file_path))
					.Where(file_info => SupportedExtensions.Contains(file_info.Extension))
					.OrderBy(file_info => file_info.Name);
			}
		}


		private void Start()
		{
			foreach (var fileInfo in AvailableMaps)
			{
				var newGo = Instantiate(potentialFilePrefab, potentialFilesParent);
				var fileButtonController = newGo.GetComponent<FileToggleController>();
				fileButtonController.SetFileInfo(fileInfo);
				fileButtonController.SetToggleGroup(fileTogglesGroup);
				fileButtonController.toggle.onValueChanged.AddListener(onFileToggleValueChanged);
				fileButtonController.OnSubmit.AddListener(onFileToggleSubmitted);
			}
		}

		private void onFileToggleValueChanged(bool value)
		{
			fileTogglesGroup.allowSwitchOff = false;
		}

		private void onFileToggleSubmitted(FileInfo fileInfo)
		{
			OnFileSelected?.Invoke(fileInfo);
		}
	}
}