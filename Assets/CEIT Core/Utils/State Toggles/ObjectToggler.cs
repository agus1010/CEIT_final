namespace CEIT.Utils
{
	public class ObjectToggler : BaseStateToggler
	{
		public override bool Value
		{
			get => gameObject.activeInHierarchy;
			protected set => gameObject.SetActive(value);
		}
	}
}