using UnityEngine;
using UnityEngine.Events;


namespace CEIT.TimeAndSpace
{
	[CreateAssetMenu(fileName = "New Sun Rotation System Events Channel", menuName = "CEIT/Events/Channels/Core/Sun Rotation System")]
	public class SunRotationSystemEventsChannel : ScriptableObject
	{
		public UnityEvent<Quaternion> RotationChanged;


		public void FireRotationChanged(Quaternion newRotation)
		{
			RotationChanged?.Invoke(newRotation);
		}
	}
}