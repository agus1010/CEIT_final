using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;


namespace CEIT.Player
{
	public class PlayerVRBody : DynamicMoveProvider
	{
		[Space, Header("Speed Provider")]
		public HorizontalMovementProvider horizontalMovementProvider;

		[Header("Config:")]
		[SerializeField] private bool locked = false;
		public bool Locked { get => locked; set => locked = value; }

		protected override Vector3 ComputeDesiredMove(Vector2 input)
		{
			if (Locked)
				return Vector3.zero;
			horizontalMovementProvider.CalculateHorizontalMovement(input);
			moveSpeed = horizontalMovementProvider.speed;
			return base.ComputeDesiredMove(input);
		}
	}
}