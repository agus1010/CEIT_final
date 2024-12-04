using UnityEngine;


namespace CEITUI.Utils
{
    public class SetWorldRotationToCero : MonoBehaviour
    {
        private void Start()
        {
            transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        }

		/*private void OnDrawGizmosSelected()
		{
            Start();
		}*/
	}
}