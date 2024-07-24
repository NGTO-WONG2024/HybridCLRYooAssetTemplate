using System.Collections.Generic;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YooAsset;

namespace Script.Scripts_HotUpdate
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Card : DropAbleObject<Card>, IPointerClickHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
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

        public Rank rank;
        public Suit suit;
        public MMFollowTarget followTarget;
        public MMF_Player destroyFeelBack;
        public MMF_Player flipFellBack;
        public Image rankImage;
        public Image frontBgImage;
        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public async void SetUp(Rank pRank, Suit pSuit, Transform initFollowTarget = null)
        {
            rank = pRank;
            suit = pSuit;
            followTarget = GetComponent<MMFollowTarget>();
            if (initFollowTarget != null)
            {
                followTarget.Target = initFollowTarget;
            }

            var package = YooAssets.GetPackage("DefaultPackage");
            var handle = package.LoadSubAssetsAsync<Sprite>("Assets/GameRes/Art/8BitDeck_opt2");
            await handle.Task;
            var sp = handle.GetSubAssetObject<Sprite>("8BitDeck_opt2_" + (13 * (int)suit + (int)pRank));
            rankImage.sprite = sp;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            flipFellBack.PlayFeedbacks();
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            followTarget.enabled = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.blocksRaycasts = true;
            followTarget.enabled = true;
        }

        protected override void OnDrop(Card obj)
        {
            obj.followTarget.Target = transform;
        }
    }
}