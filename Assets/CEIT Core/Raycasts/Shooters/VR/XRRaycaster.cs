using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;


namespace CEIT.Raycasts
{
	public class XRRaycaster : BaseRaycaster
	{
		[SerializeField] private XRRayInteractor xRRayInteractor;


		private bool m_Hit;
		private RaycastHit? m_RaycastHit;
		private int m_RaycastHitIndex;
		private RaycastResult? m_RaycastResult;
		private int m_RaycastResultIndex;
		private bool m_uiIsClosest;


		public override IShotResult Shoot(ShotFilter shotFilter = ShotFilter.SOLIDS)
		{
			m_Hit = xRRayInteractor.TryGetCurrentRaycast(
				out m_RaycastHit,
				out m_RaycastHitIndex,
				out m_RaycastResult,
				out m_RaycastResultIndex,
				out m_uiIsClosest
			);

			return !m_Hit ?
				new XRRaycastShotResult() :
				new XRRaycastShotResult
				(
					transform,
					m_RaycastHit,
					m_RaycastHitIndex,
					m_RaycastResult,
					m_RaycastResultIndex,
					m_uiIsClosest
				);
		}
	}
}