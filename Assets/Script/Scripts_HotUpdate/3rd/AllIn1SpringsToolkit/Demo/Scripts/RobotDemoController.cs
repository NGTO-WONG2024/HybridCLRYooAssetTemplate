using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class RobotDemoController : DemoElement
    {
        [Space, Header("Robot")]
        [SerializeField] private TransformSpringComponent robotSpringComponent;
        [SerializeField] private Transform targetTransform, smileTransform;
        [SerializeField] private float maxMoveRadius, punchPositionStrength;
        [SerializeField] private float scaleMin, scaleMax, punchScaleStrength;
        [SerializeField] private float punchRotationStrength;
        [SerializeField] private float upAndDownSpeed, upAndDownAmplitude;
        [SerializeField] private float smileRotateAmount;

        private Vector3 initialPosition, currentRandomPosition;
        private float bobOffset;
        private Quaternion initialSmileRotation;

        private void Start()
        {
            initialPosition = targetTransform.localPosition;
            currentRandomPosition = initialPosition;
            initialSmileRotation = smileTransform.localRotation;
        }

        private void Update()
        {
            bobOffset = Mathf.Sin(Time.time * upAndDownSpeed) * upAndDownAmplitude;
            Vector3 bobPosition = currentRandomPosition + Vector3.up * bobOffset;
            targetTransform.localPosition = bobPosition;
            
            Vector3 localVelocity = robotSpringComponent.positionSpring.GetVelocity();
            localVelocity = smileTransform.InverseTransformDirection(localVelocity);
            Vector3 eulerVelocity = localVelocity * smileRotateAmount;
            eulerVelocity.x = Mathf.Clamp(eulerVelocity.x, -45, 45);
            smileTransform.localRotation = initialSmileRotation * Quaternion.Euler(localVelocity * smileRotateAmount);
        }

        public void RandomPosition()
        {
            float currentDistanceToInitial = Vector3.Distance(currentRandomPosition, initialPosition);
            bool isTargetInInnerCircle = currentDistanceToInitial < maxMoveRadius / 2f;
            Vector3 newOffset = new Vector3(Random.Range(-maxMoveRadius, maxMoveRadius), Random.Range(-maxMoveRadius, maxMoveRadius), 0f);
            bool isNewInInnerCircle = Vector3.Distance(newOffset, initialPosition) < maxMoveRadius / 2f;
            while(isTargetInInnerCircle == isNewInInnerCircle)
            {
                newOffset = new Vector3(Random.Range(-maxMoveRadius, maxMoveRadius), Random.Range(-maxMoveRadius, maxMoveRadius), 0f);
                isNewInInnerCircle = Vector3.Distance(newOffset, initialPosition) < maxMoveRadius / 2f;
            }
            currentRandomPosition = initialPosition + newOffset;
        }

        public void PunchPosition()
        {
            robotSpringComponent.positionSpring.AddVelocity(Random.insideUnitSphere.normalized * punchPositionStrength * GetRandomPunchMultiplier());
        }

        public void RandomScale()
        {
            float currentScale = targetTransform.localScale.x;
            float randomScale = Random.Range(scaleMin, scaleMax);
            float maxScaleGap = scaleMax - scaleMin;
            while(Mathf.Abs(currentScale - randomScale) < maxScaleGap / 3f)
            {
                randomScale = Random.Range(scaleMin, scaleMax);
            }
            targetTransform.localScale = new Vector3(randomScale, randomScale, randomScale);
        }

        public void PunchScale()
        {
            float randomPunchMultiplier = GetRandomPunchMultiplier();
            randomPunchMultiplier = Random.value > 0.5f ? randomPunchMultiplier : -randomPunchMultiplier;
            robotSpringComponent.scaleSpring.AddVelocity(Vector3.one * punchScaleStrength * randomPunchMultiplier);
        }

        public void RandomRotation()
        {
            targetTransform.localPosition = Vector3.zero;
            if(targetTransform.rotation == Quaternion.identity)
            {
                targetTransform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));   
            }
            else
            {
                targetTransform.rotation = Quaternion.identity;
            }
        }

        public void PunchRotation()
        {
            robotSpringComponent.rotationSpring.AddVelocity(Random.insideUnitSphere.normalized * punchRotationStrength * GetRandomPunchMultiplier());
        }

        private float GetRandomPunchMultiplier()
        {
            return Random.Range(0.5f, 1.5f);
        }
    }
}