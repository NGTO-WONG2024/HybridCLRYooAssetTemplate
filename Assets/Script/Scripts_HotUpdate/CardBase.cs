using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CardBase : MonoBehaviour
    {
        protected Dictionary<string, MMF_Player> mmfPlayers;
        protected Dictionary<string, Image> images; 
        //protected Dictionary<string, Button> buttons;
        protected Dictionary<string, TextMeshProUGUI> texts;
        private MMFollowTarget followTarget;
        public Transform view;

        protected void Awake()
        {
            mmfPlayers = GetComponentsInChildren<MMF_Player>().ToDictionary(x => x.name, x => x);
            images = GetComponentsInChildren<Image>().ToDictionary(x => x.name, x => x);
            //buttons = GetComponentsInChildren<Button>().ToDictionary(x => x.name, x => x);
            texts = GetComponentsInChildren<TextMeshProUGUI>().ToDictionary(x => x.name, x => x);
            view = transform.GetChild(0);
            view.SetParent(Balatro.Instance.cardViewsParent);
            GetComponent<Button>().onClick.AddListener(OnClick);
            
        }

        protected virtual void OnClick()
        {
            
        }
        
        public async Task PlayFeelAsync(string feelName)
        {
            mmfPlayers[feelName].PlayFeedbacks();
            await Task.Delay((int)(mmfPlayers[feelName].TotalDuration * 1000));
        }
    }
}