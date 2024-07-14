namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class RigidbodySpringButtonHolder : Demo3dButtonHolder
    {
        private RigidbodySpringController rigidbodySpringController;
        
        public override void Initialize(DemoElement demoElement, bool hideUi)
        {
            base.Initialize(demoElement, hideUi);
            rigidbodySpringController = (RigidbodySpringController) demoElement;
        }

        public void PositionPunch()
        {
            if(!rigidbodySpringController.IsOpen()) return;
            rigidbodySpringController.PositionPunch();
        }
        
        public void RotationPunch()
        {
            if(!rigidbodySpringController.IsOpen()) return;
            rigidbodySpringController.RotationPunch();
        }
    }
}