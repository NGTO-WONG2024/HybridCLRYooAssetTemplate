using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using YooAsset;

namespace Script.Scripts_HotUpdate
{
    [RequireComponent(typeof(CanvasGroup))]
    public class Card : MonoBehaviour
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
        public Transform view;
        
        
        private MMFollowTarget followTarget;
        private Dictionary<string, MMF_Player> mmfPlayers;
        private Dictionary<string, Image> images;

        public async void SetUp(Rank pRank, Suit pSuit, Transform parent = null)
        {
            mmfPlayers = GetComponentsInChildren<MMF_Player>().ToDictionary(x => x.name, x => x);
            images = GetComponentsInChildren<Image>().ToDictionary(x => x.name, x => x);
            rank = pRank;
            suit = pSuit;
            if (parent != null)
            {
                transform.SetParent(parent);
                transform.localPosition = Vector3.zero;
            }
            var package = YooAssets.GetPackage("DefaultPackage");
            var handle = package.LoadSubAssetsAsync<Sprite>("Assets/GameRes/Art/8BitDeck_opt2");
            await handle.Task;
            var sp = handle.GetSubAssetObject<Sprite>("8BitDeck_opt2_" + (13 * (int)suit + (int)pRank));
            images["rank"].sprite = sp;
            view.SetParent(Balatro.Instance.cardViewsParent);
        }

        public void OnClick()
        {
            if (transform.parent == Balatro.Instance.handArea)
            {
                //if (Balatro.Instance.tableArea.childCount >= Balatro.Instance.tableCardLimit) return;
                transform.SetParent(Balatro.Instance.tableArea);
                transform.localPosition = Vector3.zero;
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.root.GetComponent<RectTransform>());
                return;
            }

            if (transform.parent == Balatro.Instance.tableArea)
            {
                if (Balatro.Instance.handArea.childCount >= Balatro.Instance.handCardLimit) return;
                transform.SetParent(Balatro.Instance.handArea);
                transform.localPosition = Vector3.zero;
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.root.GetComponent<RectTransform>());
                return;
            }
        }


        public async Task PlayFeelAsync(string feelName)
        {
            mmfPlayers[feelName].PlayFeedbacks();
            await Task.Delay((int)(mmfPlayers[feelName].TotalDuration * 1000));
        }
    }
}