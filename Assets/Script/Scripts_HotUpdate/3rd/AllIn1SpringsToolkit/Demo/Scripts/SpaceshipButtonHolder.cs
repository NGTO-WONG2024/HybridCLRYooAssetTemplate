namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class SpaceshipButtonHolder : Demo3dButtonHolder
    {
        private SpaceshipController spaceshipController;
        
        public override void Initialize(DemoElement demoElement, bool hideUi)
        {
            base.Initialize(demoElement, hideUi);
            spaceshipController = (SpaceshipController) demoElement;
        }

        public void ShakeRotation()
        {
            if(!spaceshipController.IsOpen()) return;
            spaceshipController.ShakeRotation();
        }
    }
}