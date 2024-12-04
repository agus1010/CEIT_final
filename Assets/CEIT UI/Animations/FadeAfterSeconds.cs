using UnityEngine;
using UnityEngine.UI;


namespace CEITUI.Animations
{
	public class FadeAfterSeconds : MonoBehaviour
	{
		public Graphic targetGraphic;
		public float secondsLeft { get; private set; }
		public float crossFadeSeconds = .5f;

    
		public void Run(float seconds)
		{
			targetGraphic.CrossFadeAlpha(1, 0, false);
			secondsLeft = seconds;
		}


		private void Start()
		{
			Run(2.5f);
		}

		private void Update()
		{
			if(secondsLeft > 0)
			{
				secondsLeft -= Time.deltaTime;
				if(secondsLeft <= 0)
				{
					targetGraphic.CrossFadeAlpha(0, crossFadeSeconds, false);
				}
			}
		}
	}
}