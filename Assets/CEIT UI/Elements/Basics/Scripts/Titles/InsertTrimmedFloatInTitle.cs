using UnityEngine;


namespace CEITUI.Elements.Titles
{
	public class InsertTrimmedFloatInTitle : InsertValueInTitle
	{
		public int decimals = 2;

		private float hundreeds => 10f * decimals;
		public override void Insert(float value)
		{
			var trimmed = Mathf.Round(value * hundreeds) / hundreeds;
			string decimalNumbers = "";
			for (int i = 0; i < decimals; i++)
				decimalNumbers += "0";
			base.Insert(trimmed.ToString("0."+decimalNumbers));
		}
	}
}