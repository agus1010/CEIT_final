using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
	public class SimpleSignalEmitter : MonoBehaviour
	{
		public UnityEvent signal;

		[Header("Debug:")]
		[SerializeField] private bool debug = false;



		private string m_msg = "";
		public void OnTrue(bool value)
		{
			if(debug)
				m_msg = $"Value recibed = {value}.";
			if (value)
			{
				signal?.Invoke();
				if (debug)
					m_msg += "Signal emmited.";
			}
			if (debug)
				print(m_msg);
		}

		public void OnFalse(bool value)
		{
			if(!value)
				signal?.Invoke();
		}
	}
}