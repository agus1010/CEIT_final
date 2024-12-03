using System.Collections.Generic;


namespace CEIT.Extensions
{
	public static class IndexingExtensions
	{
		private static int circularNextPosition(int collectionCount, int currentIndex, int offset)
			=> MathUtils.CircularNext(collectionCount, currentIndex, offset);

		public static int CircularNextPosition (this IReadOnlyCollection<object> collection, int currentIndex, int offset = 1)
			=> circularNextPosition(collection.Count, currentIndex, offset);

		public static int CircularNextPosition<T>(this IReadOnlyCollection<T> genericCollection, int currentIndex, int offset = 1)
			=> circularNextPosition(genericCollection.Count, currentIndex, offset);

		public static int CircularNextPosition(this ICollection<object> collection, int currentIndex, int offset = 1)
			=> circularNextPosition(collection.Count, currentIndex, offset);

		public static int CircularNextPosition<T>(this ICollection<T> genericCollection, int currentIndex, int offset = 1)
			=> circularNextPosition(genericCollection.Count, currentIndex, offset);


		private static int circularPreviousPosition(int collectionCount, int currentIndex, int offset)
			=> MathUtils.CircularPrevious(collectionCount, currentIndex, offset);

		public static int CircularPreviousPosition(this IReadOnlyCollection<object> collection, int currentIndex, int offset = 1)
			=> circularPreviousPosition(collection.Count, currentIndex, offset);

		public static int CircularPreviousPosition<T>(this IReadOnlyCollection<T> genericCollection, int currentIndex, int offset = 1)
			=> circularPreviousPosition(genericCollection.Count, currentIndex, offset);

		public static int CircularPreviousPosition(this ICollection<object> collection, int currentIndex, int offset = 1)
			=> circularPreviousPosition(collection.Count, currentIndex, offset);

		public static int CircularPreviousPosition<T>(this ICollection<T> genericCollection, int currentIndex, int offset = 1)
			=> circularPreviousPosition(genericCollection.Count, currentIndex, offset);


		private static int circularOffsetPosition(int collectionCount, int currentIndex, int offset)
			=> MathUtils.CircularOffset(collectionCount, currentIndex, offset);

		public static int CircularOffsetPosition(this IReadOnlyCollection<object> collection, int currentIndex, int offset = 1)
			=> circularOffsetPosition(collection.Count, currentIndex, offset);

		public static int CircularOffsetPosition<T>(this IReadOnlyCollection<T> genericCollection, int currentIndex, int offset = 1)
			=> circularOffsetPosition(genericCollection.Count, currentIndex, offset);

		public static int CircularOffsetPosition(this ICollection<object> collection, int currentIndex, int offset = 1)
			=> circularOffsetPosition(collection.Count, currentIndex, offset);

		public static int CircularOffsetPosition<T>(this ICollection<T> genericCollection, int currentIndex, int offset = 1)
			=> circularOffsetPosition(genericCollection.Count, currentIndex, offset);
	}
}