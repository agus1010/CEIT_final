using UnityEngine;

using CEIT.Persistence;


namespace CEIT.Interactables.Surfaces
{
	public class SurfaceHistoryRenderer
	{
		public Surface Current;
		public MeshRenderer meshRenderer { get; private set; }


		#region Constructors
		public SurfaceHistoryRenderer(MeshRenderer meshRenderer)
		{
			this.meshRenderer = meshRenderer;
			var ogMats = meshRenderer.sharedMaterials;
			_render(ogMats);
			Current = ScriptableObject.CreateInstance<Surface>();
			Current.UID = "Original";
			Current.materials = ogMats;
		}

		public SurfaceHistoryRenderer(Surface current, MeshRenderer meshRenderer)
		{
			this.meshRenderer = meshRenderer;
			Render(current);
		}
		#endregion


		public void Render(Surface surface)
		{
			_render(surface.materials);
			Current = surface;
		}


		private void _render(Material[] materials)
		{
			meshRenderer.sharedMaterials = materials;
		}
	}
}