using UnityEngine;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    [RequireComponent(typeof(Image))]
    public class EmptySlot : DropAbleObject<Card>
    {
        protected override void OnDrop(Card card)
        {
            Debug.Log("EmptySlot");
            card.followTarget.Target = transform.gameObject.transform.GetChild(0);
            Debug.Log("物体已放置在Drop Zone中! 物体名字: " + card.name);
        }
    }
}