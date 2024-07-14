using UnityEngine;

namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class SpringsVsNoSpringsDemoController : DemoElement
    {
        [Space, Header("Dummies")]
        [SerializeField] private DummyMovement[] dummyMovements;

        public void RandomHitsButtonPress()
        {
            if(!isOpen)
            {
                return;
            }
            
            foreach(DummyMovement dummy in dummyMovements)
            {
                dummy.DummyHitRandom();
            }
        }
    }
}