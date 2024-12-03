namespace CEIT.Extensions
{
	public static class NumberExtensions
	{
		public static bool Between(this int obj, int min, int max)
			=> min < obj && obj < max;

		public static bool Between(this float obj, float min, float max)
			=> min < obj && obj < max;

		public static bool BetweenOrEqual(this int obj, int min, int max)
			=> min <= obj && obj <= max;

		public static bool BetweenOrEqual(this float obj, float min, float max)
			=> min <= obj && obj <= max;
	}
}