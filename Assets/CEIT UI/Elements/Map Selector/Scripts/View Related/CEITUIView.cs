using System.Linq;
using UnityEngine;

using CEIT.Extensions;


namespace CEITUI.Elements.MapSelector
{
	public class CEITUIView : MonoBehaviour
	{
		private GameObject _head;
		public GameObject head
		{
			get
			{
				if (_head == null)
					_head = findDirectChildWithName("HEAD");
				return _head;
			}
		}

		private GameObject _body;
		public GameObject body
		{
			get
			{
				if (_body == null)
					_body = findDirectChildWithName("BODY");
				return _body;
			}
		}


		private GameObject findDirectChildWithName(string name)
			=> transform.GetDirectChilds().Where(t => t.name == name).First().gameObject;
	}
}