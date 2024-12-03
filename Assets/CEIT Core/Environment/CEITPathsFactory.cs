using System.IO;
using UnityEngine;


namespace CEIT.Environment
{
	public class CEITPathsFactory
	{
		public string mapName { get; private set; } = null;
		public string mapDirectory { get; private set; } = null;

		public string modelSurfacesFilePath { get; private set; } = null;
		public string simulationTimeFilePath { get; private set; } = null;

		public string propsDirectory { get; private set; } = null;
		public string appPropsFilePath { get; private set; } = null;
		public string appPropsSurfacesFilePath { get; private set; } = null;

		public FileInfo modelSurfacesFileInfo { get; private set; } = null;
		public FileInfo simulationTimeFileInfo { get; private set; } = null;
		public FileInfo appPropsFileInfo { get; private set; } = null;
		public FileInfo appPropsSurfacesFileInfo { get; private set; } = null;


		public CEITPathsFactory(string modelFileName)
			=> init(
					modelFileName,
					SavingAndLoadingRuntimeVariables.DEFAULT_PROPS_FILE_NAME,
					SavingAndLoadingRuntimeVariables.DEFAULT_SURFACES_FILE_NAME,
					SavingAndLoadingRuntimeVariables.DEFAULT_TIME_FILE_NAME
				);

		public CEITPathsFactory(string modelFileName, string defaultPropsFileName, string defaultSurfacesFileName, string defaultTimeFileName)
			=> init(modelFileName, defaultPropsFileName, defaultSurfacesFileName, defaultTimeFileName);

		private void init(string modelFileName, string defaultPropsFileName, string defaultSurfacesFileName, string defaultTimeFileName)
		{
			mapName = modelFileName;
			mapDirectory = Path.Combine(Application.persistentDataPath, mapName);
			modelSurfacesFilePath = Path.Combine(mapDirectory, defaultSurfacesFileName);
			simulationTimeFilePath = Path.Combine(mapDirectory, defaultTimeFileName);
			propsDirectory = Path.Combine(mapDirectory, "props");
			appPropsFilePath = Path.Combine(propsDirectory, defaultPropsFileName);
			appPropsSurfacesFilePath = Path.Combine(propsDirectory, defaultSurfacesFileName);
			modelSurfacesFileInfo = new FileInfo(modelSurfacesFilePath);
			simulationTimeFileInfo = new FileInfo(simulationTimeFilePath);
			appPropsFileInfo = new FileInfo(appPropsFilePath);
			appPropsSurfacesFileInfo = new FileInfo(appPropsSurfacesFilePath);
		}
	}
}