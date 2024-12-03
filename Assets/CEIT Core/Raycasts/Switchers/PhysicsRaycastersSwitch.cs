using CEIT.Player;


namespace CEIT.Raycasts
{
	public class PhysicsRaycastersSwitch : RaycastersSwitch
	{
		public PlayerPointer pointer;

		protected override void activateSelected(int selectedIndex)
		{
			if (raycasters[selectedIndex].GetType() == typeof(CursorBasedPhysicsRaycaster))
				pointer.ShotMode = ShotFilter.TRIGGERS;
			else
				pointer.ShotMode = ShotFilter.SOLIDS;
			base.activateSelected(selectedIndex);
		}
	}
}