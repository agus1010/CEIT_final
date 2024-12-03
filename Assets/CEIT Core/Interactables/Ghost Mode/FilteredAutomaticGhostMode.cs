using UnityEngine;


namespace CEIT.Interactables
{
	public class FilteredAutomaticGhostMode : AutomaticGhostMode
	{
		public System.Collections.Generic.List<string> tagsWhiteList;

		[Header("Debug:")]
		[SerializeField] private bool debug = false;


		protected override void OnTriggerEnter(Collider other)
		{
			if(tagsWhiteList.Contains(other.gameObject.tag))
			{
				base.OnTriggerEnter(other);
			}
		}

		protected override void OnTriggerExit(Collider other)
		{
			if(tagsWhiteList.Contains(other.gameObject.tag))
			{
				base.OnTriggerExit(other);
			}
		}
	}
}