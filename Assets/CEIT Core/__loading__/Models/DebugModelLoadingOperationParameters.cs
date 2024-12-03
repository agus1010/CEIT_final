using System.IO;
using UnityEngine;

using CEIT.Environment;


namespace CEIT.Loading
{
	[CreateAssetMenu(fileName = "New DEBUG Map Loading Operation Paramters", menuName = "CEIT/Core/Debug/Parameters/Map Loading Operation Parameters")]
	public class DebugModelLoadingOperationParameters : ModelLoadingOperationParameters
	{
		public SavingAndLoadingRuntimeVariables runtimeVars;

		[SerializeField] private string _mapName = "DefaultMap.fbx";

		public string mapName
		{
			get => _mapName;
			set => _mapName = value;
		}

		
		public override FileInfo mapFile
		{
			get
			{
				if (string.IsNullOrEmpty(mapName))
					return base.mapFile;
				return new FileInfo(Path.Join(runtimeVars.targetUserFiles, mapName));
			}
			set => base.mapFile = value;
		}
	}
}