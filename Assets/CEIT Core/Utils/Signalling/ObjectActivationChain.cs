using UnityEngine;


namespace CEIT.Utils
{
	public class ObjectActivationChain : MonoBehaviour
	{
		public int currentActive = 0;
		public GameObject[] objectChain;


		public void MoveNext()
		{
			moveOffset(MathUtils.CircularNext);
		}

		public void MovePrevious()
		{
			moveOffset(MathUtils.CircularPrevious);
		}

		public void SetCurrent(int index)
		{
			objectChain[currentActive].SetActive(false);
			currentActive = index;
			objectChain[currentActive].SetActive(true);
		}



		private void Awake()
		{
			SetCurrent(currentActive);
		}


		private void moveOffset(System.Func<int, int, int, int> newIndexFunc)
		{
			SetCurrent(newIndexFunc(objectChain.Length, currentActive, 1));
		}


		private void moveOffset(int direction)
		{
			objectChain[currentActive].SetActive(false);
			currentActive = MathUtils.CircularOffset(objectChain.Length, currentActive, direction);
			objectChain[currentActive].SetActive(true);
		}
	}
}