using UnityEngine;
using UnityEngine.InputSystem;


namespace CEIT.Raycasts
{
	public class CursorBasedPhysicsRaycaster : CameraBasedPhysicsRaycaster
	{
		private Vector2 m_mousePos;
		private Vector3 m_viewportPoint;
		private Ray m_rayFromViewportPoint;

		protected override Ray makeRay()
		{
			m_mousePos = Mouse.current.position.ReadValue();
			m_viewportPoint = Origin.ScreenToViewportPoint(m_mousePos);
			m_rayFromViewportPoint = Origin.ViewportPointToRay(m_viewportPoint);
			return m_rayFromViewportPoint;
		}
	}
}