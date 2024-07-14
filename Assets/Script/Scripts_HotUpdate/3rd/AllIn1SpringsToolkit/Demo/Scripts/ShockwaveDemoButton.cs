using TMPro;
using UnityEngine;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class ShockwaveDemoButton : MonoBehaviour
    {
        #region On Click
        [Header("Main Properties")]
        [SerializeField] private TextMeshProUGUI numberText;
        [SerializeField] private ShockwaveDemoController shockwaveDemoController;
        
        public void OnClicked()
        {
            shockwaveDemoController.ButtonClicked(int.Parse(numberText.text));
        }
        #endregion
    }
}