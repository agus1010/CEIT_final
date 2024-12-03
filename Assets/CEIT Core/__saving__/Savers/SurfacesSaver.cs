using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using CEIT.Environment;
using CEIT.Extensions;
using CEIT.Interactables;


namespace CEIT.Saving
{
	public class SurfacesSaver : CEITSaver<SurfaceHistoryData>
	{
		[Header("Config")]
		public APPLIED_SURFACE appliedSurface = APPLIED_SURFACE.MODEL;


		protected override IEnumerable<SurfaceHistoryData> ExtractDataFromExtractee()
			=> extractee.GetComponentsInChildren<SurfaceHistory>().Select(sh => extractSurfaceData(sh)).ToArray();

		protected override System.IO.FileInfo GetTargetFileInfo()
		{
			System.IO.FileInfo targetFile;
			switch (appliedSurface)
			{
				case APPLIED_SURFACE.MODEL:
					targetFile = runtimeVars.modelSurfaces;
					break;
				case APPLIED_SURFACE.APP_PROPS:
					targetFile = runtimeVars.appPropsSurfaces;
					break;
				case APPLIED_SURFACE.EXTERNAL_PROPS:
					targetFile = runtimeVars.externalPropsSurfacesDiff;
					break;
				default:
					targetFile = runtimeVars.modelSurfaces;
					break;
			}
			return targetFile;
		}


		private SurfaceHistoryData extractSurfaceData(SurfaceHistory surfaceHistory)
		{
			SurfaceHistoryData shd = new SurfaceHistoryData();
			var path = surfaceHistory.gameObject.GetComponentsInParent<Transform>().Select(t => t.name).Reverse();
			shd.pathInScene = path.ToArray();
			shd.surfaceId = surfaceHistory.Current.UID;
			shd.goPosition = surfaceHistory.transform.position.ToFloatArray();
			return shd;
		}
	}
}