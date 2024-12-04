using UnityEngine;
using TMPro;

using CEIT.Loading;


namespace CEITUI.Utils
{
	public class SpawnPositionReader : MonoBehaviour
	{
		public TextMeshProUGUI xCoord;
		public TextMeshProUGUI yCoord;
		public TextMeshProUGUI zCoord;

		const string DEC_FORMAT = "0.##";


		public void UpdateText(Vector3 position)
		{
			xCoord.text = $"x: {position.x.ToString(DEC_FORMAT)}";
			yCoord.text = $"y: {position.y.ToString(DEC_FORMAT)}";
			zCoord.text = $"z: {position.z.ToString(DEC_FORMAT)}";
		}

		public void UpdateText(string text)
		{
			xCoord.text = $"x: {text}";
			yCoord.text = $"y: {text}";
			zCoord.text = $"z: {text}";
		}
	}
}