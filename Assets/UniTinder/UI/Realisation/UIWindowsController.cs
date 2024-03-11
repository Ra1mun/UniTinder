using System.Collections.Generic;


namespace UniTinder.UI.Realisation
{
    public class UIWindowsController
    {
        private readonly LinkedList<IWindowController> _windowControllers = new LinkedList<IWindowController>();

        public UIWindowsController(
            UIRegistrationWindowController uiStartWindowController,
            UIRegistrationWindowController uiRegistrationWindowController,
            UIMatchWindowController uiMatchWindowController,
            UIProfileWindowController uiProfileWindowController,
            UIChatWindowController uiChatWindowController)
        {
            SetupWindow(uiStartWindowController);
            SetupWindow(uiRegistrationWindowController);
            SetupWindow(uiMatchWindowController);
            SetupWindow(uiProfileWindowController);
            SetupWindow(uiChatWindowController);
        }

        public void ShowFirstWindow()
        {
            _windowControllers.First?.Value.ShowWindow();
        }

        private void SetupWindow(IWindowController windowController)
        {
            if (_windowControllers.Last != null)
            {
                _windowControllers.Last.Value.GoToNext += windowController.ShowWindow;
            
                windowController.GoToPrevious += _windowControllers.Last.Value.ShowWindow;
            }

            _windowControllers.AddLast(windowController);
        }
    }
}
