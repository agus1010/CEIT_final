using UnityEngine;


namespace CEITUI.Animations
{
    public class LerpToRotation : MonoBehaviour
    {
		public Transform rotationTarget;
		public Transform rotationToMatch;
		[Range(0.00001f, 1f)] public float durationMilliseconds = .25f;

		[Header("Config:")]
		[SerializeField] private bool backAndForth = false;

		public bool running { get; private set; } = false;
		public bool runningForward => running && forward;
		public bool runningBackwards => running && !forward;

		private Quaternion originalRot;
		private bool forward = false;
		private float currentDuration = 0f;



		public void Play()
		{
			if (runningForward) return;
			if (rotationTarget.transform.rotation == rotationToMatch.rotation) return;
			forward = true;
			currentDuration = 0f;
			running = true;
		}

		public void Play(bool forward)
		{
			if (forward)
				Play();
			else
				Rewind();
		}

		public void Rewind()
		{
			if (runningBackwards) return;
			if (rotationTarget.transform.rotation == originalRot) return;
			forward = false;
			currentDuration = 0f;
			running = true;
		}


		private void Start()
		{
			originalRot = rotationTarget.rotation;
		}

		private void Update()
		{
			if(running)
			{
				Quaternion towards = forward? rotationToMatch.rotation : originalRot;
				float t = currentDuration / durationMilliseconds;
				rotationTarget.transform.rotation = Quaternion.Lerp(rotationTarget.transform.rotation, towards, t);
				currentDuration += Time.deltaTime;
				running = currentDuration <= durationMilliseconds;
			}
			else
			{
				if(backAndForth)
				{
					forward = !forward;
					if (forward)
						Rewind();
					else
						Play();
				}
			}
		}
	}
}
