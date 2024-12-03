using System.IO;
using UnityEngine;

using CEIT.Loading;


namespace CEIT.SavingAndLoading
{
	[CreateAssetMenu(fileName = "New Saving and Loading System", menuName = "CEIT/Persistence/Saving and Loading System")]
	public class SavingAndLoadingSystem : ScriptableObject
	{
		public SavingAndLoadingSystemEventsChannel eventsChannel;

		public string currentMapName { get; private set; } = "TEST";
		public string persistentDataDirectory
		{
			get
			{
				string targetDir = Path.Combine(Application.persistentDataPath, "user_changes");
				try
				{
					Directory.CreateDirectory(targetDir);
				} catch(IOException) { }
				return targetDir;
			}
		}


		public void LoadMap(string mapName)
		{
			currentMapName = mapName;
			try
			{
				Directory.CreateDirectory(Path.Combine(persistentDataDirectory, mapName));
			} catch (IOException) { }
			eventsChannel.FireLoadOperationRequested(mapName);
		}

		public void LoadModel(ModelLoadingOperationParameters modelLoadingOpParams)
		{
			LoadMap(modelLoadingOpParams.mapFile.FullName);
		}

		public void SaveCurrentMap()
		{
			eventsChannel.FireSaveOperationRequested();
		}
	}
}