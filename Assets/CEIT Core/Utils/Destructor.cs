using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CEIT.Utils
{
    public class Destructor : MonoBehaviour
    {
        public GameObject parent;
        public void CallDestroy()
		{
            var target = parent == null ? gameObject : parent;
            Destroy(target);
		}
    }
}