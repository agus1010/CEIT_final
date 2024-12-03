using System.Collections.Generic;

using CEIT.Persistence;


namespace CEIT.Interactables.Surfaces
{
	public class SurfaceHistoryArchive
	{
		private Stack<Surface> _archive;

		public int Count => _archive.Count;
		public Surface Latest => _archive.Peek();


		public SurfaceHistoryArchive(Surface originalSurface)
		{
			_archive = new Stack<Surface>();
			_archive.Push(originalSurface);
		}


		public void AddNew(Surface surface)
		{
			_archive.Push(surface);
		}

		public Surface MoveToPrevious()
		{
			if (Count > 1)
				_archive.Pop();
			return Latest;
		}
	}
}