using UnityEngine;


namespace CEIT.Utils
{
	public class ObjectSwitch : MonoBehaviour
	{
		[Header("Config:")]
		[SerializeField] private bool firstIsActive = true;

		[Header("References:")]
		public GameObject first;
		public GameObject second;


		public void Switch()
		{
			second.SetActive(firstIsActive);
			firstIsActive = !firstIsActive;
			first.SetActive(firstIsActive);
		}

		
		private void Awake()
		{
			first.SetActive(firstIsActive);
			second.SetActive(!firstIsActive);
		}
	}
}