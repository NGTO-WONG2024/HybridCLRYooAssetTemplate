using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{    
    [RequireComponent(typeof(Image))]
    public class CheckArea : DropAbleObject<Card>
    {
        protected override void OnDrop(Card obj)
        {
            obj.followTarget.Target = transform;
        }
    }
}