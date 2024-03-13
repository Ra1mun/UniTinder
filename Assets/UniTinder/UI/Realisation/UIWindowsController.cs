using System.Collections.Generic;


namespace UniTinder.UI.Realisation
{
    public class UIWindowsController
    {
        private readonly UIProfileWindowController _uiProfileWindowController;
        private readonly UIChatWindowController _uiChatWindowController;
        private readonly LinkedList<IWindowController> _windowControllers = new LinkedList<IWindowController>();

        public UIWindowsController(
            UIStartWindowController uiStartWindowController,
            UIRegistrationWindowController uiRegistrationWindowController,
            UIMatchWindowController uiMatchWindowController,
            UIProfileWindowController uiProfileWindowController,
            UIChatWindowController uiChatWindowController)
        {
            _uiProfileWindowController = uiProfileWindowController;
            _uiChatWindowController = uiChatWindowController;
            
            SetupWindow(uiStartWindowController);
            SetupWindow(uiRegistrationWindowController);
            SetupMatch(uiMatchWindowController);
            SetupProfile(uiProfileWindowController);
            SetupChat(uiChatWindowController);
        }

        public void ShowFirstWindow()
        {
            _windowControllers.First?.Value.ShowWindow();
        }

        private void SetupChat(UIChatWindowController chatWindowController)
        {
            if (_windowControllers.Last != null)
            {
                chatWindowController.GoToPreviousWindow += _windowControllers.Last.Value.ShowWindow;
            }
        }

        private void SetupProfile(UIProfileWindowController profileWindowController)
        {
            if (_windowControllers.Last != null)
            {
                profileWindowController.GoToPreviousWindow += _windowControllers.Last.Value.ShowWindow;
            }
        }

        private void SetupMatch(UIMatchWindowController matchWindowController)
        {
            if (_uiProfileWindowController != null)
            {
                matchWindowController.GoToProfileWindow += _uiProfileWindowController.ShowWindow;
            }

            if (_uiChatWindowController != null)
            {
                matchWindowController.GoToChatWindow += _uiChatWindowController.ShowWindow;
            }

            _windowControllers.AddLast(matchWindowController);
        }

        private void SetupWindow(IWindowController windowController)
        {
            if (_windowControllers.Last != null)
            {
                _windowControllers.Last.Value.GoToNextWindow += windowController.ShowWindow;
            
                windowController.GoToPreviousWindow += _windowControllers.Last.Value.ShowWindow;
            }

            _windowControllers.AddLast(windowController);
        }
    }
}
