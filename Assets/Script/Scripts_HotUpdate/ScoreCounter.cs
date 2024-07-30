using AllIn1SpringsToolkit;
using TMPro;
using UnityEngine;

namespace Script.Scripts_HotUpdate
{
    public class ScoreCounter : MonoBehaviour
    {
        [Space, Header("Score Springs")]
        [SerializeField] private TransformSpringComponent transformSpring;
        [SerializeField] private AnchoredPositionSpringComponent anchoredPositionSpring;
        [SerializeField] private ColorSpringComponent scoreColorSpring;
        [SerializeField] private FloatSpringComponent floatScoreTextSpring;

        [Space, Header("Configuration")]
        [SerializeField] private int minPointsChange;
        [SerializeField] private int maxPointsChange;
        [SerializeField] private int startScore;
        [SerializeField] private float minScoreRatio;
        [SerializeField] private Color addColor;
        [SerializeField] private Color subtractColor;
        [SerializeField] private TextMeshProUGUI scoreText;
        
        [Space, Header("Nudges")]
        [SerializeField] private Vector3 subtractScalePunchVector;
        [SerializeField] private Vector3 addScalePunchVector;
        [SerializeField] private float scalePunchMagnitude;
        [SerializeField] private float rotationPunchOnAdd;
        [SerializeField] private float anchorPunchMagnitude;

        [Space, Header("Score Floating Text")]
        [SerializeField] private Transform addFollowTarget;
        [SerializeField] private ScoreCounterFloaty scoreCounterFloatyAdd;
        [SerializeField] private ScoreCounterFloaty scoreCounterFloatySubtract;
        
        private int currentScore;

        private void Start()
        {
            currentScore = startScore;
            floatScoreTextSpring.SetTarget(currentScore);
            floatScoreTextSpring.ReachEquilibrium();
            scoreText.text = Mathf.RoundToInt(currentScore).ToString();
            scoreCounterFloatyAdd.Initialize(addColor);
            scoreCounterFloatySubtract.Initialize(subtractColor);
        }


        private void Update()
        {
            scoreText.text = Mathf.RoundToInt(floatScoreTextSpring.GetCurrentValue()).ToString();
        }
        
        private void FloatyFeedback(int score)
        {
            bool isAddition = score > 0;
            ScoreCounterFloaty scoreCounterFloaty = isAddition ? scoreCounterFloatyAdd : scoreCounterFloatySubtract;
            scoreCounterFloaty.ChangeScore(Mathf.Abs(score), isAddition);
        }

        [ContextMenu("Normalize Vectors")]
        private void NormalizeVectors()
        {
            subtractScalePunchVector = subtractScalePunchVector.normalized;
            addScalePunchVector = addScalePunchVector.normalized;
        }
        
        public void ChangeScore(int addScore,Vector3 addScorePos)
        {
            addFollowTarget.position = addScorePos;
            bool isAddition = addScore > 0;
            if (!isAddition)
            {
                addScore *= -1;
            }
            FloatyFeedback(addScore);
            currentScore += addScore;
            currentScore = Mathf.Max(0, currentScore);
            
            //This is the core of the effect, here we set the springs to animate the score change
            
            //Set target score that we'll use to animate the score in the text
            floatScoreTextSpring.SetTarget(currentScore);
            //Set Color, then the spring will automatically animate to white (it's the Target)
            scoreColorSpring.SetCurrentValue(isAddition ? addColor : subtractColor);
            
            //Get ratio, the bigger the change the bigger the punch
            float scoreChangeRatio = (float) addScore / maxPointsChange;
            scoreChangeRatio = Mathf.Max(minScoreRatio, scoreChangeRatio);
            
            //Scale punch, we have different vectors for addition and substraction
            Vector3 punchVector = isAddition ? addScalePunchVector * scalePunchMagnitude : subtractScalePunchVector * scalePunchMagnitude;
            transformSpring.AddVelocityToScaleSpring(punchVector * scoreChangeRatio);

            if(isAddition)
            {
                //On addition we just slightly push the score text up
                anchoredPositionSpring.AddVelocity(Vector2.up * anchorPunchMagnitude);
            }
            else
            {
                //On substraction we push the score text down and shake the rotation randomly
                float randomSign = UnityEngine.Random.value > 0.5f ? -1 : 1;
                transformSpring.AddVelocityToRotationSpring(randomSign * Vector3.forward * rotationPunchOnAdd);
                
                anchoredPositionSpring.AddVelocity(Vector2.down * anchorPunchMagnitude);
            }
        }
    }
}