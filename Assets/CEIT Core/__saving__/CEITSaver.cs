using System.Collections.Generic;
using System.IO;
using UnityEngine;

using CEIT.Environment;
using CEIT.Utils;


namespace CEIT.Saving
{
	public abstract class CEITSaver<T> : MonoBehaviour where T : struct
	{
		[Header("Environment Variables:")]
		public SavingAndLoadingRuntimeVariables runtimeVars;

		[Header("Events:")]
		public CEITOperationEventsChannel eventsChannel;

		[Header("Extracted GameObject:")]
		public GameObject extractee;

		[Header("Debug:")]
		[SerializeField] protected bool debug = false;


		public void PerformSaveOperation()
		{
			if (debug)
				print($"STARTED SAVING operation in component: {transform.parent.name}.{name}->{this.GetType()}");
			eventsChannel?.FireStarted();
			FileInfo targetFile = GetTargetFileInfo();
			if(targetFile != null)
			{
				IEnumerable<T> extractedData;
				try
				{
					extractedData = ExtractDataFromExtractee();
					eventsChannel?.FireProgressMade(0.5f);
					SaveJSONifed(extractedData, targetFile, debug);
					eventsChannel?.FireFinished();
					if (debug)
						print($"FINISHED LOADING operation in component: {transform.parent.name}.{name}->{this.GetType()}");
				}
				catch(System.Exception e)
				{
					Debug.LogException(e);
					eventsChannel?.FireError(e);
					if (debug)
						print($"SAVING operation CANCELLED in component {transform.parent.name}.{name}->{this.GetType()}, because of error: {e}");
				}
			}
			else
			{
				System.Exception e = new FileNotFoundException("SAVING OPERATION ERROR: No target file selected.");
				eventsChannel?.FireError(e);
				if (debug)
					print($"SAVING operation CANCELLED in component {transform.parent.name}.{name}->{this.GetType()}, because of error: {e}");
			}
		}

		protected abstract IEnumerable<T> ExtractDataFromExtractee();

		protected abstract FileInfo GetTargetFileInfo();

		protected void SaveJSONifed(IEnumerable<T> structs, FileInfo targetFile, bool showLogOutput = false)
		{
			string serializedObjects = Jsonificator.ToJson(structs);
			CEITIOHandler.Write(targetFile, serializedObjects, showLogOutput);
		}
	}
}