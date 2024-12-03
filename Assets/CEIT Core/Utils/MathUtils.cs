namespace CEIT
{
	public static class MathUtils
	{
		public static int CircularNext(int totalElements, int currentIndex, int offset = 1)
			=> (currentIndex + System.Math.Abs(offset)) % totalElements;

		public static int CircularPrevious(int totalElements, int currentIndex, int offset = 1)
			=> (currentIndex + totalElements - System.Math.Abs(offset)) % totalElements;

		public static int CircularOffset(int totalElements, int currentIndex, int offset)
			=> pythonicModulus((currentIndex + offset), totalElements);


		private const int SECS_IN_DAY = 60 * 60 * 24;
		public static float ClampAngleToSingleRotation(float angle)
		{
			var sign = UnityEngine.Mathf.Sign(angle);
			var additionalRots = 360f * ((int)angle / 360);
			var result = angle - sign * additionalRots;
			return result;
		}

		public static int ClampSecondsToSingleDay(int totalSeconds)
			=> totalSeconds - SECS_IN_DAY * (totalSeconds / SECS_IN_DAY);

		public static int AngleToSeconds(float angle)
		{
			float singleRotationAngle = ClampAngleToSingleRotation(angle + 270f);
			int seconds = (int)singleRotationAngle * SECS_IN_DAY / 360;
			return seconds;
		}

		public static float SecondsToAngle(int seconds)
		{
			int singleDaySeconds = ClampSecondsToSingleDay(seconds);
			float angle = singleDaySeconds * 360f / SECS_IN_DAY;
			float offsettedAngle = ClampAngleToSingleRotation(angle + 270f);
			return offsettedAngle;
		}

		public static float ClampAngle(float lfAngle, float lfMin, float lfMax)
		{
			if (lfAngle < -360f) lfAngle += 360f;
			if (lfAngle > 360f) lfAngle -= 360f;
			return UnityEngine.Mathf.Clamp(lfAngle, lfMin, lfMax);
		}


		private static int pythonicModulus(float dividend, float divisor)
			=> (int)dividend - UnityEngine.Mathf.FloorToInt(dividend / divisor) * (int)divisor;
	}
}