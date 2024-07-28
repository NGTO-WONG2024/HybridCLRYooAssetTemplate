using System.Threading.Tasks;
using DG.Tweening;
using GameRes.SO;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    public class LevelCard : CardBase
    {
        public RectTransform maskRectTransform;
        private LevelConfig levelConfig;
        private bool isOn = false;
        public async void SetUp(LevelConfig config)
        {
            levelConfig = config;
            images["bg"].sprite = await ResManager.Instance.Load<Sprite>(config.bgPath);
            texts["name"].text = config.levelName;
            view.SetParent(Balatro.Instance.bgViewsParent);
        }
        
        public override async void OnClick()
        {
            base.OnClick();
            ScrollRect scrollRect = GetComponentInParent<ScrollRect>();  // Assign your ScrollRect in the inspector=
            var sizeX = view.GetComponent<RectTransform>().sizeDelta.x + GetComponentInParent<HorizontalLayoutGroup>().spacing;
            float x = Screen.width / 2f - sizeX / 2f  -  transform.GetSiblingIndex() * sizeX +GetComponentInParent<HorizontalLayoutGroup>().spacing/2f;
            var pos = new Vector2(x, 0);


            if (Mathf.Approximately(scrollRect.content.GetComponent<RectTransform>().anchoredPosition.x, pos.x))
            {
                if (isOn == false)
                {
                    view.SetSiblingIndex(999);
                    maskRectTransform.DOSizeDelta(new Vector2(Screen.width + 1f, Screen.height + 1f), 0.3f)
                        .SetEase(Ease.InOutBack);
                    images["bg"].GetComponent<RectTransform>().DOSizeDelta(new Vector2(2512f, 1766f), 0.3f)
                        .SetEase(Ease.InOutBack);
                    isOn = true;
                }
                else
                {
                    if (Balatro.Instance.inGame)
                    {                    return;
                    }
                    Balatro.Instance.StartGame();

                    // maskRectTransform.DOSizeDelta(new Vector2(500,800),0.3f).SetEase(Ease.InOutBack);
                    // images["bg"].GetComponent<RectTransform>().DOSizeDelta(new Vector2(2512f / 2f, 1766f / 2f), 0.3f)
                    //     .SetEase(Ease.InOutBack);
                    //     images["bg"].GetComponent<RectTransform>().DOAnchorPosY(-0f,0.3f).SetEase(Ease.InOutBack);;
                    //
                    //     // () =>
                    //     // {
                    //     // };
                    // isOn = false;
                }
            }
            else
            {
                scrollRect.content.GetComponent<RectTransform>().anchoredPosition = pos;
            }
            
            
            
            
        }
    }
}