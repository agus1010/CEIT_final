using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

using CEIT.Environment;
using CEIT.Extensions;
using CEIT.Interactables;
using CEIT.Persistence;


namespace CEIT.Loading
{
	public class SurfacesLoader : CEITSimpleIOLoaderBehaviour<SurfaceHistoryData>
	{
		[Header("Loading Target:")]
		public GameObject loadingTarget;

		[Header("Databases:")]
		public ItemDatabase surfaceDatabase;

		[Header("Config:")]
		public APPLIED_SURFACE appliedSurface = APPLIED_SURFACE.MODEL;


		protected override FileInfo GetTargetFileInfo()
		{
			FileInfo targetFile;
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

		protected override void LoadData(IEnumerable<SurfaceHistoryData> readData)
		{
			// filter
			readData = readData.Where(shd => shd.surfaceId != "Original");

			int dataCount = readData.Count();

			SurfaceHistory[] allPosibleTargets = loadingTarget.GetComponentsInChildren<SurfaceHistory>();

			SurfaceHistory currentTargetHistory;
			int iter = 0;
			int surfacesApplied = 0;

			foreach (var shd in readData)
			{
				iter += 1;
				currentTargetHistory = matchAndPopFromPossibleTargets(allPosibleTargets, shd);
				if(currentTargetHistory != null)
				{
					if(tryApplySurface(currentTargetHistory, shd))
					{
						surfacesApplied += 1;
					}
				}
				eventsChannel.FireProgressMade(iter / dataCount);
			}

			if (debug)
				print($"Applied {surfacesApplied}/{dataCount} altered surfaces.");
		}


		private SurfaceHistory matchAndPopFromPossibleTargets(IEnumerable<SurfaceHistory> allSurfaceHistories, SurfaceHistoryData shd)
		{
			string shdPath = joinWithPoints(shd.pathInScene);
			string currentShPath;
			foreach (var sh in allSurfaceHistories)
			{
				currentShPath = joinWithPoints(getGameObjectPath(sh.gameObject));
				if (currentShPath == shdPath && shd.goPosition.ToVector3() == sh.transform.position)
				{
					allSurfaceHistories.ToList().Remove(sh);
					return sh;
				}
			}
			return null;
		}

		private bool tryApplySurface(SurfaceHistory history, SurfaceHistoryData historyData) 
		{
			Surface surface;
			if (historyData.surfaceId != "Original")
			{
				surface = surfaceDatabase[historyData.surfaceId] as Surface;
				if (surface != null)
				{
					history.Paint(surface);
					return true;
				}
			}
			return false;
		}

		private IEnumerable<string> getGameObjectPath(GameObject go)
		{
			Transform parent = go.transform;
			List<string> parents = new List<string>();
			while(parent != null)
			{
				parents.Add(parent.name);
				parent = parent.parent;
			}
			parents.Reverse();
			return parents;
		}
			//=> go.GetComponentsInParent<Transform>().Select(t => t.name).Reverse();

		private string joinWithPoints(IEnumerable<string> elems)
			=> string.Join('.', elems);
	}
}