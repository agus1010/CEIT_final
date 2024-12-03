using UnityEngine;


namespace CEIT.Player.VR
{
    public class CEITXRInteractorLineVisual : UnityEngine.XR.Interaction.Toolkit.XRInteractorLineVisual
	{
        [Header("CEIT References:")]
        [SerializeField] private PlayerPointer pointer;
		[SerializeField] private Color uiColor;
		[SerializeField] private Stats.PlayerStatsProvider statsProvider;

		private Color interactionColor;


        public void SetValidColorGradientFromInteraction(Persistence.Interaction interaction)
            => interactionColor = interaction.BaseColor;


		private Color m_color;
		public void OnPointerChangedTarget(GameObject newTarget)
		{
			if (pointer.ClosestTarget != null)
			{
				m_color = pointer.IsLookingAtGraphics ? uiColor : interactionColor;
				setGradientColor(m_color);
			}
		}

		public void ResetMaxLineLength()
			=> lineLength = statsProvider.MaxReachDistance;



		private void setGradientColor(Color color)
			=> validColorGradient.colorKeys[0].color = color;


		private void Start()
		{
			ResetMaxLineLength();
		}
	}
}
