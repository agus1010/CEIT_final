using UnityEngine;

using CEIT.Loading;
using CEIT.Persistence;
using CEIT.TimeAndSpace;


namespace CEIT.__booting__
{
	public class ResettablesResetter : MonoBehaviour
	{
		public GeoTranslationSystem geoTranslationSytem;
		public SunRotationSystem sunRotationSystem;
		public ModelLoadingOperationParameters modelLoadingOperationParameters;
		public SimulationLoader simulationLoader;
		public ItemPalette[] palettes;
		public SceneSetLoader[] sceneSetLoaders;


		private void OnEnable()
		{
			geoTranslationSytem.Reset();
			sunRotationSystem.Reset();
			modelLoadingOperationParameters.Reset();
			simulationLoader.Reset();
			foreach (var ssl in sceneSetLoaders)
				ssl.Reset();
		}

		private void Awake()
		{
			foreach (var palette in palettes)
			{
				palette.Reset();
			}
		}
	}
}