using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Scripts_HotUpdate
{
    public abstract class DropAbleObject<T> : MonoBehaviour, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            T obj = eventData.pointerDrag.GetComponent<T>();
            if (obj == null) return;
            OnDrop(obj);
        }

        protected abstract void OnDrop(T obj);
    }
}