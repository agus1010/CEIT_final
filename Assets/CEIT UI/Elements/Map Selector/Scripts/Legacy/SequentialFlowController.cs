using UnityEngine;
using UnityEngine.Events;


namespace CEITUI.Flows
{
    public class SequentialFlowController : MonoBehaviour
    {
        public GameObject[] views;
        
		public int index { get; private set; } = 0;

		public UnityEvent<int> OnCurrentStepChanged;
		public UnityEvent OnStepsFinished;


        public void MoveToNextView()
		{
			if(index + 1 < views.Length)
				moveToStep(index, index + 1);
			else
				OnStepsFinished?.Invoke();
		}

        public void MoveToPreviousView()
		{
			if (index - 1 >= 0)
				moveToStep(index, index - 1);
		}

		public void RestartSequence()
		{
			moveToStep(index, 0);
		}


		private void Start()
		{
			if (views.Length > 1)
			{
				views[0].SetActive(true);
				for (int i = 1; i < views.Length; i++)
				{
					views[i].SetActive(false);
				}
			}
		}


		private void moveToStep(int previousStep, int targetStep)
		{
			views[previousStep].SetActive(false);
			views[targetStep].SetActive(true);
			index = targetStep;
			OnCurrentStepChanged?.Invoke(index);
		}
	}
}