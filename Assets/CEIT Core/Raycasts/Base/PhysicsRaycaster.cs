using UnityEngine;


namespace CEIT.Raycasts
{
	public enum ShotFilter { SOLIDS = 0, TRIGGERS = 1, ALL = 2 }

	public abstract class PhysicsRaycaster : BaseRaycastShooter
	{
		[Header("Debug:")]
		[SerializeField] protected bool debug = false;
		protected abstract float maxRayDistance { get; }


		private bool m_hit;
		private Ray m_ray;
		private RaycastHit m_rHit;
		public override IShotResult Shoot(ShotFilter shotFilter = ShotFilter.SOLIDS)
		{
			m_ray = makeRay();
			m_hit = tryGetRaycastHit(m_ray, out m_rHit, shotFilter);
			var shotResult = 
				m_hit ? 
					new PhysicsShotResult(m_ray, maxRayDistance, m_rHit) : 
					new PhysicsShotResult(m_ray, maxRayDistance);
			if (debug)
			{
				print(shotResult.Target);
				Debug.DrawLine(
					m_ray.GetPoint(0),
					m_ray.GetPoint(shotResult.Distance),
					Color.blue);
			}
			return shotResult;
		}


		protected abstract Ray makeRay();
		protected abstract bool tryGetRaycastHit(Ray ray, out RaycastHit rHit, ShotFilter shotFilter);

		protected QueryTriggerInteraction triggersFilter(ShotFilter shotFilter)
		{
			QueryTriggerInteraction result;
			switch (shotFilter)
			{
				case ShotFilter.SOLIDS:
					result = QueryTriggerInteraction.Ignore;
					break;
				case ShotFilter.TRIGGERS:
					result = QueryTriggerInteraction.Collide;
					break;
				default:
					result = QueryTriggerInteraction.UseGlobal;
					break;
			}
			return result;
		}
	}
}