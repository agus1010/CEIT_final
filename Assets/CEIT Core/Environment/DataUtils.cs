namespace CEIT.Environment
{
	public enum PROPS_ORIGIN
	{
		APPLICATION, EXTERNAL
	}

	public struct PropBehaviourData
	{
		public string uid;
		public float[] position;
		public float[] eulerRotation;
		public float[] localScale;
		public PropPartBehaviourData[] parts;
	}

	public struct PropPartBehaviourData
	{
		public string currentSurfaceId;
	}


	public enum APPLIED_SURFACE
	{
		MODEL, APP_PROPS, EXTERNAL_PROPS
	}
	
	public struct SurfaceHistoryData
	{
		public string[] pathInScene;
		public string surfaceId;
		public float[] goPosition;
	}

	public struct TimeAndSpaceData
	{
		public float[] sunRotation;
	}
}