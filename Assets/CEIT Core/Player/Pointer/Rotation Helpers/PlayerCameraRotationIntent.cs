using UnityEngine;


namespace CEIT.Player.Pointer
{
	public class PlayerCameraRotationIntent : PlayerRotationIntentProvider
	{
		[SerializeField] private Camera playerCamera;
		public override Vector3 IntendedRotations => playerCamera.transform.rotation.eulerAngles;
	}
}