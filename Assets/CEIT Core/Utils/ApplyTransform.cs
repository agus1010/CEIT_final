using UnityEngine;

namespace CEIT.Utils
{
    public class ApplyTransform : MonoBehaviour
    {
        public void Apply(Transform source)
            => Apply(transform, source);

        public void Apply(Transform target, Transform source)
        {
            target.position = source.position;
            target.rotation = source.rotation;
            target.localScale = source.localScale;   
        }
    }
}
