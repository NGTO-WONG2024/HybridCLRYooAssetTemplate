using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class DummyMovement : MonoBehaviour
    {
        [SerializeField] private TransformSpringComponent baseTransformSpring;
        [SerializeField] private TransformSpringComponent[] eyesTransformSprings;
        [SerializeField] private GameObject whiteRingHitPrefab;
        
        [Space, Header("Hit Properties")]
        [SerializeField] private float scalePunch;
        [SerializeField] private float angleHitRandomVariance, byDirectionVariance;
        [SerializeField] private float minRotationPunch, maxRotationPunch;
        [SerializeField] private float minEyeRotationPunch, maxEyeRotationPunch;
        [SerializeField] private float minEyeScalePunch, maxEyeScalePunch;
        
        [Space, Header("No Spring Hit")]
        [SerializeField] private bool doNoSpringAnimation;
        [SerializeField] private float rotationAngle, animationDuration;
        [SerializeField] private int oscillations;
        
        private Quaternion initialLocalBaseRotation;
        private Coroutine noSpringHitCoroutine;
        private Camera myCamera;

        private void Start()
        {
            myCamera = Camera.main;
            initialLocalBaseRotation = baseTransformSpring.transform.localRotation;
        }

        public void DummyHitRandom()
        {
            float randomAngle = Random.Range(-angleHitRandomVariance, angleHitRandomVariance);
            Vector3 hitDirection = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.back;
            DummyHitByDirection(hitDirection);
        }

        private void OnMouseDown()
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 dummyScreenPosition = myCamera.WorldToScreenPoint(transform.position);
            
            Vector3 mouseWorldPosition = myCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, dummyScreenPosition.z));
            Instantiate(whiteRingHitPrefab, mouseWorldPosition, Quaternion.identity);

            float xOffset = dummyScreenPosition.x - mousePosition.x;
            float xDirection = Mathf.Sign(xOffset);
            Vector3 hitDirection = Quaternion.AngleAxis(xDirection * Mathf.Abs(xOffset) * 900f / Screen.width, Vector3.up) * Vector3.back;
            
            hitDirection = RandomizeHitDirection(hitDirection);

            DummyHitByDirection(hitDirection);
        }
        
        private void DummyHitByDirection(Vector3 hitDirection)
        {
            if(doNoSpringAnimation)
            {
                Transform dummyTransform = baseTransformSpring.transform;
                Vector3 rotationAxis = Vector3.Cross(Vector3.up, hitDirection.normalized);
                if(noSpringHitCoroutine != null)
                {
                    dummyTransform.localRotation = initialLocalBaseRotation;
                    StopCoroutine(noSpringHitCoroutine);
                }
                noSpringHitCoroutine = StartCoroutine(BackAndForthRotation(dummyTransform, rotationAxis));
            }
            else
            {
                baseTransformSpring.AddVelocityToScaleSpring(Vector3.one * scalePunch);
                Vector3 rotationAxis = Vector3.Cross(Vector3.up, hitDirection.normalized);
                baseTransformSpring.AddVelocityToRotationSpring(rotationAxis * Random.Range(minRotationPunch, maxRotationPunch));
            
                foreach(TransformSpringComponent eyeSpring in eyesTransformSprings)
                {
                    eyeSpring.AddVelocityToScaleSpring(Vector3.one * Random.Range(minEyeScalePunch, maxEyeScalePunch));
                    eyeSpring.AddVelocityToRotationSpring(Random.onUnitSphere * Random.Range(minEyeRotationPunch, maxEyeRotationPunch));
                }   
            }
        }

        private IEnumerator BackAndForthRotation(Transform targetTransform, Vector3 rotationAxis)
        {
            Quaternion initialRotation = targetTransform.rotation;
            float elapsedTime = 0f;

            while (elapsedTime < animationDuration)
            {
                float t = elapsedTime / animationDuration;
                float angle = Mathf.Sin(t * oscillations * Mathf.PI) * rotationAngle;
            
                targetTransform.rotation = initialRotation * Quaternion.AngleAxis(angle, rotationAxis);
            
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            targetTransform.rotation = initialRotation;
        }

        private Vector3 RandomizeHitDirection(Vector3 hitDirection)
        {
            hitDirection = new Vector3(Random.Range(-byDirectionVariance, byDirectionVariance),
                               Random.Range(-byDirectionVariance, byDirectionVariance),
                               Random.Range(-byDirectionVariance, byDirectionVariance))
                           + hitDirection;
            return hitDirection;
        }
    }
}