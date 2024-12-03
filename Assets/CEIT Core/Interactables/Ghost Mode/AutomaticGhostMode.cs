using System.Collections.Generic;
using UnityEngine;


namespace CEIT.Interactables
{
	[RequireComponent(typeof(Collider))]
	public class AutomaticGhostMode : GhostMode
	{
		protected List<Collider> intersectedColliders;
	
		
		protected override void Awake()
		{
			base.Awake();
			intersectedColliders = new List<Collider>();
		}

		protected virtual void OnTriggerEnter(Collider other)
		{
			if(!intersectedColliders.Contains(other))
			{
				intersectedColliders.Add(other);
				Activate();
			}
		}

		protected virtual void OnTriggerExit(Collider other)
		{
			if(intersectedColliders.Contains(other))
			{
				intersectedColliders.Remove(other);
				if (intersectedColliders.Count == 0)
				{
					Deactivate();
				}
			}
		}
	}
}