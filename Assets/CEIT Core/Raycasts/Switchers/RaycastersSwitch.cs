using UnityEngine;


namespace CEIT.Raycasts
{
	public class RaycastersSwitch : BaseRaycaster
	{
		[Header("Data:")]
		[SerializeField] private int selected = -1;
		[Header("Config:")]
		[SerializeField] private bool firstOneIsDefault = true;
		[Header("Scene References:")]
		public BaseRaycaster[] raycasters;

		public BaseRaycaster active => selected >= 0 && selected < raycasters.Length ? raycasters[selected] : null;


		public virtual void SetSelected(int index)
		{
			selected = index >= 0 && index < raycasters.Length ? index : selected;
			for (int i = 0; i < raycasters.Length; i++)
			{
				activateSelected(i);
			}
		}

		public override IShotResult Shoot(ShotFilter shotFilter = ShotFilter.SOLIDS)
			=> active.Shoot(shotFilter);


		protected virtual void activateSelected(int selectedIndex)
			=> raycasters[selectedIndex].gameObject.SetActive(selectedIndex == selected);


		protected void Awake()
		{
			if (firstOneIsDefault)
				SetSelected(0);
			else
				SetSelected(selected);
		}
	}
}