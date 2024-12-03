using System.IO;
using UnityEngine;


namespace CEIT.Extensions
{
	public static class FileInfoExtensions
	{
		public static float LengthInKB(this FileInfo self)
			=> self.Length / 1024f;
		public static float LengthInMB(this FileInfo self)
			=> LengthInKB(self) / 1024f;
	}
}