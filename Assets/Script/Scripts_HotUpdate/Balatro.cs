using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Script.Scripts_HotUpdate
{
    public class Balatro : SingletonMonoBehaviour<Balatro>
    {
        #region Variables

        [Header("Variables")] 
        public int randomSeed = 1;
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
        public StudentCard studentCardPrefab;
        public Transform cardViewsParent => rectTransforms["cardViewsParent"];
        public Transform deckArea => rectTransforms["deckArea"];
        public Transform tableArea => rectTransforms["tableArea"];
        public Transform outArea => rectTransforms["outArea"];
        public Transform handArea => rectTransforms["handArea"];
        public Transform senseiArea => rectTransforms["senseiArea"];
        public StudentCard[] studentCards => handArea.GetComponentsInChildren<StudentCard>();
        public SenseiCard[] senseiCards => senseiArea.GetComponentsInChildren<SenseiCard>();

        #endregion

        #region Methods

        private Dictionary<string, RectTransform> rectTransforms;

        private async void Start()
        {
            Random.InitState(randomSeed);
            rectTransforms = GetComponentsInChildren<RectTransform>().ToDictionary(x => x.name, x => x);
            await CreatDeck();
        }


        /// <summary>
        /// 创建卡组
        /// </summary>
        /// <returns></returns>
        public async Task CreatDeck()
        {
            string jsonString = (await ResManager.Instance.Load<TextAsset>("Assets/GameRes/SO/Student.json")).text;
            Dictionary<string, StudentData> characters =
                JsonConvert.DeserializeObject<Dictionary<string, StudentData>>(jsonString);
            var t = characters;
            List<StudentCard> deck = new List<StudentCard>();
            foreach (var kv in t)
            {
                var studentData = kv.Value;
                var temp = Instantiate(studentCardPrefab, deckArea);
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
            foreach (var card in studentCards)
            {
                foreach (var senseiCard in senseiCards)
                {
                    var t = senseiCard.Buff_BeforeAttack(card.studentData);
                    Debug.Log("buffed" + t.attack);
                }

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