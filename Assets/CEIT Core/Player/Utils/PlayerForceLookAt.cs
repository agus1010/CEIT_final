using UnityEngine;

using CEIT.Player;


namespace CEIT.Utils
{
	[System.Obsolete(message: "Please use PlayerSightUtils instead.")]
	public class PlayerForceLookAt : MonoBehaviour
	{
		public int frameDelay = 60;

		[Header("Debug.. Do not fill:")]
		[SerializeField] private PlayerSight playerSight;

		private int currentFrameDelay = 0;


		public void SetPlayer(GameObject player)
		{
			playerSight = player.GetComponentInChildren<PlayerSight>();
			if (playerSight == null)
				playerSight = player.GetComponentInParent<PlayerSight>();
			else
				print("Couldn't find a player sight");
		}

		private void Update()
		{
			if(playerSight != null)
			{
				currentFrameDelay += 1;
				if(currentFrameDelay == frameDelay)
				{
					playerSight.LookAt(transform);
					currentFrameDelay = 0;
				}
			}
		}
	}
}