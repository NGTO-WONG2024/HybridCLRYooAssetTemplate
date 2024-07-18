using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Script.Scripts_HotUpdate
{
    public class Balatro : SingletonMonoBehaviour<Balatro>
    {
        #region Variables
        public int randomSeed = 0;
        public int handCardLimit = 5;
        public List<Card> handCards = new List<Card>();
        public int round = 0;
        public int targetScore = 0;
        public int currentScore = 0;
        public int playCardCount = 0;
        public int rollCardCount = 0;
        
        #endregion

        #region MonoObjects

        [SerializeField] private Card cardPrefab;
        [SerializeField] private GameObject menuObj;
        [SerializeField] private GameObject handArea;
        [SerializeField] private GameObject checkScoreArea;
        [SerializeField] private GameObject gamePlayObj;
        [SerializeField] public MMFloatingTextMeshPro text;

        #endregion

        #region Methods
        
        public async void StartNewGame()
        {
            Random.InitState(randomSeed != 0 ? randomSeed : DateTime.Now.GetHashCode());
            DrawCards();
        }

        public async void DrawCards()
        {
            for (int i = handCards.Count; i < handCardLimit; i++)
            {
                Card card = Instantiate(cardPrefab,this.transform);
                card.SetUp((Card.Rank)i,(Card.Suit)Random.Range(0, 4), null);
                await Task.Delay(100);
            }
        }

        public async void PlayCards()
        {
            var selectingCards = handCards.Where(card => card).ToList();
            if (selectingCards.Count == 0) return;
            foreach (var card in selectingCards)
            {
                //card.followTarget.SetParent(checkScoreArea.transform);
                await Task.Delay(100);
            }

            var score = 0;
            foreach (var card in selectingCards)
            {
                score += (int)card.rank;
                await Task.Delay(100);
            }
            currentScore = score;
            handCards = handCards.Except(selectingCards).ToList();
            await Task.Delay(100);
            for (int i = selectingCards.Count - 1; i >= 0; i--)
            {
                await Task.Delay(100);
                selectingCards[i].DestroyMe();
            }
        }
        
        


        #endregion
        
        
        
        
    }
}