using CEIT.Player;

using UnityEngine;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Free Roam Behaviour", menuName = "CEIT/Persistence/Behaviours/Free Roam")]
	public class FreeRoamBehaviour : InteractionBehaviour
	{
		public override void TryPerformPrimary(bool newPrimaryValue) { }

		public override void TryPerformSecondary(bool newSecondaryValue) { }
	}
}