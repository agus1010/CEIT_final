using UnityEngine;


namespace CEIT.Interactables
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(SurfaceHistory))]
	public class GhostMode : MonoBehaviour
	{
		public Persistence.Surface effect;

		protected MeshFilter meshFilter { get; set; }
		protected SurfaceHistory surfaceHistory { get; set; }

		protected bool isActivated { get; private set; } = false;


		public virtual void Activate()
		{
			if(!isActivated)
			{
				surfaceHistory.Paint(effect);
				surfaceHistory.Locked = true;
				isActivated = true;
			}
		}

		public virtual void Deactivate()
		{
			if(isActivated)
			{
				surfaceHistory.Undo();
				surfaceHistory.Locked = false;
				isActivated = false;
			}
		}


		protected virtual void Awake()
		{
			meshFilter = GetComponent<MeshFilter>();
			surfaceHistory = GetComponent<SurfaceHistory>();
		}

	}
}