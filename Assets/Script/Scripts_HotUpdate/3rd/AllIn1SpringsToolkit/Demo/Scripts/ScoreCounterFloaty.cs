using TMPro;
using UnityEngine;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class ScoreCounterFloaty : MonoBehaviour
    {
        [SerializeField] private ColorSpringComponent colorSpring;
        [SerializeField] private TransformSpringComponent transformSpring;
        [SerializeField] private float scalePunch;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        private Color baseColor;
        private int accumulatedScore;
        
        public void Initialize(Color textColor)
        {
            baseColor = textColor;
            colorSpring.SetTarget(ColorWithAlpha(baseColor, 0f));
            colorSpring.ReachEquilibrium();
            accumulatedScore = 0;
        }
        
        public void ChangeScore(int score, bool isAddition)
        {
            accumulatedScore += score;
            scoreText.text = isAddition ? $"+{accumulatedScore}" : $"-{accumulatedScore}";
            
            colorSpring.SetCurrentValue(baseColor);
            
            transformSpring.scaleSpring.AddVelocity(Vector3.one * scalePunch);
        }

        private void Update()
        {
            if(accumulatedScore > 0 && scoreText.color.a < 0.1f)
            {
                accumulatedScore = 0;
            }
        }

        private Color ColorWithAlpha(Color color, float alpha)
        {
            return new Color(color.r, color.g, color.b, alpha);
        }
    }
}