using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AllIn1SpringsToolkit.Demo.Scripts;
using GameRes.SO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    public class Balatro : SingletonMonoBehaviour<Balatro>
    {
        #region Variables

        [Header("Variables")] 
        public int randomSeed = 1;
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

        [Header("UnityBehaviour")] 
        public StudentCard studentCardPrefab;
        public SenseiCard senseiCardPrefab;
        public LevelCard levelCardPrefab;

        public ScoreCounter scoreCounter;
        
        public Transform cardViewsParent => rectTransforms["cardViewsParent"];
        public Transform bgViewsParent => rectTransforms["bgViewsParent"];
        public Transform deckArea => rectTransforms["deckArea"];
        public Transform tableArea => rectTransforms["tableArea"];
        public Transform outArea => rectTransforms["outArea"];
        public Transform handArea => rectTransforms["handArea"];
        public Transform senseiArea => rectTransforms["senseiArea"];
        public RectTransform Bottom => rectTransforms["Bottom"];
        public Transform shopSenseiArea => rectTransforms["shopSenseiArea"];
        public Transform shopPanel => rectTransforms["shopPanel"];
        public Transform levelScroll => rectTransforms["levelScroll"];
        
        public StudentCard[] HandCards => handArea.GetComponentsInChildren<StudentCard>();
        public StudentCard[] DeckCards => deckArea.GetComponentsInChildren<StudentCard>();
        public StudentCard[] TableCads => tableArea.GetComponentsInChildren<StudentCard>();
        public SenseiCard[] SenseiCards => senseiArea.GetComponentsInChildren<SenseiCard>();
        public int Coin { get; set; } = 100;

        public bool inGame;

        #endregion

        #region Methods

        private Dictionary<string, RectTransform> rectTransforms;

        private async void Start()
        {
            Random.InitState(randomSeed);
            
            rectTransforms = GetComponentsInChildren<RectTransform>().ToDictionary(x => x.name, x => x);
            
            var levelConfigs= await ResManager.Instance.LoadAll<LevelConfig>("Assets/GameRes/SO/Level1.asset");
            foreach (var levelConfig in levelConfigs)
            {
                var tempLevel = Instantiate(levelCardPrefab, levelScroll.GetComponent<ScrollRect>().content.transform);
                tempLevel.SetUp(levelConfig);
            }
        }

        public async void StartGame()
        {
            inGame = true;
            SetBottomActive(true);
            await CreatStudentDeck();
            await Task.Delay((int)(1000/Time.timeScale));
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
                var temp = Instantiate(studentCardPrefab, deckArea);
                temp.SetUp(studentData, deckArea);
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
                card.GetComponent<CardBase>().PlaySound("cardFanSound");
                await Task.Delay((int)(200 / Time.timeScale));
            }

            while (true)
            {
                if (tableArea.childCount == tableCardLimit) break;
                if (deckArea.childCount == 0) break;
                var random = Random.Range(0, deckArea.childCount);
                var card = deckArea.GetChild(random).transform;
                card.SetParent(tableArea);
                card.localPosition = Vector3.zero;
                card.GetComponent<CardBase>().PlaySound("cardFanSound");
                await Task.Delay((int)(200 / Time.timeScale));
            }
        }


        public async void ShowShop()
        {
            handArea.Translate(new Vector3(0,Screen.height,0));
            tableArea.Translate(new Vector3(0,Screen.height,0));
            await Task.Delay((int)(400 / Time.timeScale));
            shopPanel.localPosition = new Vector3(0, 0, 0);
            await Task.Delay((int)(400 / Time.timeScale));
            shopPanel.localScale = Vector3.one;
            await Task.Delay((int)(400 / Time.timeScale));
            RollSenseiCards();
        }
        
        public async void HideShop()
        {
            handArea.Translate(new Vector3(0,-Screen.height,0));
            tableArea.Translate(new Vector3(0,-Screen.height,0));
            shopPanel.localScale = Vector3.one/2f;
            await Task.Delay((int)(400 / Time.timeScale));
            shopPanel.localPosition = new Vector3(Screen.width, 0, 0);
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
                if (shopSenseiArea.childCount == shopSenseiLimit) break;
                var random = Random.Range(0, senseis.Count);
                var senseiCard = Instantiate(senseiCardPrefab, shopSenseiArea);
                senseiCard.SetUp(senseis[random], shopSenseiArea);
                senseiCard.transform.SetParent(shopSenseiArea);
                senseiCard.transform.localPosition = Vector3.zero;
                senseiCard.PlaySound("cardFanSound");
                await Task.Delay((int)(200 / Time.timeScale));
            }
        }


        /// <summary>
        /// 出牌
        /// </summary>
        public async void PlayCard()
        {
            tableArea.Translate(new Vector3(0, 1000, 0));
            await Task.Delay((int)(1000 / Time.timeScale));
            handArea.Translate(new Vector3(0, 500, 0));
            await Task.Delay((int)(1000 / Time.timeScale));
            foreach (var card in HandCards)
            {
                var data = card.studentData;
                foreach (var senseiCard in SenseiCards)
                {
                    data = await senseiCard.Buff_BeforeAttack(data);
                    scoreCounter.ChangeScore(data.attack,card.transform.position+new Vector3(0,150,0));
                }
                card.PlaySound("multhit1Sound");
                scoreCounter.ChangeScore(data.attack,card.transform.position+new Vector3(0,150,0));
                await card.PlayFeelAsync("count");
            }
            await Task.Delay((int)(1000 / Time.timeScale));
            handArea.Translate(new Vector3(0, -500, 0));
            await Task.Delay((int)(1000 / Time.timeScale));
            tableArea.Translate(new Vector3(0, -1000, 0));
        }

        public void TimeScale(float v)
        {
            Time.timeScale = v;
        }

        #endregion
    }
}