using UnityEngine;

using CEIT.Raycasts;


namespace CEIT.Player
{
	public class UIRaycastersManager : MonoBehaviour
	{
		[SerializeField] private UIRaycaster raycaster;
		
		public IUIShotResult CurrentFrameResult { get; private set; }


		private void Start()
		{
			CurrentFrameResult = new UIShotResult();
		}

		private void Update()
		{
			CurrentFrameResult = raycaster.Shoot() as IUIShotResult;
		}
	}
}