namespace CEIT.Loading.Models
{
	public enum ModelLoadingOperationStatus
	{
		IDLE = 0,
		STARTED = 1,
		MESHES_LOADED = 2,
		MATERIALS_LOADED = 3,
		COMPLETED = 10,			// 10s and over are for finished status
		ABORTED_MANUALLY = 20,		//20s are for errors
		ABORTED_BY_ERROR = 21
	}
}