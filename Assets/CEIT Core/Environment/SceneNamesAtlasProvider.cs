using UnityEngine;


namespace CEIT.Environment
{
	[CreateAssetMenu(fileName = "New Scene Names Atlas Provider", menuName = "CEIT/Persistence/Scene Names Atlas Provider")]
	public class SceneNamesAtlasProvider : ScriptableObject
	{
		[Header("Config:")]
		[SerializeField] private __booting__.HardwareTypeProvider hardwareTypeProvider;

		[Header("Atlases:")]
		[SerializeField] private SceneNamesAtlas fpsAtlas;
		[SerializeField] private SceneNamesAtlas vrAtlas;

		public SceneNamesAtlas CurrentAtlas 
			=> hardwareTypeProvider.hardwareType == __booting__.HardwareType.PC ? fpsAtlas : vrAtlas;
	}
}