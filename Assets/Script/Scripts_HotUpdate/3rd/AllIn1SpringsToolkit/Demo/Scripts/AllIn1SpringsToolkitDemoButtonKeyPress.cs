using AllIn1SpringsToolkit.Utils;
using TMPro;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
using UnityEngine.InputSystem;
#endif

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class AllIn1SpringsToolkitDemoButtonKeyPress : MonoBehaviour
    {
        [SerializeField] private KeyCode targetKey = KeyCode.A;
        [SerializeField] private AllIn1SpringsToolkitSimpleButton simpleButton;

        [Space, Header("Label Settings")]
        [SerializeField] private bool completelyIgnoreLabel;
        [SerializeField] private bool showKeyLabel = true;
        [SerializeField] private TextMeshProUGUI keyLabel; 
        
        private void Start()
        {
            if(!completelyIgnoreLabel)
            {
                if(!showKeyLabel)
                {
                    keyLabel.enabled = false;
                    enabled = false;
                }
                else
                {
                    keyLabel.text = $"(Key {targetKey.ToString()})";
                }   
            }
        }

        private void Update()
        {
            if(IsTargetKeyPressed())
            {
                simpleButton.SimulateClick();
            }
        }

        private bool IsTargetKeyPressed()
        {
#if ENABLE_INPUT_SYSTEM && !ENABLE_LEGACY_INPUT_MANAGER
            if(Keyboard.current != null && Keyboard.current[KeyCode].wasPressedThisFrame) return true;
#else
            return Input.GetKeyDown(targetKey);
#endif
        }
    }
}