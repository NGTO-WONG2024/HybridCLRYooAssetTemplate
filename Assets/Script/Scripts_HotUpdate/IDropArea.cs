using UnityEngine;
using UnityEngine.UI;

namespace Script.Scripts_HotUpdate
{
    public interface IDropArea
    {
        void HandleDrop(Card card);
    }
}