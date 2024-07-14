using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class SpaceshipController : DemoElement
    {
        [Space, Header("Spaceship")]
        [SerializeField] private TransformSpringComponent spaceshipTransformSpring;
        [SerializeField] private float rotationPunchStrength;
        [SerializeField] private Transform spaceshipTransform, parentForwardTransform;
        [SerializeField] private FloatSpringComponent hoverFloatSpring;
        [SerializeField] private Vector3SpringComponent hoverForwardSpring;
        [SerializeField] private ShaderFloatSpringComponent shaderFloatSpring;
        [SerializeField] private float onShakeFloatSpringPunch;
        [SerializeField] private CamFovOrSizeSpringComponent camFovSpringComponent;
        [SerializeField] private float onShakeCamFovSpringPunch;
        [SerializeField] private float onShakeScaleSpringPunch;
        [SerializeField] private float desiredHoverDistance, maxHoverDistance;

        [Space, Header("Rock Spawning")]
        [SerializeField] private Transform spawnOrigin;
        [SerializeField] private SpaceshipRockMove[] rockSprings;
        [SerializeField] private float spawnWidth;
        [SerializeField] private float spawnMinTime, spawnMaxTime;
        [SerializeField] private float rockOffScreenMoveSpeed;
        
        private float timeUntilNextSpawn;
        private int currentRockIndex;

        private void Start()
        {
            GetRandomNextSpawnTime();
        }

        public override void Initialize(bool hideUi)
        {
            base.Initialize(hideUi);
            
            foreach(SpaceshipRockMove rockSpring in rockSprings)
            {
                rockSpring.gameObject.SetActive(false);
            }
            
            //Have a few rocks since the beginning
            int randomRockCount = Random.Range(5, 10);
            for (int i = 0; i < randomRockCount; i++)
            {
                SpawnRock(Random.Range(0f, 20f));
            }
        }

        private void Update()
        {
            if(isOpen)
            {
                SpaceshipSpringsUpdate();
                SpawnRocksUpdate();
            }
            else
            {
                //Somewhat hacky way to move the rocks out of the way when the demo is transitioning out
                //This way we prevent them from showing temporarily from offscreen
                Vector3 moveRockDirection = transform.position.x > 0f ? Vector3.left : Vector3.right;
                spawnOrigin.localPosition += moveRockDirection * (rockOffScreenMoveSpeed * Time.deltaTime);
            }
        }

        private void SpaceshipSpringsUpdate()
        {
            //Raycast down to find the ground so that we can set the float spring target and find the hit normal
            Vector3 hitNormal = Vector3.up;
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 100f))
            {
                float targetHoverDistance = Mathf.Min(maxHoverDistance, hit.point.y + desiredHoverDistance);
                hoverFloatSpring.SetTarget(targetHoverDistance);
                hitNormal = hit.normal;
            }
    
            //Position the hovercraft at the float spring value. It makes the hovercraft float
            Vector3 newHoverCraftPosition = spaceshipTransform.position;
            newHoverCraftPosition.y = hoverFloatSpring.GetCurrentValue();
            spaceshipTransform.position = newHoverCraftPosition;
        
            //Rotate the hovercraft to match the hitNormal with a spring
            hoverForwardSpring.SetTarget(Vector3.Cross(parentForwardTransform.right, hitNormal));
            Vector3 forwardDirection = hoverForwardSpring.GetCurrentValue();
            if (forwardDirection != Vector3.zero)
            {
                spaceshipTransform.forward = forwardDirection;
            }
        }

        private void SpawnRocksUpdate()
        {
            if(timeUntilNextSpawn <= 0)
            {
                SpawnRock();
                GetRandomNextSpawnTime();
            }
            else
            {
                timeUntilNextSpawn -= Time.deltaTime;
            }
        }

        private void SpawnRock(float zOffset = 0f)
        {
            Vector3 randomOffset = new Vector3(Random.Range(-spawnWidth, spawnWidth), 0f, 0f);
            randomOffset = spawnOrigin.TransformDirection(randomOffset);
            Vector3 spawnPosition = spawnOrigin.position + randomOffset;
            
            int startIndex = currentRockIndex;
            while (rockSprings[currentRockIndex].gameObject.activeInHierarchy)
            {
                currentRockIndex = (currentRockIndex + 1) % rockSprings.Length;
        
                if (currentRockIndex == startIndex)
                {
                    return;
                }
            }
            SpaceshipRockMove spaceshipRockMove = rockSprings[currentRockIndex];
            
            spawnPosition += spawnOrigin.forward * -zOffset;
            spaceshipRockMove.Initialize(spawnPosition);
        }

        public void ShakeRotation()
        {
            spaceshipTransformSpring.AddVelocityToRotationSpring(new Vector3(0f, 0f, Random.Range(-rotationPunchStrength, rotationPunchStrength)));
            spaceshipTransformSpring.AddVelocityToScaleSpring(Vector3.one * onShakeScaleSpringPunch);
            shaderFloatSpring.AddVelocity(onShakeFloatSpringPunch);
            camFovSpringComponent.AddVelocity(onShakeCamFovSpringPunch);
        }
        
        private void GetRandomNextSpawnTime()
        {
            timeUntilNextSpawn = Random.Range(spawnMinTime, spawnMaxTime);
        }
        
#if UNITY_EDITOR
        [ContextMenu("Get Spaceship References")]
        private void GetSpaceshipReferences()
        {
            int childCount = spawnOrigin.childCount;
            rockSprings = new SpaceshipRockMove[childCount];
            for(int i = 0; i < childCount; i++)
            {
                rockSprings[i] = spawnOrigin.GetChild(i).GetComponent<SpaceshipRockMove>();
            }
            
            EditorUtility.SetDirty(this);
        }
#endif
    }
}