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

        protected override void OnClick()
        {
            base.OnClick();
            if (transform.parent == Balatro.Instance.handArea)
            {
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

    }
}