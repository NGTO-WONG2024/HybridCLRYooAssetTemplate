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
        protected Dictionary<string, AudioSource> audioSources;
        protected Dictionary<string, TextMeshProUGUI> texts;
        private MMFollowTarget followTarget;
        [MMReadOnly]
        public Transform view;

        protected void Awake()
        {
            mmfPlayers = GetComponentsInChildren<MMF_Player>().ToDictionary(x => x.name, x => x);
            images = GetComponentsInChildren<Image>().ToDictionary(x => x.name, x => x);
            texts = GetComponentsInChildren<TextMeshProUGUI>().ToDictionary(x => x.name, x => x);
            audioSources = GetComponentsInChildren<AudioSource>().ToDictionary(x => x.name, x => x);
            view = transform.GetChild(0);
            view.SetParent(Balatro.Instance.cardViewsParent);
            
        }

        public virtual async void OnClick()
        {
            PlaySound("clickSound");
        }
        
        public async Task PlayFeelAsync(string feelName)
        {
            mmfPlayers[feelName].PlayFeedbacks();
            await Task.Delay((int)(mmfPlayers[feelName].TotalDuration * 1000));
        }
        public void PlaySound(string sound)
        {
            audioSources[sound].Play();
        }

        public void DestroyCard()
        {
             Destroy(view.gameObject);
             Destroy(gameObject);
        }
    }
}