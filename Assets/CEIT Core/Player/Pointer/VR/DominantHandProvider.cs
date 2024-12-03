using UnityEngine;
using UnityEngine.Events;


namespace CEIT.Player.VR
{
	public class DominantHandProvider : MonoBehaviour
	{
		[SerializeField] private bool _isRight = true;
		public bool isRight { get => _isRight; set => _isRight = value; }

		public GameObject rightHand;
		public GameObject leftHand;

		public GameObject dominantHand => isRight ? rightHand : leftHand;

		public UnityEvent<GameObject> OnDominantHandChanged;
		public UnityEvent OnDominantHandChangedToRight;
		public UnityEvent OnDominantHandChangedToLeft;
	}
}