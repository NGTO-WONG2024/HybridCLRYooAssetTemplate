using TMPro;
using UnityEngine;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class ShockwaveDemoController : DemoElement
    {
        [Space, Header("Main properties")]
        [SerializeField] private Transform gridTransform;
        [SerializeField] private TransformSpringComponent[] buttonSpringComponents;
        [SerializeField] private ColorSpringComponent[] buttonColorSprings;
        
        [Space, Header("Spring Nudges")]
        [SerializeField] private float scalePunchMax;
        [SerializeField] private float positionPunchMax;
        [SerializeField] private float maxShockwaveDistance;
        [SerializeField] private Color buttonPressColor;
        
        #if UNITY_EDITOR
        [ContextMenu("Set Grid Texts")]
        public void SetGridTexts()
        {
            buttonSpringComponents = new TransformSpringComponent[gridTransform.childCount];
            buttonColorSprings = new ColorSpringComponent[gridTransform.childCount];
            for (int i = 0; i < gridTransform.childCount; i++)
            {
                Transform child = gridTransform.GetChild(i);
                TextMeshProUGUI text = child.GetComponentInChildren<TextMeshProUGUI>();
                if (text != null)
                {
                    text.text = i.ToString();
                }
                
                buttonSpringComponents[i] = child.GetComponent<TransformSpringComponent>();
                buttonColorSprings[i] = child.GetComponent<ColorSpringComponent>();
            }
            UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(UnityEngine.SceneManagement.SceneManager.GetActiveScene());
        }
        #endif

        public void RandomButtonPress()
        {
            int randomIndex = Random.Range(0, buttonSpringComponents.Length);
            ButtonClicked(randomIndex);
        }

        public void ButtonClicked(int index)
        {
            if(!isOpen)
            {
                return;
            }
            
            buttonSpringComponents[index].AddVelocityToScaleSpring(Vector3.one * scalePunchMax);
            Vector3 targetButtonPosition = buttonSpringComponents[index].transform.localPosition;
            
            for (int i = 0; i < buttonSpringComponents.Length; i++)
            {
                if(i == index)
                {
                    buttonColorSprings[i].SetCurrentValue(buttonPressColor);
                    continue;
                }
                
                Vector3 otherButtonPosition = buttonSpringComponents[i].transform.localPosition;
                Vector3 direction = otherButtonPosition - targetButtonPosition;
                float distance = direction.magnitude;
                if (distance < maxShockwaveDistance)
                {
                    float shockwavePower = 1 - distance / maxShockwaveDistance;
                    buttonSpringComponents[i].AddVelocityToPositionSpring(direction.normalized * positionPunchMax * shockwavePower);
                    buttonSpringComponents[i].AddVelocityToScaleSpring(Vector3.one * scalePunchMax * shockwavePower);
                    buttonColorSprings[i].SetCurrentValue(Color.Lerp(Color.white, buttonPressColor, shockwavePower));
                }
            }
        }
    }
}