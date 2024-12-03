using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
	public abstract class BaseStateToggler : MonoBehaviour, IStateToggler
	{
		public abstract bool Value { get; protected set; }

		public UnityEvent<bool> OnValueChanged;
		public UnityEvent OnValueTurnedTrue;
		public UnityEvent OnValueTurnedFalse;


		public void ForceValue(bool value)
		{
			if (Value != value)
				Toggle();
		}
		
		public void Toggle()
		{
			Value = !Value;
			triggerEvents();
		}


		protected virtual void triggerEvents()
		{
			OnValueChanged?.Invoke(Value);
			if (Value)
				OnValueTurnedTrue?.Invoke();
			else
				OnValueTurnedFalse?.Invoke();
		}
	}
}