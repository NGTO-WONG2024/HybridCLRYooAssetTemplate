using System;
using System.Collections.Generic;
using DG.Tweening;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YooAsset;

namespace Script.Scripts_HotUpdate
{
    public class Card : MonoBehaviour, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
    {
        public enum Suit
        {
            Clubs, // 梅花
            Diamonds, // 方块
            Hearts, // 红心
            Spades // 黑桃
        }

        public enum Rank
        {
            Two,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
        }
        public enum BackGround
        {
            Normal,
        }

        public Card parent = null;
        public Card child = null;
        public Rank rank;
        public Suit suit;
        private MMFollowTarget followTarget;
        public MMF_Player destroyFeelBack;
        public MMF_Player flipFellBack;
        public GameObject front;
        public GameObject back;
        public bool isFront = true;
        public Image rankImage;
        public Image frontBgImage;

        public async void SetUp(Rank pRank, Suit pSuit, Card pParent)
        {
            parent = pParent;
            rank = pRank;
            suit = pSuit;
            followTarget = GetComponent<MMFollowTarget>();

            var package = YooAssets.GetPackage("DefaultPackage");
            var handle = package.LoadSubAssetsAsync<Sprite>("Assets/GameRes/Art/8BitDeck_opt2");
            await handle.Task;
            var sp = handle.GetSubAssetObject<Sprite>("8BitDeck_opt2_" + (13 * (int)suit + (int)pRank));
            rankImage.sprite = sp;
        }

        public Card GetHead()
        {
            return parent == null ? this : parent.GetHead();
        }

        public Card GetTail()
        {
            return child == null ? this : child.GetTail();
        }

        public bool AreRelated(Card node1, Card node2)
        {
            if (node1 == null || node2 == null)
                return false;

            if (node1 == node2)
                return true;

            // Search backwards from node1
            Card current = node1;
            while (current.parent != null)
            {
                current = current.parent;
                if (current == node2)
                    return true;
            }

            // Search forwards from node1
            current = node1;
            while (current.child != null)
            {
                current = current.child;
                if (current == node2)
                    return true;
            }

            return false;
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

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.TryGetComponent<Card>(out var card))
                {
                    if (AreRelated(card, this)) continue;
                    if (parent) parent.child = null;
                    parent = null;
                    var tail = card.GetTail();
                    tail.child = this;
                    parent = tail;
                    followTarget.Target = parent == null ? null : parent.transform;
                    SortCanvas();
                    break;
                }

                if (result.gameObject.name.Equals("RollArea"))
                {
                    Debug.Log("Roll");
                    followTarget.Target = result.gameObject.transform.GetChild(0);
                }
                
            }


            followTarget.enabled = true;
        }

        private void SortCanvas()
        {
            var node = this;
            while (true)
            {
                node.transform.SetSiblingIndex(int.MaxValue);
                if (node.child == null) break;
                node = node.child;
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            SortCanvas();
            followTarget.enabled = false;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            flipFellBack.PlayFeedbacks();
        }

    }
}