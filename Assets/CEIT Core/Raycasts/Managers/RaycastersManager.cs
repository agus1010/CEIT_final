using UnityEngine;

using CEIT.Raycasts;


namespace CEIT.Player
{
	public class RaycastersManager : MonoBehaviour
	{
		[Header("Raycasters:")]
		public CameraBasedPhysicsRaycaster cameraBasedPhysicsRaycaster;
		public CursorBasedPhysicsRaycaster cursorBasedPhysicsRaycaster;

		[Header("Init:")]
		[SerializeField] private bool cameraBasedIsDefault = true;

		public PhysicsRaycaster Current { get; private set; }


		public void SetCameraBasedAsActive()
			=> swap(cursorBasedPhysicsRaycaster, cameraBasedPhysicsRaycaster);

		public void SetCursorBasedAsActive()
			=> swap(cameraBasedPhysicsRaycaster, cursorBasedPhysicsRaycaster);


		private void Awake()
		{
			if (cameraBasedIsDefault)
				SetCameraBasedAsActive();
			else
				SetCursorBasedAsActive();
		}

		private void swap(PhysicsRaycaster toDeactivate, PhysicsRaycaster toActivate)
		{
			toDeactivate.gameObject.SetActive(false);
			toActivate.gameObject.SetActive(true);
			Current = toActivate;
		}
	}
}