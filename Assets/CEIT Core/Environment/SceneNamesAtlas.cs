using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace CEIT.Environment
{
	public enum CEITSceneClassification
	{
		PLAYER_BODY,
		PLAYER_INPUT_FULL,
		PLAYER_INPUT_REDUCED,
		MODEL_SELECTION_ENVIRONMENT,
		MODEL_LOADING_TARGET,
		PROPS_LOADING_TARGET,
		LIGHTING_LOADING_TARGET,
		PLAYER_UI_FULL,
		PLAYER_UI_REDUCED,
		MODEL_SELECTION_UI
	}


	[CreateAssetMenu(fileName = "New Scene Names Atlas", menuName = "CEIT/Persistence/Scene Names Atlas")]
	public class SceneNamesAtlas : ScriptableObject, IReadOnlyDictionary<CEITSceneClassification, Scene>
	{
		[Header("Scene Names:")]

		[Header("Player:")]
		[SerializeField] private string body;

		[Header("Input")]
		[SerializeField] private string full;
		[SerializeField] private string reduced;

		[Header("Map Selection:")]
		[SerializeField] private string environment;

		[Header("Loading Targets:")]
		[SerializeField] private string model;
		[SerializeField] private string props;
		[SerializeField] private string lighting;

		[Header("UI:")]
		[SerializeField] private string fullPlayerUI;
		[SerializeField] private string reducedPlayerUI;
		[SerializeField] private string mapSelectionUI;


		public string playerBodySceneName => body;
		public string fullInputSceneName => full;
		public string reducedInputSceneName => reduced;
		public string modelSelectionEnvironmentSceneName => environment;
		public string modelLoadingTargetSceneName => model;
		public string propsLoadingTargetSceneName => props;
		public string lightingLoadingTargetSceneName => lighting;
		public string fullPlayerUISceneName => fullPlayerUI;
		public string reducedPlayerUISceneName => reducedPlayerUI;
		public string mapSelectionUISceneName => mapSelectionUI;

		public Scene playerBodyScene => GetScene(body);
		public Scene fullInputScene => GetScene(full);
		public Scene reducedInputScene => GetScene(reduced);
		public Scene modelSelectionEnvironmentScene => GetScene(environment);
		public Scene modelLoadingTargetScene => GetScene(model);
		public Scene propsLoadingTargetScene => GetScene(props);
		public Scene lightingLoadingTargetScene => GetScene(lighting);
		public Scene fullPlayerUIScene => GetScene(fullPlayerUI);
		public Scene reducedPlayerUIScene => GetScene(reducedPlayerUI);
		public Scene mapSelectionUIScene => GetScene(mapSelectionUI);

		public Scene this[CEITSceneClassification key] => GetScene(key);

		public int Count => Keys.Count();
		public IEnumerable<CEITSceneClassification> Keys => System.Enum.GetValues(typeof(CEITSceneClassification)).Cast<CEITSceneClassification>();
		public IEnumerable<Scene> Values => Keys.Select(sceneClass => GetScene(sceneClass));

		public IDictionary<CEITSceneClassification, Scene> ToDictionary
		{
			get
			{
				var zip = Keys.Zip(Values, (key, value) => new KeyValuePair<CEITSceneClassification, Scene>(key, value));
				return zip.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
			}
		}


		public bool ContainsKey(CEITSceneClassification key) => true;

		public string GetSceneName(CEITSceneClassification sceneClassification)
		{
			string sceneName;
			switch (sceneClassification)
			{
				case CEITSceneClassification.PLAYER_BODY:
					sceneName = playerBodySceneName;
					break;
				case CEITSceneClassification.PLAYER_INPUT_FULL:
					sceneName = fullInputSceneName;
					break;
				case CEITSceneClassification.PLAYER_INPUT_REDUCED:
					sceneName = reducedInputSceneName;
					break;
				case CEITSceneClassification.MODEL_SELECTION_ENVIRONMENT:
					sceneName = modelSelectionEnvironmentSceneName;
					break;
				case CEITSceneClassification.MODEL_LOADING_TARGET:
					sceneName = modelLoadingTargetSceneName;
					break;
				case CEITSceneClassification.PROPS_LOADING_TARGET:
					sceneName = propsLoadingTargetSceneName;
					break;
				case CEITSceneClassification.LIGHTING_LOADING_TARGET:
					sceneName = lightingLoadingTargetSceneName;
					break;
				case CEITSceneClassification.PLAYER_UI_FULL:
					sceneName = fullPlayerUISceneName;
					break;
				case CEITSceneClassification.PLAYER_UI_REDUCED:
					sceneName = reducedPlayerUISceneName;
					break;
				case CEITSceneClassification.MODEL_SELECTION_UI:
					sceneName = mapSelectionUISceneName;
					break;
				default:
					throw new KeyNotFoundException($"The key {sceneClassification} was not found.");
			}
			return sceneName;
		}

		public Scene GetScene(string sceneName)
			=> SceneManager.GetSceneByName(sceneName);

		public Scene GetScene(CEITSceneClassification sceneClassification)
			=> GetScene(GetSceneName(sceneClassification));

		public bool TryGetValue(CEITSceneClassification key, out Scene value)
		{
			value = GetScene(key);
			return true;
		}

		public IEnumerator<KeyValuePair<CEITSceneClassification, Scene>> GetEnumerator()
			=> ToDictionary.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator()
			=> ToDictionary.GetEnumerator();
	}
}