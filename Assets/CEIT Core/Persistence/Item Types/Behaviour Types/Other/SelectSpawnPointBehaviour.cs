using UnityEngine;

using CEIT.Player;


namespace CEIT.Persistence
{
	[CreateAssetMenu(fileName = "New Select Spawn Point Behaviour", menuName = "CEIT/Persistence/Behaviours/Select Spawn Point")]
	public class SelectSpawnPointBehaviour : InteractionBehaviour
	{
		public string[] maskedTags;
		
		[Header("Debug:")]
		[SerializeField] private bool debug = false;
		
		public bool holdingSpawnPoint { get; private set; } = false;
		public PlayerPointer pointer { get; private set; }
		public Vector3 shotPosition { get; private set; } = Vector3.zero;
		public bool validTarget { get; private set; } = false;

		private Raycasts.ShotFilter prevShotFilter;


		public override void Initialize(Interaction interaction, PlayerPointer pointer)
		{
			this.pointer = pointer;
			prevShotFilter = this.pointer.ShotMode;
			validTarget = false;
			holdingSpawnPoint = false;
			shotPosition = Vector3.zero;
			this.pointer.ShotMode = Raycasts.ShotFilter.TRIGGERS;

			m_prevHoldingSpawnPoint = !holdingSpawnPoint;
		}

		public override void Stop()
		{
			pointer.ShotMode = prevShotFilter;
			pointer = null;
		}

		public override void TryPerformPrimary(bool newPrimaryValue)
		{
			holdingSpawnPoint = newPrimaryValue;
		}

		public override void TryPerformSecondary(bool newSecondaryValue) { }


		private bool m_prevHoldingSpawnPoint;
		public override void OnUpdate()
		{
			if(holdingSpawnPoint)
			{
				var physicsShot = pointer.CurrentPhysicsShot;
				
				validTarget = physicsShot.Hit && !tagInMaskedTags(physicsShot.Target.tag);
				if(validTarget)
				{
					shotPosition = physicsShot.Point;
					log($"New shot position pointing at: {shotPosition}");
				}
				else
				{
					if (m_prevHoldingSpawnPoint != holdingSpawnPoint)
						log("Not pointing a valid target.");
				}
			}
			else
			{
				if(m_prevHoldingSpawnPoint != holdingSpawnPoint)
					log("Waiting for input");
			}
			m_prevHoldingSpawnPoint = holdingSpawnPoint;
		}


		private void log(string msg)
		{
			if(debug)
				Debug.Log(msg);
		}


		private bool tagInMaskedTags(string tag)
		{
			foreach (var maskedTag in maskedTags)
				if (tag == maskedTag)
					return true;
			return false;
		}
	}
}