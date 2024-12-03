using System.Linq;
using System.IO;
using UnityEngine;

using CEIT.Loading;
using CEIT.Extensions;


namespace CEIT.Environment
{
	[CreateAssetMenu(fileName = "New Saving and Loading Runtime Variables", menuName = "CEIT/Persistence/Saving and Loading Runtime Variables")]
	public class SavingAndLoadingRuntimeVariables : ScriptableObject
	{
		public const string DEFAULT_PROPS_FILE_NAME = "props.json";
		public const string DEFAULT_SURFACES_FILE_NAME = "surfaces.json";
		public const string DEFAULT_TIME_FILE_NAME = "time.json";

		public string targetUserFiles { get; private set; } = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "CEIT");
		public string[] supportedFileFormats { get; private set; } = new string[] { ".fbx", ".obj" };
		
		[SerializeField] private float _maxFileSizeInMB = 250f;
		public float maxFileSizeInMB { get => _maxFileSizeInMB; set => _maxFileSizeInMB = value; }
		
		public FileInfo[] availableUserModels
		{
			get
			{
				return Directory.EnumerateFiles(
						targetUserFiles,
						string.Join(";", supportedFileFormats.Select(extension => "*" + extension)),
						SearchOption.AllDirectories)
					.Select(path => new FileInfo(path))
					.Where(fileInfo => fileInfo.LengthInMB() <= maxFileSizeInMB)
					.ToArray();
			}
		}

		[SerializeField] private string _mapName = "DefaultMap";
		public string mapName { get => _mapName; private set => _mapName = value; }
		
		public string externalPropsFileName { get; private set; } = null;

		public FileInfo modelSurfaces => pathsFactory != null? pathsFactory.modelSurfacesFileInfo : null;
		public FileInfo simulationTime => pathsFactory != null ? pathsFactory.simulationTimeFileInfo : null;

		public FileInfo appProps => pathsFactory != null? pathsFactory.appPropsFileInfo : null;
		public FileInfo appPropsSurfaces => pathsFactory != null ? pathsFactory.appPropsSurfacesFileInfo : null;

		public FileInfo externalPropsDiff { get; private set; } = null;
		public FileInfo externalPropsSurfacesDiff { get; private set; } = null;


		private CEITPathsFactory pathsFactory;
		private string externalPropsDirectory = null;


		public void SetValuesFromParameters(ModelLoadingOperationParameters parameters)
		{
			if(parameters.mapFile != null)
				SetMapName(parameters.mapFile.Name);
			if(parameters.propsFile != null)
				SetPickedPropsName(parameters.propsFile.Name);
		}

		public void SetMapName(string mapFileName)
		{
			pathsFactory = new CEITPathsFactory(mapFileName, DEFAULT_PROPS_FILE_NAME, DEFAULT_SURFACES_FILE_NAME, DEFAULT_TIME_FILE_NAME);
			mapName = mapFileName;
			tryCreateDir(pathsFactory.propsDirectory);
		}

		public void SetPickedPropsName(string pickedPropsFileName)
		{
			externalPropsFileName = pickedPropsFileName;
			tryCreateDir(externalPropsDirectory);
			if (pathsFactory != null)
			{
				externalPropsDiff = getFileInfoCombinning(pathsFactory.propsDirectory, DEFAULT_PROPS_FILE_NAME);
				externalPropsSurfacesDiff = getFileInfoCombinning(pathsFactory.propsDirectory, DEFAULT_SURFACES_FILE_NAME);
			}
		}



		private FileInfo getFileInfoCombinning(string path1, string path2)
			=> new FileInfo(Path.Combine(path1, path2));

		private void tryCreateDir(string path)
		{
			try
			{
				Directory.CreateDirectory(path);
			}
			catch (IOException) { };
		}
	}
}