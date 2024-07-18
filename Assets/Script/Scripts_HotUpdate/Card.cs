using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    public class Card : MonoBehaviour, IPointerClickHandler, IDragHandler , IEndDragHandler , IBeginDragHandler
    {
        public Card parent = null;
        public Card child = null;
        
        public int rank = 10;
        public int color;
        public MMFollowTarget followTarget;
        public bool dragging = false;
        public MMF_Player destroyFeelBack;

        public Card GetHead()
        {
            return parent == null ? this : parent.GetHead();
        }
        
        public Card GetTail()
        {
            return child == null ? this : child.GetTail();
        }

        public void DestroyMe()
        {
            destroyFeelBack.PlayFeedbacks();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            var t = this.GetComponentInParent<GraphicRaycaster>();
            t.Raycast(eventData, results);

            Card dragStopCard = null;
            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent<Card>(out dragStopCard))
                {
                    if (dragStopCard == this) continue;
                    if (dragStopCard == child) continue;
                    break;
                }
            }

            if (dragStopCard != null)
            {
                if (parent) parent.child = null;
                parent = null;
                var tail = dragStopCard.GetTail();
                tail.child = this;
                parent = tail;
                followTarget.Target = parent == null ? null : parent.transform;
                transform.SetSiblingIndex(parent == null ? 0 : parent.transform.GetSiblingIndex() + 1);
                SortCanvas();
            }
            

            //
            // //;
            //
            // if (dragStopCard != null)
            // {
            //     parent = dragStopCard;
            //     dragStopCard.child = this;
            //     child = null;
            // }
            // else
            // {
            //     parent = null;
            // }
            // followTarget.Target = parent == null ? null : parent.transform;
            followTarget.enabled = true;
        }

        private void SortCanvas()
        {
            var head= GetHead();
            var node = head;
            while (true)
            {
                Debug.Log(node.gameObject);
                if (node.child==null)
                {
                    break;
                }
                else
                {
                    node = node.child;
                }
            }
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            followTarget.enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
        }
    }
}