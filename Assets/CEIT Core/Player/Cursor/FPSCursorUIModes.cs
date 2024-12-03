using UnityEngine;
using UnityEngine.InputSystem;


namespace CEIT.Player.FPS
{
	public abstract class FPSPointerUIMode
	{
		public static bool CursorVisible
		{
			get => Cursor.visible;
			set => Cursor.visible = value;
		}
		public static bool CursorLocked
		{
			get => Cursor.lockState == CursorLockMode.Locked;
			set => Cursor.lockState = value ? CursorLockMode.Locked : CursorLockMode.None;
		}
		public abstract void UpdateRun(IUIShotResult uiShotResult);
	}


	public class ScreenSpaceUIMode : FPSPointerUIMode
	{
		public override void UpdateRun(IUIShotResult uiShotResult)
		{
			if (!CursorVisible) CursorVisible = true;
			if (CursorLocked) CursorLocked = false;
		}
	}


	public class WorldSpaceUIMode : FPSPointerUIMode
	{
		private Vector3 _screenCenter;

		public WorldSpaceUIMode(Camera camera)
		{
			_screenCenter = camera.ViewportToScreenPoint(new Vector3(.5f, .5f, .5f));
		}


		public override void UpdateRun(IUIShotResult uiShotResult)
		{
			if (CursorVisible) CursorVisible = false;

			if (uiShotResult.Hit)
			{
				if (CursorLocked)
					CursorLocked = false;
				else
					Mouse.current.WarpCursorPosition(_screenCenter);
			}
			else
			{
				if (!CursorLocked)
					CursorLocked = true;
			}
		}
	}
}