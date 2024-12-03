using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Events
{
	public class PlayerPointerEventsListener : EventsListener
	{
		public CEIT.Player.PlayerPointerEventsChannel eventsChannel;
		protected override object channel => eventsChannel;

		public UnityEvent<GameObject> OnNewTargetDetected;
		public UnityEvent<GameObject> OnNewPhysicsTargetDetected;
		public UnityEvent<GameObject> OnNewUITargetDetected;


		public override void Subscribe()
		{
			eventsChannel.NewTargetDetected.AddListener(onNewTargetDetected);
			eventsChannel.NewPhysicsTargetDetected.AddListener(onNewPhysicsTargetDetected);
			eventsChannel.NewUITargetDetected.AddListener(onNewUITargetDetected);
		}

		public override void Unsubscribe()
		{
			eventsChannel.NewTargetDetected.RemoveListener(onNewTargetDetected);
			eventsChannel.NewPhysicsTargetDetected.RemoveListener(onNewPhysicsTargetDetected);
			eventsChannel.NewUITargetDetected.RemoveListener(onNewUITargetDetected);
		}

		private void onNewTargetDetected(GameObject target)
			=> OnNewTargetDetected?.Invoke(target);
		private void onNewPhysicsTargetDetected(GameObject target)
			=> OnNewPhysicsTargetDetected?.Invoke(target);
		private void onNewUITargetDetected(GameObject target)
			=> OnNewUITargetDetected?.Invoke(target);

	}
}