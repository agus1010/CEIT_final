using System.IO;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;
using CEIT.Environment;
using System;

namespace CEITUI.Elements
{
	public class FileToggleController : MonoBehaviour
	{
		[Header("References:")]
		public TextMeshProUGUI fileSizeTitle;
		public TextMeshProUGUI fileChangesTitle;
		public TextMeshProUGUI fileNameTitle;

		[Header("Config:")]
		public bool showLastModification = false;

		[Header("Events:")]
		public UnityEvent<FileInfo> OnSubmit;

		public Toggle toggle => GetComponent<Toggle>();

		private FileInfo fileInfo;


		public void SetFileInfo(FileInfo fileInfo)
		{
			this.fileInfo = fileInfo;
			SetFileName(fileInfo.Name);
			SetFileChanges(fileInfo.Name);
			SetFileSize(fileInfo.Length);
		}

		public void SetToggleGroup(ToggleGroup toggleGroup)
		{
			GetComponent<Toggle>().group = toggleGroup;
		}


		public void SetFileName(string fileName)
		{
			fileNameTitle.text = fileName;
		}

		public void SetFileChanges(string fileName)
		{
			if(!showLastModification)
			{
				fileChangesTitle.text = "--";
				return;
			}
			CEITPathsFactory pathsFactory = new CEITPathsFactory(fileName);
			string msg = "Sin Modificaciones.";
			if (Directory.Exists(pathsFactory.mapDirectory))
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(pathsFactory.mapDirectory);
				TimeSpan timeSpan = directoryInfo.LastWriteTime.TimeOfDay;
				int totalDays = (int)timeSpan.TotalDays;
				if (totalDays == 0)
				{
					msg = "Modificado recientemente.";
				}
				else
				{
					fileChangesTitle.fontStyle = FontStyles.Bold;
					if (totalDays <= 30)
					{
						msg = $"Modificado hace {totalDays} día";
						if (totalDays > 1)
							msg += "s";
						msg += ".";
					}
					else
					{
						msg = $"Modificado hace más de 30 días.";
					}
				}

			}
			fileChangesTitle.text = msg;
		}

		public void SetFileSize(long bytes)
		{
			string unit = "KB";
			float size = bytes / 1024f;
			if(size > 1024)
			{
				unit = "MB";
				size /= 1024f;
				if(size > 1024)
				{
					unit = "GB";
					size /= 1024f;
				}
			}
			string sizeStr = size.ToString(".##");
			fileSizeTitle.text = $"{sizeStr} {unit}";
		}

		public void Submit()
		{
			OnSubmit?.Invoke(fileInfo);
		}
	}
}