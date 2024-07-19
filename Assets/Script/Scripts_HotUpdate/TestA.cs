using UnityEngine;
using UnityEngine.EventSystems;

namespace Script.Scripts_HotUpdate
{
    public class TestA : MonoBehaviour , IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log("abs");
        }
    }
}