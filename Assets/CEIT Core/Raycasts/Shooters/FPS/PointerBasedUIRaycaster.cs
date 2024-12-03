using UnityEngine;
using UnityEngine.InputSystem.UI;


namespace CEIT.Raycasts
{
	public class PointerBasedUIRaycaster : UIRaycaster
	{
		[Header("Scene Elements:")]
		public InputSystemUIInputModule uiInput;

		[Header("Config:")]
		public int PointerId = 0;

		public override bool isPointingAtGraphics => uiInput.IsPointerOverGameObject(PointerId);


		protected override UIShotResult makeUIShot()
			=> new UIShotResult
				(
					uiInput.GetLastRaycastResult(0).gameObject,
					uiInput.GetLastRaycastResult(0).distance,
					Stats.MaxReachDistance
				);
	}
}