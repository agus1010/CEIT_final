using UnityEngine;


namespace CEIT.Player.FPS
{
    public enum PointerUIMode { SCREEN, WORLD }
    public class FPSCursorManager : MonoBehaviour
    {
        public PlayerPointer pointer;

        [Header("Debug:")]
        [SerializeField] private bool debug = false;
        [Space(7)]
        [SerializeField] private CursorLockMode currentLockMode;
        [SerializeField] private PointerUIMode currentPointerMode = PointerUIMode.SCREEN;

        public bool CanUnlockCursor { get; set; } = false;
        public FPSPointerUIMode UIMode { get; private set; }

        private FPSPointerUIMode m_screenSpaceMode;
        private FPSPointerUIMode m_worldSpaceMode;

        private FPSPointerUIMode targetUIMode => currentPointerMode == PointerUIMode.SCREEN ? m_screenSpaceMode : m_worldSpaceMode;


		public void LockCursor()
		    => SetCursorSettings(CursorLockMode.Locked, false, PointerUIMode.WORLD);

        public void UnlockCursor()
            => SetCursorSettings(CursorLockMode.None, true, PointerUIMode.SCREEN);

		public void SetCursorSettings(CursorLockMode lockMode, bool visible, PointerUIMode pointerUIMode)
		{
			currentLockMode = lockMode;
			Cursor.lockState = currentLockMode;
			Cursor.visible = visible;
            currentPointerMode = pointerUIMode;
            UIMode = targetUIMode;
		}


		private void Awake()
        {
            m_screenSpaceMode = new ScreenSpaceUIMode();
            m_worldSpaceMode = new WorldSpaceUIMode(Camera.main);
            LockCursor();
        }

        private void Update()
		{
            if(debug && (currentLockMode != Cursor.lockState || UIMode != targetUIMode))
            {
				if (currentLockMode == CursorLockMode.None)
					UnlockCursor();
				else
					SetCursorSettings(currentLockMode, false, currentPointerMode);
            }
            UIMode.UpdateRun(pointer.CurrentUIShot);
		}
	}
}