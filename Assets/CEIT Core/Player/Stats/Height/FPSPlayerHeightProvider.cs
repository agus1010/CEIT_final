using UnityEngine;


namespace CEIT.Player.Stats
{
	public class FPSPlayerHeightProvider : PlayerHeightProvider
	{
		[SerializeField] private PlayerStatsProvider statsProvider;
		[SerializeField] private PlayerSight sight;
		public override float Height => statsProvider.useDefaultStats? statsProvider.Height : sight.transform.position.y;
	}
}