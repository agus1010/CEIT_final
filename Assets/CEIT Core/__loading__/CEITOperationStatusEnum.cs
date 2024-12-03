namespace CEIT.Loading
{
	public enum CEIT_OPERATION_STATUS
	{
		IDLE = 0,
		STARTED = 1,
		FINISHED = 10,		// 10 and above is for "DONE RUNNING" status;
		CANCELLED = 20,		// 20 and above are for "ABORTED" status
		CRASHED = 21
	}
}