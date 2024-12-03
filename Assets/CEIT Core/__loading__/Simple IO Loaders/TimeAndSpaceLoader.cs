using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using CEIT.Environment;
using CEIT.TimeAndSpace;


namespace CEIT.Loading
{
	public class TimeAndSpaceLoader : CEITSimpleIOLoaderBehaviour<TimeAndSpaceData>
	{
		[Header("System:")]
		public SunRotationSystem sunRotationSystem;


		protected override FileInfo GetTargetFileInfo()
			=> runtimeVars.simulationTime;

		protected override void LoadData(IEnumerable<TimeAndSpaceData> readData)
		{
			if (debug)
				print($"Loading time and space data.");
			TimeAndSpaceData data = readData.First();
			Quaternion sunRotation = new Quaternion
				(
					data.sunRotation[0],
					data.sunRotation[1],
					data.sunRotation[2],
					data.sunRotation[3]
				);
			sunRotationSystem.RotateTo(sunRotation);
		}
	}
}