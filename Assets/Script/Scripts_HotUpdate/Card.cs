using System;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    public class Card : MonoBehaviour, IPointerClickHandler, IDragHandler , IEndDragHandler , IBeginDragHandler
    {
        public int rank = 10;
        public int color;
        public bool selecting = false;
        public Transform followTarget;
        public bool dragging = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (dragging) return;
            selecting = !selecting;
        }

        private void Update()
        {
            GetComponent<MMFollowTarget>().Offset = new Vector3(0, selecting ? 150 : 0, 0);
        }

        public void OnDrag(PointerEventData eventData)
        {
            followTarget.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            followTarget.GetComponentInParent<HorizontalLayoutGroup>().enabled = false;
            followTarget.GetComponentInParent<HorizontalLayoutGroup>().enabled = true;
            dragging = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            dragging = true;
        }
    }
}