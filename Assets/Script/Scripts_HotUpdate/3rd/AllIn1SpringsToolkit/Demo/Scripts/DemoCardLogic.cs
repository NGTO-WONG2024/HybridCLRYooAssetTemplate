using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using TMPro;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class DemoCardLogic : MonoBehaviour
    {
        public RectTransform targetRectTransform;
        public RectTransform cardShadowRectTransform;
        public RectTransform myRectTransform;
        [SerializeField] private RectTransform reactorRotationParentRectTransform, tiltRotationParentRectTransform;
        [SerializeField] private float autoRotationTiltSpeed, autoRotationTiltAmount;
        [SerializeField] private float reactorRotationXAmount, reactorRotationYAmount;
        [SerializeField] private Image iconImage;
        [SerializeField] private Sprite[] possibleSprites;
        [SerializeField] private float shadowLateralPositionInfluence, maxXShadowOffset;
        [SerializeField] private SimpleDraggable simpleDraggable;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Color[] cardColors;
        [SerializeField] private Image[] cardImages;
        [SerializeField] private TextMeshProUGUI[] cardTexts;
        [SerializeField] private GameObject backFaceIconGameObject, selectedIndicatorGameObject;
        [SerializeField] private float colorHighlightFactor;
        
        [Header("Springs")]
        [SerializeField] private TransformSpringComponent transformSpringComponent;
        [SerializeField] private TransformSpringComponent childSpringComponent;
        [SerializeField] private float maxPunchVerticalVelocity;
        [SerializeField] private float posSpringDragWhenDragging, posSpringForceWhenDragging;
        [SerializeField] private float posSpringDragWhenNotDragging, posSpringForceWhenNotDragging, posSpringForceWhenClosing;
        [SerializeField] private float scaleSpringTargetWhenPointed;
        [SerializeField] private float scaleSpringPunchWhenPointed;
        [SerializeField] private float positionSpringPunchWhenPointed;
        [SerializeField] private ColorSpringComponent backgroundColorSpringComponent;
        private Color highlightedCardColor, normalCardColor;
        
        private float initialShadowYOffset, demoCardControllerXPosition;
        private DemoCardController demoCardController;
        private Transform rotationSpringTransform;
        private int cardIndex;
        private Color currentCardColor;
        
        public void Initialize(DemoCardController newDemoCardController, int newCardIndex)
        {
            cardIndex = newCardIndex;
            demoCardController = newDemoCardController;
            demoCardController.OnClose += SetCardAtClosingState;
            demoCardControllerXPosition = demoCardController.transform.position.x;
            initialShadowYOffset = myRectTransform.position.y - cardShadowRectTransform.position.y;
            iconImage.sprite = possibleSprites[UnityEngine.Random.Range(0, possibleSprites.Length)];

            rotationSpringTransform = childSpringComponent.transform;
            normalCardColor = backgroundImage.color;

			CardDressWithRandomColors();
			SetCardAtRestState();

			SetBackfaceOrFrontFace();
			//childSpringComponent.SetValuesRotationSpring(new Vector3(0f, 180f, 0f));
		}

        private void Update()
        {
            UpdateShadowTransform();
            SetBackfaceOrFrontFace();
            RotationReactsToPositionMovement();
            AutoTiltRotation();
            backgroundImage.color = backgroundColorSpringComponent.GetCurrentValue();
        }

        private void UpdateShadowTransform()
        {
            Vector3 targetPosition = myRectTransform.position;
            Vector3 shadowPosition = cardShadowRectTransform.position;
            float shadowXOffset = shadowLateralPositionInfluence * (targetPosition.x - demoCardControllerXPosition);
            shadowXOffset = Mathf.Clamp(shadowXOffset, -maxXShadowOffset, maxXShadowOffset);
            shadowPosition.x = targetPosition.x + shadowXOffset;
            shadowPosition.y = targetPosition.y - initialShadowYOffset;
            cardShadowRectTransform.position = shadowPosition;
            cardShadowRectTransform.rotation = reactorRotationParentRectTransform.rotation;
            cardShadowRectTransform.localScale = rotationSpringTransform.localScale;
        }
        
        private void SetBackfaceOrFrontFace()
        {
            Vector3 cardForward = rotationSpringTransform.forward;
            Vector3 upVector = Vector3.forward;
            float dotProduct = Vector3.Dot(cardForward, upVector);
            bool isFrontFace = dotProduct >= 0;
            backFaceIconGameObject.SetActive(!isFrontFace);
        }
        
        private void RotationReactsToPositionMovement()
        {
            Vector3 movementVelocity = transformSpringComponent.positionSpring.GetVelocity() / 100f;
            reactorRotationParentRectTransform.localEulerAngles =
                new Vector3(movementVelocity.y * reactorRotationYAmount, 0, movementVelocity.x * reactorRotationXAmount);
        }
        
        private void AutoTiltRotation()
        {
			float sine = Mathf.Sin((Time.time + (cardIndex * 0.33f)) * autoRotationTiltSpeed) * autoRotationTiltAmount;
            float cosine = Mathf.Cos((Time.time + (cardIndex * 0.33f)) * autoRotationTiltSpeed) * autoRotationTiltAmount;
            Vector3 newRotation = new Vector3(sine, cosine, 0f);
            tiltRotationParentRectTransform.localEulerAngles = newRotation;
        }
        
        public bool IsBeingDragged()
        {
            return simpleDraggable.isDragging;
        }
        
        public bool IsBeingPointed()
        {
			return simpleDraggable.isPointed;
        }
        
        public void PointerEnter()
        {
			SetCardAtHighlightState();
			demoCardController.CardWasHighlighted(this);
		}
        
        public void PointerExit()
        {
            if(!simpleDraggable.isDragging)
            {
                SetCardAtRestState();
            }
        }
        
        public void DragStarted()
        {
            demoCardController.SomeCardIsBeingDragged(true, this);
			targetRectTransform.rotation = Quaternion.identity;
        }

        public void DragMoved()
        {
            demoCardController.UpdateCardSlots(this);
        }
        
        public void DragEnded()
        {
            demoCardController.UpdateCardSlots(null);
            demoCardController.SomeCardIsBeingDragged(false, null);
            
        }

        public void ShuffleSpringPunch()
        {
            transformSpringComponent.positionSpring.AddVelocity(Vector3.up * Random.Range(-maxPunchVerticalVelocity, maxPunchVerticalVelocity));
        }

        public void ToggleFlipState()
        {
            Vector3 currentSpringEuler = childSpringComponent.rotationSpring.GetCurrentValue().eulerAngles;
            Vector3 targetEuler = currentSpringEuler;
            targetEuler.y = targetEuler.y >= 180f ? 0f : 180f;

			childSpringComponent.SetRotationTarget(targetEuler);
        }
        
        public void SetCardToUnflipped()
        {
            childSpringComponent.SetRotationTarget(Vector3.zero);
        }
        
        public void SetCardToFlipped()
        {
			childSpringComponent.SetRotationTarget(Vector3.up * 180f);
        }
        
        private void SetCardAtHighlightState()
        {
            if(demoCardController.IsAnyCardBeingDragged())
            {
                return;
            }
            transformSpringComponent.positionSpring.unifiedDrag = posSpringDragWhenDragging;
            transformSpringComponent.positionSpring.unifiedForce = posSpringForceWhenDragging;
            transformSpringComponent.AddVelocityToPositionSpring(Vector3.up * positionSpringPunchWhenPointed);
            childSpringComponent.SetScaleTarget(Vector3.one * scaleSpringTargetWhenPointed);
            childSpringComponent.AddVelocityToScaleSpring(Vector3.one * scaleSpringPunchWhenPointed);
            backgroundColorSpringComponent.SetTarget(highlightedCardColor);
            selectedIndicatorGameObject.SetActive(true);
        }
        
        public void SetCardAtRestState()
        {
            if(demoCardController.IsAnyCardBeingDragged() && demoCardController.GetSelectedCard().Equals(this))
            {
                return;
            }

            transformSpringComponent.positionSpring.unifiedDrag = posSpringDragWhenNotDragging;
            transformSpringComponent.positionSpring.unifiedForce = posSpringForceWhenNotDragging;
            childSpringComponent.SetScaleTarget(Vector3.one);
            backgroundColorSpringComponent.SetTarget(normalCardColor);
            selectedIndicatorGameObject.SetActive(false);
        }
        
        public void SetCardAtClosingState()
        {
            transformSpringComponent.positionSpring.unifiedForce = posSpringForceWhenClosing;
        }
        
        private void CardDressWithRandomColors()
        {
            currentCardColor = cardColors[cardIndex % cardColors.Length];
            highlightedCardColor = Color.Lerp(normalCardColor, currentCardColor, colorHighlightFactor);
            
            foreach (Image cardImage in cardImages)
            {
                cardImage.color = currentCardColor;
            }
            
            foreach (TextMeshProUGUI cardText in cardTexts)
            {
                cardText.color = currentCardColor;
            }
        }

        private void OnDestroy()
        {
            demoCardController.OnClose -= SetCardAtClosingState;
        }
    }
}