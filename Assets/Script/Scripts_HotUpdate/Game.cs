using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameRes.SO;
using MoreMountains.Tools;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    public class Game : SingletonMonoBehaviour<Game>
    {
        #region Variables

        [Header("Variables")] public int randomSeed = 1;
        public int handCardLimit = 5;
        public int tableCardLimit = 5;
        public int shopSenseiLimit = 5;
        public int round = 0;
        public int targetScore = 0;
        public int currentScore = 0;
        public int playCardCount = 0;
        public int rollCardCount = 0;

        #endregion

        #region UnityBehaviour

        [Header("UnityBehaviour")] public Tutorial tutorial;
        public StudentCard studentCardPrefab;
        public SenseiCard senseiCardPrefab;
        public LevelCard levelCardPrefab;

        public ScoreCounter scoreCounter;

        public RectTransform CardViewsParent => transform.FindByName<RectTransform>("cardViewsParent");
        public RectTransform BgViewsParent => transform.FindByName<RectTransform>("bgViewsParent");
        public RectTransform DeckArea => transform.FindByName<RectTransform>("deckArea");
        public RectTransform TableArea => transform.FindByName<RectTransform>("tableArea");
        public RectTransform OutArea => transform.FindByName<RectTransform>("outArea");
        public RectTransform HandArea => transform.FindByName<RectTransform>("handArea");
        public RectTransform SenseiArea => transform.FindByName<RectTransform>("senseiArea");
        public RectTransform Bottom => transform.FindByName<RectTransform>("Bottom");
        public RectTransform ShopSenseiArea => transform.FindByName<RectTransform>("shopSenseiArea");
        public RectTransform ShopPanel => transform.FindByName<RectTransform>("shopPanel");
        public RectTransform LevelScroll => transform.FindByName<RectTransform>("levelScroll");

        public StudentCard[] HandCards => HandArea.GetComponentsInChildren<StudentCard>();
        public StudentCard[] DeckCards => DeckArea.GetComponentsInChildren<StudentCard>();
        public StudentCard[] TableCads => TableArea.GetComponentsInChildren<StudentCard>();
        public SenseiCard[] SenseiCards => SenseiArea.GetComponentsInChildren<SenseiCard>();
        public int Coin { get; set; } = 100;

        public bool inGame;

        #endregion

        #region Methods


        private async void Start()
        {
            Random.InitState(randomSeed);
            
            string jsonString = (await ResManager.Instance.Load<TextAsset>("Assets/GameRes/SO/Level.json")).text;
            Dictionary<string, LevelData> levelDatas =
                JsonConvert.DeserializeObject<Dictionary<string, LevelData>>(jsonString);
            
            foreach (var kv in levelDatas)
            {
                var tempLevel = Instantiate(levelCardPrefab, LevelScroll.GetComponent<ScrollRect>().content.transform);
                tempLevel.SetUp(kv.Value);
            }

            tutorial.TutorialPlay(0);
        }

        public async void StartGame()
        {
            inGame = true;
            SetBottomActive(true);
            await CreatStudentDeck();
            await Task.Delay((int)(1000 / Time.timeScale));
            RollCards();
        }

        public void SetBottomActive(bool isOn)
        {
            Bottom.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, isOn ? 150 : -350);
        }

        /// <summary>
        /// 创建卡组
        /// </summary>
        /// <returns></returns>
        public async Task CreatStudentDeck()
        {
            string jsonString = (await ResManager.Instance.Load<TextAsset>("Assets/GameRes/SO/Student.json")).text;
            Dictionary<string, StudentData> studentDatas =
                JsonConvert.DeserializeObject<Dictionary<string, StudentData>>(jsonString);
            foreach (var kv in studentDatas)
            {
                var studentData = kv.Value;
                var temp = Instantiate(studentCardPrefab, DeckArea);
                temp.SetUp(studentData, DeckArea);
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
                if (TableArea.childCount == 0) break;
                var card = TableArea.GetChild(TableArea.childCount - 1).transform;
                card.SetParent(OutArea,Vector3.zero);
                card.GetComponent<CardBase>().PlaySound("cardFanSound");
                await Task.Delay((int)(200 / Time.timeScale));
            }

            while (true)
            {
                if (TableArea.childCount == tableCardLimit) break;
                if (DeckArea.childCount == 0) break;
                var random = Random.Range(0, DeckArea.childCount);
                var card = DeckArea.GetChild(random).transform;
                card.SetParent(TableArea,Vector3.zero);
                card.GetComponent<CardBase>().PlaySound("cardFanSound");
                await Task.Delay((int)(200 / Time.timeScale));
            }
        }


        public async void ShowShop()
        {
            HandArea.Translate(new Vector3(0, Screen.height, 0));
            TableArea.Translate(new Vector3(0, Screen.height, 0));
            await Task.Delay((int)(400 / Time.timeScale));
            ShopPanel.localPosition = new Vector3(0, 0, 0);
            await Task.Delay((int)(400 / Time.timeScale));
            ShopPanel.localScale = Vector3.one;
            await Task.Delay((int)(400 / Time.timeScale));
            RollSenseiCards();
        }

        public async void HideShop()
        {
            HandArea.Translate(new Vector3(0, -Screen.height, 0));
            TableArea.Translate(new Vector3(0, -Screen.height, 0));
            ShopPanel.localScale = Vector3.one / 2f;
            await Task.Delay((int)(400 / Time.timeScale));
            ShopPanel.localPosition = new Vector3(Screen.width, 0, 0);
            await Task.Delay((int)(400 / Time.timeScale));
        }

        public async void RollSenseiCards()
        {
            List<ISensei> senseis = new()
            {
                new Sensei1(),
                new Sensei2(),
            };


            while (true)
            {
                if (ShopSenseiArea.childCount == shopSenseiLimit) break;
                var random = Random.Range(0, senseis.Count);
                var senseiCard = Instantiate(senseiCardPrefab, ShopSenseiArea);
                senseiCard.SetUp(senseis[random], ShopSenseiArea);
                senseiCard.transform.SetParent(ShopSenseiArea,Vector3.zero);
                senseiCard.PlaySound("cardFanSound");
                await Task.Delay((int)(200 / Time.timeScale));
            }
        }


        /// <summary>
        /// 出牌
        /// </summary>
        public async void PlayCard()
        {
            TableArea.Translate(new Vector3(0, 1000, 0));
            await Task.Delay((int)(1000 / Time.timeScale));
            HandArea.Translate(new Vector3(0, 500, 0));
            await Task.Delay((int)(1000 / Time.timeScale));
            foreach (var card in HandCards)
            {
                var data = card.studentData;
                foreach (var senseiCard in SenseiCards)
                {
                    data = await senseiCard.Buff_BeforeAttack(data);
                    scoreCounter.ChangeScore(data.attack, card.transform.position + new Vector3(0, 150, 0));
                }

                card.PlaySound("multhit1Sound");
                scoreCounter.ChangeScore(data.attack, card.transform.position + new Vector3(0, 150, 0));
                await card.PlayFeelAsync("count");
            }

            await Task.Delay((int)(1000 / Time.timeScale));
            HandArea.Translate(new Vector3(0, -500, 0));
            await Task.Delay((int)(1000 / Time.timeScale));
            TableArea.Translate(new Vector3(0, -1000, 0));
        }

        public void TimeScale(float v)
        {
            Time.timeScale = v;
        }

        #endregion
    }
}