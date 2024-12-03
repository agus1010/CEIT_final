using UnityEngine;


namespace CEIT.Player.Utils
{
    public class PlayerBodyUtils : MonoBehaviour
    {
		public GameObject playerParent;
        public GameObject playerBody;
        public PlayerSight playerSight;
		public Stats.PlayerStatsProvider statsProvider;

		private Vector3 halfPlayersHeight => Vector3.up * (statsProvider.Height * 0.5f);

		private Quaternion m_playerSightOriginalRotation = Quaternion.identity;
		

		public void ResetPoseAndPosition()
		{
			TeleportToZero();
			playerBody.transform.rotation = Quaternion.identity;
			playerSight.transform.rotation = playerSight != null? m_playerSightOriginalRotation : Quaternion.identity;
		}

		public void Teleport(Vector3 position)
		{
			Vector3 playerTargetPos = position + Vector3.up * statsProvider.Height;
			playerParent.gameObject.SetActive(false);
			playerBody.gameObject.SetActive(false);
			playerParent.transform.position = playerTargetPos;
			playerBody.transform.position = playerTargetPos; //halfPlayersHeight;
			playerBody.gameObject.SetActive(true);
			playerParent.gameObject.SetActive(true);
		}

		public void TeleportUp(float units)
			=> Teleport(Vector3.up * units + halfPlayersHeight);

		public void TeleportForward(float units)
			=> Teleport(Vector3.forward * units + halfPlayersHeight);

		public void TeleportToParameters(Loading.ModelLoadingOperationParameters parameters)
			=> Teleport(parameters.spawnPosition + halfPlayersHeight);

		public void TeleportRightward(float units)
			=> Teleport(Vector3.right * units + halfPlayersHeight);

		public void TeleportToZero()
			=> Teleport(Vector3.zero);

		public void TeleportToZero(Stats.IHeightProvider heightProvider)
			=> Teleport(halfPlayersHeight);

		public void UpdateFPSHeight(float newHeight)
			=> playerSight.transform.localPosition = Vector3.up * (newHeight * 0.5f);

		public void UpdateFPSHeight(Stats.PlayerStatsProvider stats)
			=> UpdateFPSHeight(stats.Height);


		private void Start()
		{
			if(playerSight != null)
				m_playerSightOriginalRotation = playerSight.transform.rotation;
		}
	}
}
