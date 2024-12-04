using TMPro;
using UnityEngine;


namespace CEITUI.Elements
{
	public class PickAFileTitle : MonoBehaviour
	{
		public FileListPopulator populator;
		public TextMeshProUGUI indications;
		public TextMeshProUGUI path;
		private string[] supportedExtensions => populator.SupportedExtensions;


		private void Start()
		{
			string extensionsJoined = populator.SupportedExtensions[0];
			if (populator.SupportedExtensions.Length > 1)
			{
				extensionsJoined = string.Join(", ", supportedExtensions[supportedExtensions.Length - 2]);
				extensionsJoined += $" y {supportedExtensions[supportedExtensions.Length - 1]}";
			}

			indications.text = $"Mostrando los archivos con extensión {extensionsJoined} en la carpeta:";
			path.text = populator.MapsFolder;
		}
	}
}