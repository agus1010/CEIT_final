using UnityEngine.Events;

using CEIT.Environment;
using CEIT.Loading;
using CEIT.Loading.Models.Utils;

namespace CEITUI.Elements.MapSelector.Assets
{
	public class LoadSimulationMapSelectorView : MapSelectorView
	{
		public HeaderController headerController;
		public AddedGroundController addedGroundController;
		public ModelLoadingOperationParameters parameters;
		public SavingAndLoadingRuntimeVariables runtimeVars;
		public SimulationLoader simulationLoader;

		public override void UpdateGraphics()
		{
			base.UpdateGraphics();
			headerController.backtrackingAllowed = false;
			runtimeVars.SetValuesFromParameters(parameters);
			addedGroundController.isVisible = isCurrentlyActive;
			if(parameters.mapFile != null)
				simulationLoader.PerformLoadingOperation();
		}


		private void OnApplicationQuit()
		{
			simulationLoader?.Cancel();
		}
	}
}