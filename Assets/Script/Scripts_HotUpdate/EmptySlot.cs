using UnityEngine;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    [RequireComponent(typeof(Image))]
    public class EmptySlot : MonoBehaviour , IDropArea
    {
        public void HandleDrop(Card card)
        {
            Debug.Log("EmptySlot");
            if (card.parent) card.parent.child = null;
            card.parent = null;
            card.followTarget.Target = transform.gameObject.transform.GetChild(0);
        }
    }
}