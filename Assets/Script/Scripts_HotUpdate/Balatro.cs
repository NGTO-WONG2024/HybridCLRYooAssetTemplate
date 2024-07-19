using System.Collections.Generic;
using UnityEngine;

namespace Script.Scripts_HotUpdate
{
    public class Balatro : SingletonMonoBehaviour<Balatro>
    {
        #region Variables
        [Header("Variables")]
        public int randomSeed = 0;
        public int handCardLimit = 5;
        public List<Card> handCards = new List<Card>();
        public int round = 0;
        public int targetScore = 0;
        public int currentScore = 0;
        public int playCardCount = 0;
        public int rollCardCount = 0;
        
        #endregion

        #region UnityBehaviour
        [Header("UnityBehaviour")]
        [SerializeField] private Card cardPrefab;
        [SerializeField] private Transform deckPos;
        [SerializeField] private Transform cards;
        #endregion

        #region Methods

        public List<Card> CreatDeck()
        {
            List<Card> deck = new List<Card>();
            for (int i = 0; i < 52; i++)
            {
                var temp= Instantiate(cardPrefab,cards);
                temp.SetUp((Card.Rank)(i % 13), (Card.Suit)(i / 13), null,deckPos);
                deck.Add(temp);
            }
            return deck;
        }

        private void Start()
        {
            CreatDeck();
        }


        // public async void DrawCards()
        // {
        //     for (int i = handCards.Count; i < handCardLimit; i++)
        //     {
        //         Card card = Instantiate(cardPrefab,this.transform);
        //         card.SetUp((Card.Rank)i,(Card.Suit)Random.Range(0, 4), null,null);
        //         await Task.Delay(100);
        //     }
        // }
        //
        // public async void PlayCards()
        // {
        //     var selectingCards = handCards.Where(card => card).ToList();
        //     if (selectingCards.Count == 0) return;
        //     foreach (var card in selectingCards)
        //     {
        //         //card.followTarget.SetParent(checkScoreArea.transform);
        //         await Task.Delay(100);
        //     }
        //
        //     var score = 0;
        //     foreach (var card in selectingCards)
        //     {
        //         score += (int)card.rank;
        //         await Task.Delay(100);
        //     }
        //     currentScore = score;
        //     handCards = handCards.Except(selectingCards).ToList();
        //     await Task.Delay(100);
        //     for (int i = selectingCards.Count - 1; i >= 0; i--)
        //     {
        //         await Task.Delay(100);
        //         selectingCards[i].DestroyMe();
        //     }
        // }
        //
        //
        //

        #endregion
        
        
        
        
    }
}