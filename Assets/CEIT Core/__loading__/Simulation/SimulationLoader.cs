using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

using CEIT.Environment;


namespace CEIT.Loading
{
	[CreateAssetMenu(fileName = "New Simulation Loader", menuName = "CEIT/Loaders/Simulation Loader")]
	public class SimulationLoader : ScriptableObject, ICEITLoader
	{
		[Header("Channel:")]
		public CEITOperationEventsChannel channel;

		[Header("Channels Heared:")]
		public CEITOperationEventsChannel[] hearedChannels;

		[Header("Debug?")]
		[SerializeField] private bool debug = false;

		public float progress { get; private set; } = 0f;

		private LoaderListener[] listeners;
		private CancellationTokenSource listenersObserverCTS;


		public void Reset()
		{
			progress = 0f;
			Cancel();
		}

		public void PerformLoadingOperation()
		{
			listeners = new LoaderListener[hearedChannels.Length];
			for (int i = 0; i < listeners.Length; i++)
				listeners[i] = new LoaderListener(hearedChannels[i]);

			bootListenersObserverDaemon();
			
			channel?.FireStarted();
		}

		public void Cancel()
		{
			if (listenersObserverCTS != null && !listenersObserverCTS.IsCancellationRequested && listenersObserverCTS.Token.CanBeCanceled)
				listenersObserverCTS.Cancel();
		}

		private void bootListenersObserverDaemon()
		{
			Cancel();
			listenersObserverCTS?.Dispose();
			listenersObserverCTS = new CancellationTokenSource();
			listenersObserverDaemon();
		}


		private async void listenersObserverDaemon()
		{
			System.Func<bool> areOperationsRunning = () => listeners.Where(l => (int)l.status >= 10).Count() < listeners.Length;
			float currentProgress = 0f;
			try
			{
				while (areOperationsRunning())
				{
					listenersObserverCTS.Token.ThrowIfCancellationRequested();
					currentProgress = listeners.Sum(l => l.progress);
					if(currentProgress != progress)
					{
						progress = currentProgress;
						channel?.FireProgressMade(progress);
					}
					await Task.Delay(1000);
				}
				channel?.FireFinished();
			}
			catch (System.OperationCanceledException)
			{
				channel?.FireCancelled();
			}
			finally
			{
				foreach (var listener in listeners)
					listener.Dispose();
				listenersObserverCTS.Dispose();
				listenersObserverCTS = null;
			}
		}
	}
}