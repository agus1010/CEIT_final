using UnityEngine;

using CEIT.Player;


namespace CEIT.Persistence
{
    public abstract class InteractionBehaviour : ScriptableObject
    {
        public virtual void Initialize(Interaction interaction, PlayerPointer pointer)
		{
            return;
		}
        public virtual void Stop()
		{
            return;
		}
        public abstract void TryPerformPrimary(bool newPrimaryValue);
        public abstract void TryPerformSecondary(bool newSecondaryValue);
        public virtual void TryPerformChangeInteractionValue(int direction)
		{
            return;
		}
        public virtual void OnUpdate()
		{
            return;
		}
        public virtual void OnFixedUpdate()
		{
            return;
		}
    }
}