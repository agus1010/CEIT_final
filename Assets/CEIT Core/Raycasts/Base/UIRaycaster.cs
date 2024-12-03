namespace CEIT.Raycasts
{
	public abstract class UIRaycaster : BaseRaycastShooter
	{
		public abstract bool isPointingAtGraphics { get; }

		[UnityEngine.Header("Debug:")]
		[UnityEngine.SerializeField] private bool debug = false;


		private UIShotResult m_uiShotResult;
		public override IShotResult Shoot(ShotFilter shotFilter = ShotFilter.SOLIDS)
		{
			m_uiShotResult =
				isPointingAtGraphics ?
					makeUIShot() :
					new UIShotResult();
			if (debug)
				print(m_uiShotResult);
			return m_uiShotResult;
		}

		
		protected abstract UIShotResult makeUIShot();
	}
}