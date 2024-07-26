using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using YooAsset;

namespace Script.Scripts_HotUpdate
{
    public class Balatro : SingletonMonoBehaviour<Balatro>
    {
        #region Variables

        [Header("Variables")] public int randomSeed = 0;
        public int handCardLimit = 5;
        public int tableCardLimit = 5;
        public int round = 0;
        public int targetScore = 0;
        public int currentScore = 0;
        public int playCardCount = 0;
        public int rollCardCount = 0;

        #endregion

        #region UnityBehaviour

        [Header("UnityBehaviour")] 
        public Card cardPrefab;

        public Transform cardViewsParent;
        public Transform deckArea;
        public Transform tableArea; 
        public Transform outArea; 
        public Transform handArea;

        #endregion

        #region Methods

        /// <summary>
        /// 创建卡组
        /// </summary>
        /// <returns></returns>
        public async Task CreatDeck()
        {
            var package = YooAssets.GetPackage("DefaultPackage");
            // 注意：location只需要填写资源包里的任意资源地址。
            var handle = package.LoadAllAssetsAsync<StudentData>("Assets/GameRes/SO/266px-Airi.asset");
            await handle.Task;
            var t = handle.AllAssetObjects;

            List<Card> deck = new List<Card>();
            foreach (var o in t)
            {
                var studentData = (StudentData)o;
                var temp = Instantiate(cardPrefab, deckArea);
                temp.SetUp(studentData, deckArea);
                deck.Add(temp);
            }
        }
        
        /// <summary>
        /// 补满桌面
        /// </summary>
        /// <returns></returns>
        public async void RollCards()
        {
            while (true)
            {
                if (tableArea.childCount == 0) break;
                var card = tableArea.GetChild(tableArea.childCount - 1).transform;
                card.SetParent(outArea);
                card.localPosition = Vector3.zero;
                await Task.Delay(200);
            }

            while (true)
            {
                if (tableArea.childCount == tableCardLimit) break;
                if (deckArea.childCount == 0) break;
                var card = deckArea.GetChild(deckArea.childCount - 1).transform;
                card.SetParent(tableArea);
                card.localPosition= Vector3.zero;
                await Task.Delay(200);
            }
        }

        private void Start()
        {
            CreatDeck();
        }


        public async void PlayCard()
        {
            var cards = handArea.GetComponentsInChildren<Card>();
            tableArea.Translate(new Vector3(0,1000,0));
            await Task.Delay(1000);
            handArea.Translate(new Vector3(0,500,0));
            await Task.Delay(1000);
            foreach (var card in cards)
            {
                await card.PlayFeelAsync("count");
            }
            await Task.Delay(1000);
            handArea.Translate(new Vector3(0,-500,0));
            await Task.Delay(1000);
            tableArea.Translate(new Vector3(0,-1000,0));

        }

        #endregion
    }
}