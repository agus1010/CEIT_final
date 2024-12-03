using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;


namespace CEIT.Raycasts
{
	public class VRHandUIRaycaster : UIRaycaster
	{
		[Header("Scene Elements:")]
		public XRRayInteractor xrRayInteractor;

		public override bool isPointingAtGraphics => xrRayInteractor.IsOverUIGameObject();


		private RaycastResult m_raycastResult;
		protected override UIShotResult makeUIShot()
		{
			xrRayInteractor.TryGetCurrentUIRaycastResult(out m_raycastResult);
			return new UIShotResult
				(
					m_raycastResult.gameObject,
					m_raycastResult.distance,
					xrRayInteractor.maxRaycastDistance
				);
		}


		private void Start()
		{
			xrRayInteractor.maxRaycastDistance = Stats.MaxReachDistance;
		}
	}
}