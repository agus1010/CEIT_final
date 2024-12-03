using UnityEngine;

using CEIT.Interactables.Surfaces;
using CEIT.Persistence;


namespace CEIT.Interactables
{
	[RequireComponent(typeof(MeshFilter))]
	[RequireComponent(typeof(MeshRenderer))]
	public class SurfaceHistory : MonoBehaviour
	{
		public Surface Current => _archive.Latest;
		public bool Locked { get => locked; set => locked = value; }


		[SerializeField] private bool locked = false;

		private SurfaceHistoryArchive _archive;
		private SurfaceHistoryRenderer _renderer;


		public virtual void Paint(Surface surface)
		{
			if (Locked)
				return;
			_initialize();
			if (surface != _archive.Latest)
			{
				_renderer.Render(surface);
				_archive.AddNew(surface);
			}
		}

		public virtual Surface Undo()
		{
			if (Locked)
				return Current;
			Surface s = _archive.MoveToPrevious();
			_renderer.Render(s);
			return s;
		}

		public virtual void ResetArchive(Surface newOriginal)
		{
			_archive = new SurfaceHistoryArchive(newOriginal);
			_renderer.Render(newOriginal);
		}

		private void OnEnable()
		{
			_initialize();
		}


		private void _initialize()
		{
			var mr = GetComponent<MeshRenderer>();
			if(_renderer == null)
				_renderer = new SurfaceHistoryRenderer(mr);
			if(_archive == null)
				_archive = new SurfaceHistoryArchive(_renderer.Current);
		}
	}
}