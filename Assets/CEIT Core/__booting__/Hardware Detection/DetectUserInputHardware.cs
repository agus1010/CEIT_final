using UnityEngine;
using UnityEngine.XR;


namespace CEIT.XR
{
	public class DetectUserInputHardware : MonoBehaviour
	{
		[Header("Player GameObjects")]
		[SerializeField] private GameObject xrPlayerPrefab;
		[SerializeField] private GameObject fpsPlayerPrefab;


		public bool IsXR { get; private set; }



		
		private void Awake()
		{
			bool xrDeviceActive = XRSettings.isDeviceActive;

			if (XRSettings.loadedDeviceName == "MockHMD Display")
				Debug.LogWarning("  ! ¡ ! ¡ ! ¡ ! ¡  MockHMD ACTIVE. Remember to TURN OFF when making FINAL BUILD.");

			_enableInputTypes(!xrDeviceActive, xrDeviceActive);
		}


		private void _enableInputTypes(bool fps, bool xr)
		{
			if (fpsPlayerPrefab != null)
				fpsPlayerPrefab.SetActive(fps);

			if (xrPlayerPrefab != null)
				xrPlayerPrefab.SetActive(xr);
		}
	}
}