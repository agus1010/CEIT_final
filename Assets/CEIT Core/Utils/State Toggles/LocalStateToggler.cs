using UnityEngine;


namespace CEIT.Utils
{
	public class LocalStateToggler : BaseStateToggler
	{
		[SerializeField] protected bool initialState = false;
		public override bool Value
		{
			get => initialState;
			protected set => initialState = value;
		}
	}
}