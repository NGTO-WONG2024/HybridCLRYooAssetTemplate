namespace AllIn1SpringsToolkit.Demo.Scripts
{
    public class ButtonPress3DButtonHolder : Demo3dButtonHolder
    {
        private ButtonPress3DController buttonPress3DController;
        
        public override void Initialize(DemoElement demoElement, bool hideUi)
        {
            base.Initialize(demoElement, hideUi);
            buttonPress3DController = (ButtonPress3DController) demoElement;
        }

        public void ButtonPress()
        {
            if(!buttonPress3DController.IsOpen()) return;
            buttonPress3DController.ButtonPress();
        }
    }
}