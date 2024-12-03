using System.IO;


namespace CEIT.Utils
{
	public class CEITIOHandler
	{
		public static string Read(string filePath, bool logOutput = false)
		{
			if (!File.Exists(filePath))
			{
				if (logOutput)
					UnityEngine.Debug.Log($"Target file: \"{filePath}\" does not exist.");
				throw new FileNotFoundException($"Target file: \"{filePath}\" does not exist.");
			}

			string lines = File.ReadAllText(filePath);
			if (logOutput)
				log($"Read operation finished successfully for: \"{filePath}\".");
			return lines;
		}

		public static string Read(FileInfo fileInfo, bool logOutput = false)
			=> Read(fileInfo.FullName, logOutput);

		public static void Write(string filePath, string lines, bool logOutput = false)
		{
			File.WriteAllText(filePath, lines);
			if (logOutput)
				log($"Write operation finished successfully for: \"{filePath}\".");
		}

		public static void Write(FileInfo fileInfo, string lines, bool logOutput = false)
			=> Write(fileInfo.FullName, lines, logOutput);


		private static void log(string msg)
			=> UnityEngine.Debug.Log(msg);
	}
}