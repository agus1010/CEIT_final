using UnityEngine;


namespace CEIT.Utils
{
    public class ContinuosFacer : MonoBehaviour
    {

        [SerializeField] bool _freezeRotationX = false;
        [SerializeField] bool _freezeRotationY = false;
        [SerializeField] bool _freezeRotationZ = false;

        [SerializeField] bool _flip = false;

        [Header("Debug:")]
        [SerializeField] bool _running = false;
        [SerializeField] private Transform _target;

        public void StartFacing(GameObject target)
        {
            _target = target.transform;
            _running = true;
        }

        public void StopFacing()
        {
            _target = null;
            _running = false;
        }

        void Update()
        {
            if (!_running) return;
            Vector3 frozenAxis = new Vector3
            (
                _target.position.x * (_freezeRotationX ? 0 : 1),
                _target.position.y * (_freezeRotationY ? 0 : 1),
                _target.position.z * (_freezeRotationZ ? 0 : 1)
            );
            transform.LookAt(frozenAxis);
            if (_flip)
                transform.Rotate(Vector3.up, 180f);
        }
    }

}