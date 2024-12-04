using UnityEngine;
using UnityEngine.UI;

using CEIT.Persistence;
using CEITUI.Utils;


namespace CEITUI.Elements
{
	public class SpawnPointSelectionViewManager : MonoBehaviour
	{
		public bool allowConfirmation { get; set; } = false;

		public Button confirmationButton;
		public SpawnPositionReader spawnPositionReader;
		public SelectSpawnPointBehaviour selectSpawnPointBehaviour;
		public CEIT.Loading.ModelLoadingParametersSetter spawnPosCalculator;


		public void SpawnPointSelected(Vector3 point)
		{
			allowConfirmation = true;
			UpdateConfirmationButtonStatus();
		}

		public void UpdateConfirmationButtonStatus()
		{
			confirmationButton.interactable = allowConfirmation;
		}

		public void Reset()
		{
			allowConfirmation = false;
		}


		private bool m_validTarget;
		private void Update()
		{
			if(selectSpawnPointBehaviour.holdingSpawnPoint)
			{
				if(!selectSpawnPointBehaviour.pointer.IsLookingAtGraphics)
				{
					m_validTarget = selectSpawnPointBehaviour.validTarget;
					confirmationButton.interactable = m_validTarget;
					if (m_validTarget)
						spawnPositionReader.UpdateText(spawnPosCalculator.spawnPosition);
					else
						spawnPositionReader.UpdateText("--");
				}
			}
		}
	}
}