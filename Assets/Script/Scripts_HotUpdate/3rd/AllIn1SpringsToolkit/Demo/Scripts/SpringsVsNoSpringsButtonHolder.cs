namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class SpringsVsNoSpringsButtonHolder : Demo3dButtonHolder
    {
        private SpringsVsNoSpringsDemoController springsVsNoSpringsDemoController;
        
        public override void Initialize(DemoElement demoElement, bool hideUi)
        {
            base.Initialize(demoElement, hideUi);
            springsVsNoSpringsDemoController = (SpringsVsNoSpringsDemoController) demoElement;
        }

        public void RandomHitsButtonPress()
        {
            if(!springsVsNoSpringsDemoController.IsOpen()) return;
            springsVsNoSpringsDemoController.RandomHitsButtonPress();
        }
    }
}