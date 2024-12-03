using UnityEngine;
using UnityEngine.Events;


namespace CEIT.__booting__
{
	public enum GameObjectMoment
	{
		OnEnable, Awake, Start
	}

	public class HardwareDependantSceneInitializer : MonoBehaviour
	{
		[Header("Config:")]
		public HardwareTypeProvider hardwareTypeProvider;
		public GameObjectMoment initMoment = GameObjectMoment.Start;

		[Header("Debug:")]
		[SerializeField] private bool debug = false;

		[Header("Events")]
		public UnityEvent OnVRInit;
		public UnityEvent OnPCInit;



		private void OnEnable()
		{
			if (initMoment == GameObjectMoment.OnEnable)
				fireEvents();
		}

		private void Awake()
		{
			if (initMoment == GameObjectMoment.Awake)
				fireEvents();
		}

		private void Start()
		{
			if (initMoment == GameObjectMoment.Start)
				fireEvents();
		}


		private void fireEvents()
		{
			var hardwareType = hardwareTypeProvider.hardwareType;
			if (debug)
				print($"Hardware type is: {hardwareType}.");
			if (hardwareType == HardwareType.VR)
				OnVRInit?.Invoke();
			else
				OnPCInit?.Invoke();
		}
	}
}