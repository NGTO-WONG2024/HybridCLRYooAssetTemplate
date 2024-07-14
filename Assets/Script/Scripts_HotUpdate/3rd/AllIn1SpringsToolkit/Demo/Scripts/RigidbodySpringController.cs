using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class RigidbodySpringController : DemoElement
    {
        [Space, Header("Rigidbody Spring")]
        [SerializeField] private RigidbodySpringComponent rigidbodySpring;
        [SerializeField] private float positionPunchForce, rotationPunchForce, initialWaitTime;
        [SerializeField] private Transform springTargetT;
        
        [Space, Header("Color Spring")]
        [SerializeField] private ColorSpringComponent colorSpring;
        [SerializeField] private Color punchColor;

        [Space, Header("Balls")]
        [SerializeField] private bool spawnIsOn;
        [SerializeField] private Transform ballSpawnPointT;
        [SerializeField] private Transform[] balls;
        [SerializeField] private MeshRenderer[] ballMeshRenderers;
        [SerializeField] private Rigidbody[] ballRigidbodies;
        [SerializeField] private Color[] ballColors;
        [SerializeField] private float spawnTime, spawnRadius, heightForDeactivation;

        private Coroutine spawnCoroutine;
        private int currentBallIndex;

        private void Start()
        {
            currentBallIndex = 0;
            rigidbodySpring.enabled = false;
            springTargetT.SetParent(transform);
            
            for(int i = 0; i < balls.Length; i++)
            {
                balls[i].gameObject.SetActive(false);
            }

            spawnCoroutine = StartCoroutine(SpawnBallsCR());
        }

        private IEnumerator SpawnBallsCR()
        {
            yield return new WaitForSeconds(initialWaitTime);
            rigidbodySpring.enabled = true;
            
            while(true)
            {
                SpawnBall();
                yield return new WaitForSeconds(spawnTime);
                CheckHeightForDeactivation();
            }
        }

        private void SpawnBall()
        {
            if(!spawnIsOn)
            {
                return;
            }
            
            int startIndex = currentBallIndex;
            while (balls[currentBallIndex].gameObject.activeInHierarchy)
            {
                currentBallIndex = (currentBallIndex + 1) % balls.Length;
        
                if (currentBallIndex == startIndex)
                {
                    return;
                }
            }
            
            Vector3 randomPosition = ballSpawnPointT.position + Random.insideUnitSphere * spawnRadius;
            balls[currentBallIndex].position = randomPosition;
            int randomColorIndex = Random.Range(0, ballColors.Length);
            ballMeshRenderers[currentBallIndex].material.color = ballColors[randomColorIndex];
            ballRigidbodies[currentBallIndex].linearVelocity = Vector3.zero;
            balls[currentBallIndex].gameObject.SetActive(true);
            currentBallIndex = (currentBallIndex + 1) % (balls.Length - 1);
        }
        
        private void CheckHeightForDeactivation()
        {
            for(int i = 0; i < balls.Length; i++)
            {
                if(balls[i].position.y < heightForDeactivation)
                {
                    balls[i].gameObject.SetActive(false);
                    continue;
                }

                if(balls[i].position.y > ballSpawnPointT.position.y)
                {
                    balls[i].gameObject.SetActive(false);
                }
            }
        }

        public void PositionPunch()
        {
            rigidbodySpring.positionSpring.AddVelocity(Vector3.up * positionPunchForce);
            colorSpring.SetCurrentValue(punchColor);
        }

        public void RotationPunch()
        {
            float randomSign = Random.Range(0, 2) == 0 ? -1 : 1;
            Vector3 randomRotationAxis = Random.Range(0, 2) == 0 ? Vector3.forward : Vector3.right;
            rigidbodySpring.rotationSpring.AddVelocity(randomSign * randomRotationAxis * rotationPunchForce);
            colorSpring.SetCurrentValue(punchColor);
        }

        public override void Close()
        {
            base.Close();
            rigidbodySpring.enabled = false;
            if(spawnCoroutine != null)
            {
                StopCoroutine(spawnCoroutine);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Get Ball References")]
        private void GetBallReferences()
        {
            int childCount = ballSpawnPointT.childCount;
            balls = new Transform[childCount];
            ballMeshRenderers = new MeshRenderer[childCount];
            ballRigidbodies = new Rigidbody[childCount];
            for(int i = 0; i < ballSpawnPointT.childCount; i++)
            {
                balls[i] = ballSpawnPointT.GetChild(i);
                ballMeshRenderers[i] = balls[i].GetComponent<MeshRenderer>();
                ballRigidbodies[i] = balls[i].GetComponent<Rigidbody>();
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }
}