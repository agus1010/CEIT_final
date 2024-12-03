using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

using CEIT.Environment;
using CEIT.Extensions;
using CEIT.Interactables;
using CEIT.Persistence;


namespace CEIT.Loading
{
	public class PropsLoader : CEITSimpleIOLoaderBehaviour<PropBehaviourData>
	{
		[Header("Loading Target:")]
		public GameObject loadingTarget;
		
		[Header("Databases:")]
		public ItemDatabase propDatabase;
		public ItemDatabase surfaceDatabase;

		[Header("Config:")]
		public PROPS_ORIGIN propsOrigin = PROPS_ORIGIN.APPLICATION;


		protected override FileInfo GetTargetFileInfo()
			=> propsOrigin == PROPS_ORIGIN.APPLICATION ?
				runtimeVars.appProps :
				runtimeVars.externalPropsDiff;

		protected override async void LoadData(IEnumerable<PropBehaviourData> readData)
		{
			await loadGOsFromReadData(readData.ToArray());
		}

		private IEnumerable<System.Tuple<PropPartBehaviourData, PropPartBehaviour>> zipPartsDataToParts(PropPartBehaviourData[] data, PropPartBehaviour[] parts)
			=> data.Zip(
				parts,
				(first, second)
					=> new System.Tuple<PropPartBehaviourData, PropPartBehaviour>(first, second)
			);

		private async Task loadGOsFromReadData(PropBehaviourData[] readData)
		{
			PropBehaviourData pbd;
			GameObject newGo;
			for (int i = 0; i < readData.Length; i++)
			{
				pbd = readData[i];
				newGo = Instantiate((propDatabase[pbd.uid] as Prop).prefab, pbd.position.ToVector3(), pbd.eulerRotation.ToQuaternion());
				newGo.transform.SetParent(loadingTarget.transform);
				newGo.transform.localScale = pbd.localScale.ToVector3();
				await loadSurfacesIntoGO(newGo, pbd);
				eventsChannel?.FireProgressMade((float)(i / readData.Length));
			}
		}

		private async Task loadSurfacesIntoGO(GameObject go, PropBehaviourData pbd)
		{
			Surface surface;
			var zip = zipPartsDataToParts(pbd.parts, go.GetComponentsInChildren<PropPartBehaviour>()).ToArray();
			foreach (var tuple in zip)
			{
				surface = surfaceDatabase[tuple.Item1.currentSurfaceId] as Surface;
				if (surface != null)
				{
					tuple.Item2.GetComponent<SurfaceHistory>().Paint(surface);
				}
				await Task.Yield();
			}
		}
	}
}