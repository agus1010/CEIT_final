using UnityEngine;


namespace CEIT.Raycasts
{
	public class UIShotResult : IUIShotResult
	{
		public float Distance { get; private set; } = float.MaxValue;
		public float MaxDistance { get; private set; }
		public bool Hit { get; private set; }
		public GameObject Target { get; private set; }


		public UIShotResult() { }

		public UIShotResult(GameObject target, float distance, float maxDistance)
		{
			Hit = true;
			Distance = distance;
			Target = target;
			MaxDistance = maxDistance;
		}

		public override string ToString()
			=> "Targetting: " + (Hit ? $"{Target.name}, at {Distance} metters." : "Nothing.");

	}
}