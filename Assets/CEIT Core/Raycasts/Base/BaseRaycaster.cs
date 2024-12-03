using UnityEngine;


namespace CEIT.Raycasts
{
	public abstract class BaseRaycaster : MonoBehaviour, IShooter
	{
		public abstract IShotResult Shoot(ShotFilter shotFilter = ShotFilter.SOLIDS);
	}
}