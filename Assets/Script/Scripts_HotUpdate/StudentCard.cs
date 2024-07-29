using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    public class StudentCard : CardBase
    {
        public StudentData studentData;
        
        public async void SetUp(StudentData data, Transform parent = null)
        {
            studentData = data;
            if (parent != null)
            {
                transform.SetParent(parent);
                transform.localPosition = Vector3.zero;
            }
            images["headIcon"].sprite = await ResManager.Instance.Load<Sprite>(data.headIconPath);
            images["school"].sprite = await ResManager.Instance.Load<Sprite>(data.schoolIconPath);
            texts["attack"].text = data.attack.ToString();
            texts["name"].text = data.en;
        }

        public override async void OnClick()
        {
            base.OnClick();
            if (transform.parent == Game.Instance.handArea)
            {
                transform.SetParent(Game.Instance.tableArea);
                transform.localPosition = Vector3.zero;
                Debug.Log("A");
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.root.GetComponent<RectTransform>());
                return;
            }

            if (transform.parent == Game.Instance.tableArea)
            {
                if (Game.Instance.handArea.childCount >= Game.Instance.handCardLimit) return;
                transform.SetParent(Game.Instance.handArea);
                transform.localPosition = Vector3.zero;
                LayoutRebuilder.ForceRebuildLayoutImmediate(transform.root.GetComponent<RectTransform>());
                await Task.Delay((int)(1000 / Time.timeScale));
                var handCards = Game.Instance.HandCards;
                foreach (var card in handCards)
                {
                    if (card.studentData.school == this.studentData.school)
                    {
                        _ = card.PlayFeelAsync("schoolFeel");
                    }
                }
                _ =  PlayFeelAsync("schoolFeel");
                return;
            }
        }

    }
}