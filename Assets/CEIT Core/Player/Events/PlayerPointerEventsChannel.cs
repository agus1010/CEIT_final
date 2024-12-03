using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Player
{
	[CreateAssetMenu(fileName = "New Player Pointer Events Channel", menuName = "CEIT/Events/Channels/Core/Player Pointer")]
    public class PlayerPointerEventsChannel : ScriptableObject
    {
        public UnityEvent<GameObject> NewTargetDetected;
        public UnityEvent<GameObject> NewPhysicsTargetDetected;
        public UnityEvent<GameObject> NewUITargetDetected;


        public void FireNewTargetDetected(GameObject target)
            => NewTargetDetected?.Invoke(target);

        public void FireNewPhysicsTargetDetected(GameObject target)
            => NewPhysicsTargetDetected?.Invoke(target);

        public void FireNewUITargetDetected(GameObject target)
            => NewUITargetDetected?.Invoke(target);
    }
}