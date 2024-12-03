using UnityEngine;


namespace CEIT.Interactables
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(PropPartGhostMode))]
	public class PropPartBehaviour : MonoBehaviour
	{
		[Header("Overrides:")]
		public bool overridePropValues = false;
		public bool collidesWhenPlacedOverride = false;
		public bool surfaceHistoryShouldBeLocked = false;

		public PropBehaviour propBehaviour { get; private set; }
		public Bounds meshBounds => meshFilter ? meshFilter.sharedMesh.bounds : new Bounds();

		private MeshFilter meshFilter;
		private PropPartGhostMode ghostMode;
		private Collider[] colliders;
		private LayerMask originalLayer;

		private bool collidesWhenPlaced => overridePropValues ? collidesWhenPlacedOverride : propBehaviour.collidesWhenPlaced;


		public void Accomodate()
		{
			ghostMode.canActivate = true;
			setLayer("Ignore Raycast");
			trySetCollidersTriggerMode(true);
		}

		public void Place()
		{
			ghostMode.Deactivate();
			ghostMode.canActivate = false;
			setLayer(originalLayer);
			trySetCollidersTriggerMode(!collidesWhenPlaced);
		}


		private void Awake()
		{
			meshFilter = GetComponent<MeshFilter>();
			ghostMode = GetComponent<PropPartGhostMode>();
			propBehaviour = GetComponentInParent<PropBehaviour>();
			colliders = GetComponents<Collider>();
			originalLayer = gameObject.layer;
			trySetCollidersTriggerMode(!collidesWhenPlaced);
		}

		
		private void setLayer(string layer)
			=> setLayer(LayerMask.NameToLayer(layer));

		private void setLayer(int layer)
			=> gameObject.layer = layer;

		private void trySetCollidersTriggerMode(bool activate)
		{
			foreach (var collider in colliders)
			{
				collider.isTrigger = activate;
			}
		}

	}
}