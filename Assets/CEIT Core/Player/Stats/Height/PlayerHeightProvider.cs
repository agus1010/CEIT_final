using UnityEngine;


namespace CEIT.Player.Stats
{
	public abstract class PlayerHeightProvider : MonoBehaviour, IHeightProvider
	{
		public abstract float Height { get; }
	}
}