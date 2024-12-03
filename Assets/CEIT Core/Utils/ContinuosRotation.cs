using UnityEngine;


namespace CEIT.Utils
{
    public class ContinuosRotation : MonoBehaviour
    {
        public float Speed = 10f;
        public Vector3 Axis = Vector3.up;

        void Update()
        {
            transform.Rotate(Axis * Speed * Time.deltaTime);
        }
    }

}