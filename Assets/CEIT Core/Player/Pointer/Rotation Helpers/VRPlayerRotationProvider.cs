using UnityEngine;


namespace CEIT.Player.Pointer
{
	[RequireComponent(typeof(PlayerPointer))]
	public class VRPlayerRotationProvider : PlayerRotationIntentProvider
	{
		public GameObject rightHand;

		public override Vector3 IntendedRotations
		{
			get
			{
				Vector3 eulers = rightHand.transform.eulerAngles;
				return new Vector3(eulers.x, eulers.y, -1 * eulers.z);
			}
		}

	}
}