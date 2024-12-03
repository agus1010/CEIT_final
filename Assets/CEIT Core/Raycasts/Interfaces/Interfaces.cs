using CEIT.Raycasts;
using UnityEngine;


namespace CEIT
{
	public interface IShooter
	{
		public IShotResult Shoot(ShotFilter shotFilter = ShotFilter.SOLIDS);
	}

	public interface IShotResult
	{
		public float Distance { get; }
		public float MaxDistance { get; }
		public bool Hit { get; }
		public GameObject Target { get; }
	}

	public interface IPhysicsShotResult : IShotResult
	{
		public Ray Ray { get; }
		public Vector3 Point { get; }
		public Vector3 Normal { get; }
		public Collider Collider { get; }
	}

	public interface IUIShotResult : IShotResult
	{

	}
}