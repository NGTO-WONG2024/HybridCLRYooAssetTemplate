using UnityEngine;
using UnityEngine.EventSystems;

public class DragAble : MonoBehaviour , IBeginDragHandler , IDragHandler , IEndDragHandler
{
    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
    }
}
