using UnityEngine;
using UnityEngine.Events;

using CEIT.Persistence;


namespace CEIT.Assets.Events
{
    public class NewItemDetectedEventMultiplexor : MonoBehaviour
    {
        public UnityEvent<string> OnNewNameDetected;
        public UnityEvent<Sprite> OnNewThumbnailDetected;


        public virtual void FireAllEventsWith(Item item)
		{
            OnNewNameDetected?.Invoke(item.ItemName);
            OnNewThumbnailDetected?.Invoke(item.Thumbnail);
		}
    }
}