using System.Collections.Generic;
using UnityEngine;

using CEIT.Environment;
using CEIT.Extensions;
using CEIT.Interactables;


namespace CEIT.Saving
{
	public class PropsSaver : CEITSaver<PropBehaviourData>
	{
		[Header("Config:")]
		public PROPS_ORIGIN propsOrigin = PROPS_ORIGIN.APPLICATION;


		protected override IEnumerable<PropBehaviourData> ExtractDataFromExtractee()
		{
			int childCount = extractee.transform.childCount;
			PropBehaviourData[] sceneBehavioursData = new PropBehaviourData[childCount];
			if (debug)
				print($"Serializing {childCount} props.");
			for (int i = 0; i < childCount; i++)
			{
				sceneBehavioursData[i] = extractBehaviourData(extractee.transform.GetChild(i).gameObject);
			}
			return sceneBehavioursData;
		}

		protected override System.IO.FileInfo GetTargetFileInfo()
			=> propsOrigin == PROPS_ORIGIN.APPLICATION ?
				runtimeVars.appProps :
				runtimeVars.externalPropsDiff;


		private PropBehaviourData extractBehaviourData(GameObject go)
		{
			PropBehaviour pb = go.GetComponent<PropBehaviour>();
			PropBehaviourData pbd = new PropBehaviourData();
			pbd.uid = pb.uid;
			pbd.position = pb.transform.position.ToFloatArray();
			pbd.eulerRotation = pb.transform.rotation.ToFloatArray();
			pbd.localScale = pb.transform.localScale.ToFloatArray();
			pbd.parts = extractAllPartsBehaviourData(go);
			return pbd;
		}

		private PropPartBehaviourData[] extractAllPartsBehaviourData(GameObject go)
		{
			PropPartBehaviour[] parts = go.GetComponentsInChildren<PropPartBehaviour>();
			PropPartBehaviourData[] partsData = new PropPartBehaviourData[parts.Length];
			PropPartBehaviourData current;
			for (int i = 0; i < parts.Length; i++)
			{
				current = new PropPartBehaviourData();
				current.currentSurfaceId = parts[i].GetComponent<SurfaceHistory>().Current.UID;
				partsData[i] = current;
			}
			return partsData;
		}
	}
}