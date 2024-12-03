using System.Collections.Generic;
using System.IO;

using CEIT.Utils;


namespace CEIT.Loading
{
	public abstract class CEITSimpleIOLoaderBehaviour<T> : CEITLoaderBehaviour where T : struct
	{
		protected override void performLoad()
		{
			FileInfo targetFile = GetTargetFileInfo();
			if (targetFile == null)
				throw new FileNotFoundException("Target file doesn't exist.");
			IEnumerable<T> data = ReadData(targetFile);
			LoadData(data);
		}

		protected IEnumerable<T> ReadData(FileInfo fileInfo)
		{
			string lines;
			lines = CEITIOHandler.Read(fileInfo, debug);
			IEnumerable<T> data = Jsonificator.FromJson<T>(lines);
			return data;
		}

		protected abstract FileInfo GetTargetFileInfo();

		protected abstract void LoadData(IEnumerable<T> readData);
	}
}