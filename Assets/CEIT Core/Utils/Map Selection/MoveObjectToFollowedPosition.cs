using UnityEngine;

using CEIT.Persistence;


namespace CEIT.Utils
{
    public class MoveObjectToFollowedPosition : MonoBehaviour
    {
        public SelectSpawnPointBehaviour spawnPointBehaviour;
        public GameObject Arrow;


        private void Update()
        {
            Arrow.transform.position = spawnPointBehaviour.shotPosition;
        }
    }
}