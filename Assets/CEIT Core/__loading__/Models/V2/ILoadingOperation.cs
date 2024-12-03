namespace CEIT.Loading.Models
{
	public interface ILoadingOperation
	{
		public float progress { get; }
		public void Abort();
		public void Begin();
	}
}