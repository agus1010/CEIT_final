using UnityEngine;

using CEIT.Loading;


namespace CEIT.Player
{
	[System.Obsolete(message:"Please use PlayerBodyUtils instead.")]
	public class PlayerTeleporter : MonoBehaviour
	{
		public GameObject player;
		public Stats.PlayerHeightProvider heightProvider;

		private Vector3 halfPlayersHeight => Vector3.up * heightProvider.Height;

		public void TeleportToParameters(ModelLoadingOperationParameters parameters)
			=> Teleport(parameters.spawnPosition + halfPlayersHeight);

		public void Teleport(Vector3 position)
		{
			player.SetActive(false);
			player.transform.position = position + halfPlayersHeight;
			player.SetActive(true);
		}

		public void TeleportUp(float units)
			=> Teleport(Vector3.up * units + halfPlayersHeight);

		public void TeleportForward(float units)
			=> Teleport(Vector3.forward * units + halfPlayersHeight);

		public void TeleportRightward(float units)
			=> Teleport(Vector3.right * units + halfPlayersHeight);

		public void TeleportToZero()
			=> Teleport(halfPlayersHeight);
			//=> Teleport(new Vector3(0f, heightProvider.Height, 0f));

		public void TeleportToZero(Stats.IHeightProvider heightProvider)
			=> Teleport(halfPlayersHeight);
	}
}