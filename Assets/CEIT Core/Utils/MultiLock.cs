using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Utils
{
	public class MultiLock : MonoBehaviour
	{
		[SerializeField] private int lockCount = 0;
		public int LockCount { get => lockCount; private set => lockCount = value; }

		public UnityEvent<bool> OnLockStatusChanged;
		public UnityEvent OnLockStatusChangedToON;
		public UnityEvent OnLockStatusChangedToOFF;


		private readonly object m_lock = new object();


		public void AddLock()
		{
			lock (m_lock)
			{
				bool triggerEvents = LockCount == 0;
				LockCount += 1;
				if (triggerEvents)
				{
					OnLockStatusChanged?.Invoke(true);
					OnLockStatusChangedToON?.Invoke();
				}
			}
		}

		public void SubstractLock()
		{
			lock (m_lock)
			{
				if (LockCount > 0)
				{
					bool triggerEvents = LockCount == 1;
					LockCount -= 1;
					if (triggerEvents)
					{
						OnLockStatusChanged?.Invoke(false);
						OnLockStatusChangedToOFF?.Invoke();
					}
				}
			}
		}

		public void ResetLocks()
		{
			bool triggerEvents = LockCount != 0;
			ResetLocksWithoutNotify();
			if(triggerEvents)
			{
				OnLockStatusChanged?.Invoke(false);
				OnLockStatusChangedToOFF?.Invoke();
			}
		}

		public void ResetLocksWithoutNotify()
		{
			LockCount = 0;
		}


		private void Start()
		{
			LockCount = 0;
		}
	}
}