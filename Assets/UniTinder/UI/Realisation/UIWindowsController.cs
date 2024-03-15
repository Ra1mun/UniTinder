using System.Collections.Generic;


namespace UniTinder.UI.Realisation
{
    public class UIWindowsController
    {
        private readonly UILoginWindowController _uiLoginWindowController;
        private readonly UIRegistrationWindowController _uiRegistrationWindowController;
        private readonly UIMatchWindowController _uiMatchWindowController;
        private readonly UIProfileWindowController _uiProfileWindowController;
        private readonly UIChatWindowController _uiChatWindowController;
        
        private readonly LinkedList<IWindowController> _windowControllers = new LinkedList<IWindowController>();

        public UIWindowsController(
            UILoginWindowController uiLoginWindowController,
            UIRegistrationWindowController uiRegistrationWindowController,
            UIMatchWindowController uiMatchWindowController,
            UIProfileWindowController uiProfileWindowController,
            UIChatWindowController uiChatWindowController)
        {
            _uiLoginWindowController = uiLoginWindowController;
            _uiRegistrationWindowController = uiRegistrationWindowController;
            _uiMatchWindowController = uiMatchWindowController;
            _uiProfileWindowController = uiProfileWindowController;
            _uiChatWindowController = uiChatWindowController;
            
            SetupLogin(uiLoginWindowController);
            SetupRegistration(uiRegistrationWindowController);
            SetupMatch(uiMatchWindowController);
            SetupProfile(uiProfileWindowController);
            SetupChat(uiChatWindowController);
        }

        public void ShowFirstWindow()
        {
            _windowControllers.First?.Value.ShowWindow();
        }
        
        private void SetupLogin(IWindowController windowController)
        {
            _windowControllers.AddLast(windowController);
        }

        private void SetupRegistration(UIRegistrationWindowController uiRegistrationWindowController)
        {
            if (_windowControllers.Last != null)
            {
                _uiLoginWindowController.GoToRegistrationWindow += uiRegistrationWindowController.ShowWindow;
            }

            if (_uiMatchWindowController != null)
            {
                uiRegistrationWindowController.GoToNextWindow += _uiMatchWindowController.ShowWindow;
            }
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
            if (_windowControllers.Last != null)
            {
                _windowControllers.Last.Value.GoToNextWindow += matchWindowController.ShowWindow;
            
                matchWindowController.GoToPreviousWindow += _windowControllers.Last.Value.ShowWindow;
            }
            
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
    }
}
