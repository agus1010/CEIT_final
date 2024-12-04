using UnityEngine;


namespace CEITUI.Elements.Titles
{
	public class InsertIntInTitle : InsertValueInTitle
	{
		public override void Insert(float value)
			=> formatTitleTextWith(Mathf.RoundToInt(value));
	}
}