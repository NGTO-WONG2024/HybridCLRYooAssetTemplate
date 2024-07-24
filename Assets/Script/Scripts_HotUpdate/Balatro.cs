using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

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
        public List<Card> CreatDeck()
        {
            List<Card> deck = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                var temp = Instantiate(cardPrefab, deckArea);
                temp.SetUp((Card.Rank)(i % 13), (Card.Suit)(i / 13), deckArea);
                deck.Add(temp);
            }

            return deck;
        }
        
        /// <summary>
        /// 补满桌面
        /// </summary>
        /// <returns></returns>
        public async void RollCards()
        {
            for (int i = 0; i < tableCardLimit; i++)
            {
                if (tableArea.childCount == 0) break;
                var card = tableArea.GetChild(0).transform;
                card.SetParent(outArea);
                card.localPosition = Vector3.zero;
                await Task.Delay(200);
            }
            
            for (int i = 0; i < tableCardLimit; i++)
            {
                if (tableArea.childCount == tableCardLimit) break;
                if (deckArea.childCount == 0) break;
                var card = deckArea.GetChild(0).transform;
                card.SetParent(tableArea);
                card.localPosition= Vector3.zero;
                await Task.Delay(200);
            }
        }

        private void Start()
        {
            CreatDeck();
        }

        #endregion
    }
}