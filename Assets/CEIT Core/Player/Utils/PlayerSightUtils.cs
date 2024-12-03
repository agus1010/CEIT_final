using UnityEngine;


namespace CEIT.Player.Utils
{
	public class PlayerSightUtils : MonoBehaviour
	{
		public string playerTag = "Player";

		[SerializeField] private Transform _fixedTarget;
		public Transform fixedTarget { get => _fixedTarget; set => _fixedTarget = value; }

		private Transform targetTransform => fixedTarget != null ? fixedTarget : transform;


		public void ForcePlayerLookAtTarget(GameObject playerGameObject)
			=> ForcePlayerLookAtTarget(playerGameObject, targetTransform);

		public void ForcePlayerLookAtTarget(PlayerSight playerSight)
			=> ForcePlayerLookAtTarget(playerSight, targetTransform);

		public void ForcePlayerLookAtTarget(GameObject playerGameObject, Transform target)
		{
			try
			{
				var playerSight = extractPlayerSight(playerGameObject);
				ForcePlayerLookAtTarget(playerSight, target);
			}
			catch (System.ArgumentException ae)
			{
				Debug.LogError(ae);
			}
		}

		public void ForcePlayerLookAtTarget(PlayerSight playerSight, Transform target)
			=> playerSight.LockSightTowards(target);


		private PlayerSight extractPlayerSight(GameObject playerGameObject)
		{
			bool validTag = playerGameObject.CompareTag(playerTag);
			var playerSight = playerGameObject.GetComponentInChildren<PlayerSight>();
			bool hasPlayerSight = playerSight != null;

			if (validTag && hasPlayerSight)
				return playerSight;

			string errorMsg = "";
			if (!validTag)
				errorMsg += $"Not a valid player (GameObject doesn't have the tag {playerTag}). ";
			if (!hasPlayerSight)
				errorMsg += "Not a valid player (GameObject doesn't have a PlayerSight within its hierarchy).";

			throw new System.ArgumentException(errorMsg);
		}
	}
}