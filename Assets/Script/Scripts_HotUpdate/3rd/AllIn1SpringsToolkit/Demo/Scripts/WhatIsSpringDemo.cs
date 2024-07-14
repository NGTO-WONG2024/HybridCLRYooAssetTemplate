using UnityEngine;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class WhatIsSpringDemo : DemoElement
    {
        [SerializeField] private AnchoredPositionSpringComponent anchoredPositionSpring;
        [SerializeField] private RectTransform targetRectTransform, moveAreaRectTransform;
        [SerializeField] private float minDistanceThreshold;
        [SerializeField] private float scaleVarianceMin, scaleVarianceMax;
        [SerializeField] private float rotationVariance;

        [Space, Header("Target Object Polish")]
        [SerializeField] private Transform targetTransform;
        [SerializeField] private TransformSpringComponent targetTransformSpring;
        [SerializeField] private ColorSpringComponent targetColorSpring;
        [SerializeField] private float scalePunchAfterTeleport;
        [SerializeField] private Color colorAfterTeleport;

        private float lastPositionChangeTime, timeOpenStarted;
        private bool useRightHalf = false;

        private void Update()
        {
            if(timeOpenStarted < 0.6f)
            {
                if(isOpen)
                {
                    timeOpenStarted += Time.deltaTime;   
                }
                return;
            }
            
            if (anchoredPositionSpring.GetVelocity().magnitude < 0.25f && lastPositionChangeTime + 0.1f < Time.time)
            {
                RandomizeTarget();
            }
        }

        private void RandomizeTarget()
        {
            lastPositionChangeTime = Time.time;
            Vector2 newPosition = GenerateNewPosition();
            anchoredPositionSpring.SetTarget(newPosition);
            
            targetTransform.localRotation = Quaternion.Euler(0, 0, UnityEngine.Random.Range(0f, rotationVariance) * (useRightHalf ? 1f : -1f));
            
            targetTransformSpring.scaleSpring.SetTarget(
                Vector3.one * UnityEngine.Random.Range(1f - scaleVarianceMin, 1f + scaleVarianceMax)
            );
            targetTransformSpring.scaleSpring.AddVelocity(Vector3.one * scalePunchAfterTeleport);
            
            targetColorSpring.SetCurrentValue(colorAfterTeleport);
        }

        private Vector2 GenerateNewPosition()
        {
            float rectWidth = moveAreaRectTransform.rect.width;
            float rectHeight = moveAreaRectTransform.rect.height;
            Vector2 currentPosition = targetRectTransform.anchoredPosition;

            Vector2 newPosition = NewPosition(rectWidth, rectHeight);
            int tries = 0;
            while(tries < 10 && Vector2.Distance(newPosition, currentPosition) < minDistanceThreshold)
            {
                newPosition = NewPosition(rectWidth, rectHeight);
                tries++;
            }

            useRightHalf = !useRightHalf;

            return newPosition;
        }

        private Vector2 NewPosition(float rectWidth, float rectHeight)
        {
            float xMin = useRightHalf ? 0 : -rectWidth / 2f;
            float xMax = useRightHalf ? rectWidth / 2f : 0;

            Vector2 newPosition = new(
                UnityEngine.Random.Range(xMin, xMax),
                UnityEngine.Random.Range(-rectHeight / 2f, rectHeight / 2f)
            );
            return newPosition;
        }
    }
}