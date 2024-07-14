using UnityEngine;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif

namespace AllIn1SpringsToolkit.Scripts.Utils
{
    [DefaultExecutionOrder(500)]
    public class SpringBone : MonoBehaviour
    {
		[SerializeField] private bool autoUpdate = true;
        [SerializeField] private Transform transformToReactTo;
        [SerializeField] private Transform boneTransform;
        
        [Space, Header("Transform Spring")]
        [SerializeField] private TransformSpringComponent transformSpringComponent;

        [Space, Header("Movement Inertia")]
        [SerializeField] private bool movementInertiaEnabled;
        [SerializeField] private float movementInertia;
        
        private Vector3 reactToLastPosition;
        private Quaternion initialRotation;
        private Vector3 initialUpVector;
        
        private void Start()
        {
            initialRotation = Quaternion.Inverse(transformToReactTo.rotation) * boneTransform.rotation;
            transformSpringComponent.rotationSpring.SetTarget(transformToReactTo.rotation * initialRotation);
            transformSpringComponent.rotationSpring.ReachEquilibrium();
            initialUpVector = transformToReactTo.up;

            if(movementInertiaEnabled)
            {
                reactToLastPosition = transformToReactTo.position;
            }
        }

        private void LateUpdate()
        {
            if(autoUpdate)
            {
                UpdateMethod();   
            }
        }

        public void UpdateMethod()
        {
            Quaternion targetRotation = transformToReactTo.rotation * initialRotation;
            transformSpringComponent.rotationSpring.SetTarget(targetRotation);
            boneTransform.rotation = transformSpringComponent.rotationSpring.GetCurrentValue();
            
            if(movementInertiaEnabled)
            {
                Vector3 reactToPositionDelta = transformToReactTo.position - reactToLastPosition;
                Vector3 currentUpVector = transformToReactTo.rotation * initialUpVector;
                Vector3 rotationAxis = Vector3.Cross(reactToPositionDelta, currentUpVector).normalized;
                float angle = reactToPositionDelta.magnitude * movementInertia * Time.deltaTime;
                if(angle > 0f)
                {
                    Vector3 localRotationAxis = Quaternion.Inverse(boneTransform.rotation) * rotationAxis;
                    transformSpringComponent.rotationSpring.AddVelocity(localRotationAxis * angle);
                }

                reactToLastPosition = transformToReactTo.position;
            }
        }
        
        public void SetAutoUpdate(bool value)
        {
            autoUpdate = value;
        }

        #region Editor helpers
        #if UNITY_EDITOR
        private void Reset()
        {
            transformToReactTo = transform.parent;
            boneTransform = transform;
            if(transformSpringComponent == null)
            {
                transformSpringComponent = GetComponent<TransformSpringComponent>();
            }
        }

        [ContextMenu("Auto setup for all children")]
        private void AddBonesToHierarchy()
        {
            foreach(Transform child in transform)
            {
                TransformSpringComponent newTransformSpringComponent = child.GetComponent<TransformSpringComponent>();
                if(newTransformSpringComponent == null)
                {
                    newTransformSpringComponent = child.gameObject.AddComponent<TransformSpringComponent>();
                }

                SpringBone springBone = child.GetComponent<SpringBone>();
                if(springBone == null)
                {
                    springBone = child.gameObject.AddComponent<SpringBone>();
                }
            }

            SetChildSpringsToThis();
        }

        [ContextMenu("Copy Spring Component To Children")]
        private void SetChildSpringsToThis()
        {
            ComponentUtility.CopyComponent(transformSpringComponent);
            TransformSpringComponent[] springComponents = GetComponentsInChildren<TransformSpringComponent>();
            foreach(TransformSpringComponent springComponent in springComponents)
            {
                bool previousUseTransformAsTarget = springComponent.useTransformAsTarget;
                Transform previousTargetTransform = springComponent.targetTransform;
                ComponentUtility.PasteComponentValues(springComponent);
                springComponent.useTransformAsTarget = previousUseTransformAsTarget;
                springComponent.targetTransform = previousTargetTransform;
            }
        }
        #endif
        #endregion
    }
}