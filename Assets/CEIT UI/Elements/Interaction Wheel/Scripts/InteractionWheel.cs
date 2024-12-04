using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using CEIT.Persistence;


namespace CEITUI.Elements
{
    public class InteractionWheel : MonoBehaviour
    {
        public List<Toggle> toggles;


		private void Start()
		{
			foreach (var toggle in toggles)
			{
				toggle.SetIsOnWithoutNotify(false);
			}

		}
	}
}