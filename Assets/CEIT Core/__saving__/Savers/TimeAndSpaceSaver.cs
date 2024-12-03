using System.Collections.Generic;

using CEIT.Environment;
using CEIT.Extensions;
using CEIT.TimeAndSpace;


namespace CEIT.Saving
{
	public class TimeAndSpaceSaver : CEITSaver<TimeAndSpaceData>
	{
		[UnityEngine.Header("External System:")]
		public SunRotationSystem sunRotationSystem;

		protected override IEnumerable<TimeAndSpaceData> ExtractDataFromExtractee()
		{
			TimeAndSpaceData data = new TimeAndSpaceData();
			data.sunRotation = sunRotationSystem.CurrentSunRotation.ToFloatArray();
			return new TimeAndSpaceData[] { data };
		}

		protected override System.IO.FileInfo GetTargetFileInfo()
			=> runtimeVars.simulationTime;
	}
}