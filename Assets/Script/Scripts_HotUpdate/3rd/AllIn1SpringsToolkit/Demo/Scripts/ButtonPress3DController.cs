using UnityEngine;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class ButtonPress3DController : DemoElement
    {
        [Space, Header("3D Button")]
        [SerializeField] private TransformSpringComponent buttonTransformSpring;
        [SerializeField] private TransformSpringComponent buttonBaseTransformSpring;
        [SerializeField] private ColorSpringComponent colorSpringComponent;
        [SerializeField] private CamFovOrSizeSpringComponent camFovSpringComponent;
        [SerializeField] private float camFovPressed;
        [SerializeField] private Vector3 pressedScale, basePressedScale;
        [SerializeField] private float pressedPunchScale;
        [SerializeField] private Color pressedColor;
        
        private bool isPressed;
        private Color startColor;
        private float startFov;

        private void Start()
        {
            startColor = colorSpringComponent.GetCurrentValue();
            startFov = camFovSpringComponent.fovSpring.GetCurrentValue();
        }

        public void MouseDown()
        {
            //Immediately set the target scale to the pressed scale
            buttonTransformSpring.SetScaleTarget(pressedScale);
            buttonTransformSpring.ReachEquilibrium();
            buttonBaseTransformSpring.SetScaleTarget(basePressedScale);
            buttonBaseTransformSpring.ReachEquilibrium();
            
            //Add a punch scale to the button for a more dynamic effect
            Vector3 punchScaleVector = new Vector3(1f, 0f, 1f) * pressedPunchScale;
            buttonTransformSpring.AddVelocityToScaleSpring(punchScaleVector);
            buttonBaseTransformSpring.AddVelocityToScaleSpring(punchScaleVector);
            
            //Animate button to red when pressed
            colorSpringComponent.SetTarget(pressedColor);
            colorSpringComponent.ReachEquilibrium();
            
            //Set the camera fov to the pressed fov
            camFovSpringComponent.SetTargetAndReachEquilibrium(camFovPressed);
        }
        
        public void MouseUp()
        {
            //Set the target scale back to the original scale so we smoothly animate back to it
            buttonTransformSpring.SetScaleTarget(Vector3.one);
            buttonBaseTransformSpring.SetScaleTarget(Vector3.one);
            
            //Animate button back to original green color
            colorSpringComponent.SetTarget(startColor);
            
            //Set the camera fov back to the original fov
            camFovSpringComponent.fovSpring.SetTarget(startFov);
        }

        public void ButtonPress()
        {
            MouseDown();
            MouseUp();
        }

        private void Update()
        {
            if(!isOpen)
            { 
                camFovSpringComponent.SetTargetAndReachEquilibrium(startFov);
            }
        }
    }
}