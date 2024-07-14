using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class FloatSpringRemapDemo : DemoElement
    {
        [Space, Header("Float Spring and Components")]
        [SerializeField] private FloatSpringComponent floatSpring;
        [SerializeField] private Slider slider;
        [SerializeField] private SliderPressDetector sliderPressDetector;
        [SerializeField] private TextMeshProUGUI sliderValueText;
        [SerializeField] private RectTransform textImageRt, splotchRt;
        [SerializeField] private RectTransform leftRt, rightRt;
        [SerializeField] private Image textImage, splotchImage;
        
        [Space, Header("Remap Values And Configuration")]
        [SerializeField] private float textImageMaxRotation;
        [SerializeField] private float textImageMaxSpeed;
        [SerializeField] private float scoreMaxRotation;
        [SerializeField] private float scoreMaxSpeed;
        [SerializeField] private float minTextImageColorAlpha;
        [SerializeField] private float minTextImageScale;
        [SerializeField] private float minSplotchScale;
        [SerializeField] private float splotchCurveExponent;
        
        private Color textImageColor, splotchImageColor;
        private RectTransform sliderRect;
        private float lastUserSetValue;

        private void Start()
        {
            const float startValue = 0.5f;
            lastUserSetValue = startValue;
            floatSpring.SetTarget(0.5f);
            floatSpring.ReachEquilibrium();
            slider.value = startValue;
            textImageColor = textImage.color;
            splotchImageColor = splotchImage.color;
            sliderRect = slider.GetComponent<RectTransform>();
        }

        private void Update()
        {
            RemappingOfElements();
            UpdateTextSliderValue();
            SetSpringTargetWithMousePosition();
        }

        private void RemappingOfElements()
        {
            float zeroToOneValue = floatSpring.GetCurrentValue();
            float minusOneToOneValue = (zeroToOneValue * 2f) - 1f;
            float absMinusOneToOneValue = Mathf.Abs(minusOneToOneValue);
            float springVelocity = floatSpring.springFloat.GetVelocity();
            
            //Position, scale, rotation and color of text
            textImageRt.position = Vector3.LerpUnclamped(leftRt.position, rightRt.position, zeroToOneValue);
            textImageRt.localScale = Vector3.one * Mathf.LerpUnclamped(1f, minTextImageScale, absMinusOneToOneValue);
            float absSpringVelocity = Mathf.Abs(springVelocity);
            float textImageZRotation = RemapClamped(absSpringVelocity, 0f, textImageMaxSpeed, 0f, textImageMaxRotation);
            textImageRt.rotation = Quaternion.Euler(0f, 0f, textImageZRotation * Mathf.Sign(springVelocity));
            textImageColor.a = Mathf.LerpUnclamped(1f, minTextImageColorAlpha, absMinusOneToOneValue);
            textImage.color = textImageColor;
            
            //Scale and color of splotch
            splotchRt.localScale = Vector3.one * Mathf.Pow(Mathf.LerpUnclamped(1f, minSplotchScale, absMinusOneToOneValue), splotchCurveExponent);
            textImageColor.a = Mathf.LerpUnclamped(1f, minTextImageColorAlpha, absMinusOneToOneValue);
            textImage.color = textImageColor;
            splotchImageColor.a = Mathf.LerpUnclamped(1f, 0f, Mathf.Pow(absMinusOneToOneValue, 1f / splotchCurveExponent));
            splotchImage.color = splotchImageColor;

            //Tilt the slider value text
            float scoreZRotation = RemapClamped(absSpringVelocity, 0f, scoreMaxSpeed, 0f, scoreMaxRotation);
            sliderValueText.transform.rotation = Quaternion.Euler(0f, 0f, scoreZRotation * Mathf.Sign(springVelocity));
        }

        private void UpdateTextSliderValue()
        {
            string sliderValue = slider.value.ToString("F1");
            sliderValueText.text = sliderValue.Replace(",", ".");
        }

        private void SetSpringTargetWithMousePosition()
        {
            if (sliderPressDetector.IsSliderPressed())
            {
                // Get the mouse position in local coordinates of the slider
                Vector2 localMousePosition = GetLocalMousePosition();

                // Calculate the slider value based on mouse position
                lastUserSetValue = CalculateSliderValueFromMousePosition(localMousePosition);

                // Set the spring target to the calculated value
                floatSpring.SetTarget(lastUserSetValue);
            }
            else
            {
                // When not pressed, use the closest snap value
                float closestSliderValue = Mathf.Round(lastUserSetValue * 2f) / 2f;
                floatSpring.SetTarget(closestSliderValue);
            }

            // Update the slider's visual position based on the spring
            slider.SetValueWithoutNotify(floatSpring.GetCurrentValue());
        }
        
        private Vector2 GetLocalMousePosition()
        {
            Vector2 mousePosition = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(sliderRect, mousePosition, null, out Vector2 localPoint);
            return localPoint;
        }

        private float CalculateSliderValueFromMousePosition(Vector2 localPosition)
        {
            float sliderWidth = sliderRect.rect.width;
            float normalizedPosition = (localPosition.x + sliderWidth / 2) / sliderWidth;
            return Mathf.Clamp01(normalizedPosition);
        }
        
        private float RemapClamped(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            float normalizedValue = Mathf.Clamp01((value - fromMin) / (fromMax - fromMin));
            return Mathf.Lerp(toMin, toMax, normalizedValue);
        }
        
        private float RemapUnclamped(float value, float fromMin, float fromMax, float toMin, float toMax)
        {
            float normalizedValue = (value - fromMin) / (fromMax - fromMin);
            return Mathf.Lerp(toMin, toMax, normalizedValue);
        }
    }
}