using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

using CEIT.Environment;


namespace CEIT.Loading
{
	[CreateAssetMenu(fileName = "New Scene Set Loader", menuName = "CEIT/Loaders/Scene Set Loader")]
	public class SceneSetLoader : ScriptableObject, ICEITLoader
	{
		[Header("Atlas:")]
		public SceneNamesAtlasProvider atlasProvider;

		[Header("Scenes:")]
		public CEITSceneClassification[] set;

		[Header("Options:")]
		public int activeSceneInSet = -1;

		[Header("Events:")]
		public CEITOperationEventsChannel eventsChannel;

		[Header("Debug:")]
		public bool debug = false;


		private SceneNamesAtlas atlas => atlasProvider.CurrentAtlas;
		//items.GroupBy(x => x.Id).Select(y => y.First());
		private IEnumerable<string> setScenesNames => set.Select(sc => atlas.GetSceneName(sc)).ToHashSet();

		private CancellationTokenSource cancellationTokenSource;
		private CancellationToken cancellationToken;
		private AsyncOperation[] setLoadingOps;
		private string[] loadedScenesNames;
		private Task loadingScenesTask;


		public async void PerformLoadingOperation()
		{
			var scenesToLoad = getScenesToLoad();
			eventsChannel?.FireStarted();
			if (debug)
				Debug.Log("Scene set loading STARTED");

			cancellationTokenSource = new CancellationTokenSource();
			cancellationToken = cancellationTokenSource.Token;
			loadingScenesTask = loadNecesaryScenes(scenesToLoad);
			try
			{
				await loadingScenesTask;
				setCorrespondingActiveScene();
				eventsChannel?.FireFinished();
				if (debug)
					Debug.Log("Scene set loading FINISHED");
			}
			catch (OperationCanceledException)
			{
				if (debug)
					Debug.LogWarning("Scene set loading CANCELLED. Unloading loaded scenes.");
				await performUnloadingOfScenes(loadedScenesNames);
				eventsChannel?.FireCancelled();
			}
			finally
			{
				cancellationTokenSource.Dispose();
				cancellationTokenSource = null;
			}
		}

		public async void PerformUnloadingOperationOfScenesOutsideSet()
			=> await performUnloadingOfScenes(false);

		public async void PerformUnloadingOperationOfScenesWithinSet()
			=> await performUnloadingOfScenes(true);

		public async void PerformUnloadingOperationOfLoadedScenes()
		{
			await performUnloadingOfScenes(loadedScenesNames);
		}
		
		public void Cancel()
		{
			if(cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
				if(debug)
					Debug.LogWarning("Cancellation requested for scene set loading operation");
			}
		}

		public void Reset()
		{
			Cancel();
			setLoadingOps = null;
		}

		private IEnumerable<string> getScenesToLoad()
			=> set
				.Select(sc => atlas.GetSceneName(sc))
				.Where(name => atlas.GetScene(name).buildIndex == -1);

		private async Task loadNecesaryScenes(IEnumerable<string> scenesToLoad)
		{
			populateLoadingOps(scenesToLoad.ToArray());
			float currentProgress = 0f;
			float prevProgress = 0f;
			float maxProgress = setLoadingOps.Length;
			do
			{
				prevProgress = currentProgress;
				await Task.Delay(5);
				if (cancellationToken.IsCancellationRequested)
				{
					cancellationToken.ThrowIfCancellationRequested();
				}
				currentProgress = setLoadingOps.Sum(op => op.progress) / maxProgress;
				if (debug)
					Debug.Log($"Scene set loading operation PROGRESS = {currentProgress}");
				eventsChannel?.FireProgressMade(currentProgress);
			}
			while (currentProgress < 1f || prevProgress > currentProgress);
		}

		private async Task performUnloadingOfScenes(bool inSet)
		{
			loadedScenesNames = setScenesNames.ToArray();
			var scenesToUnload = new Range(0, SceneManager.sceneCount)
				.Select(i => SceneManager.GetSceneAt(i))
				.Where(s => inSet ? loadedScenesNames.Contains(s.name) : !loadedScenesNames.Contains(s.name))
				.Select(s => s.name)
				.ToArray();
			await performUnloadingOfScenes(scenesToUnload);
		}

		private async Task performUnloadingOfScenes(string[] scenesToUnload)
		{
			Scene s;
			foreach (var name in scenesToUnload)
			{
				s = SceneManager.GetSceneByName(name);
				if(s.buildIndex != -1)
				{
					SceneManager.UnloadSceneAsync(s);
					await Task.Yield();
				}
			}
			if (debug)
				Debug.Log($"All scenes where unloaded.");
		}

		private void populateLoadingOps(string[] scenesToLoad)
		{
			setLoadingOps = new AsyncOperation[scenesToLoad.Length];
			for (int i = 0; i < setLoadingOps.Length; i++)
			{
				setLoadingOps[i] = SceneManager.LoadSceneAsync(scenesToLoad[i], LoadSceneMode.Additive);
				setLoadingOps[i].allowSceneActivation = true;
			}
		}

		private void setCorrespondingActiveScene()
		{
			if (activeSceneInSet > 0 && activeSceneInSet < setLoadingOps.Length)
			{
				Scene newActive = atlas.GetScene(set[activeSceneInSet]);
				SceneManager.SetActiveScene(newActive);
			}
		}
	}
}