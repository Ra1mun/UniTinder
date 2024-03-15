using System;

namespace UniTinder.UI.Realisation
{
    public class UILoginWindowController : IWindowController
    {
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }
        
        public Action GoToRegistrationWindow { get; set; }

        private readonly UILoginWindow _uiLoginWindow;
        private readonly UIService.UIService _uiService;

        public UILoginWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiLoginWindow = uiService.Get<UILoginWindow>();
        }
        
        public void ShowWindow()
        {
            _uiLoginWindow.OnSubmitUserData += HandleUserData;
            _uiLoginWindow.OnRegistrationButtonClickEvent += GoToRegistration;

            _uiService.Show<UILoginWindow>();
        }

        private void HandleUserData(string email, string password)
        {
            
            GoToNext();
        }

        private void GoToNext()
        {
            GoToNextWindow?.Invoke();
            
            HideWindow();
        }

        private void GoToRegistration()
        {
            GoToRegistrationWindow?.Invoke();
            
            HideWindow();
        }

        public void HideWindow()
        {
            _uiLoginWindow.OnSubmitUserData -= HandleUserData;
            _uiLoginWindow.OnRegistrationButtonClickEvent -= GoToRegistration;

            _uiService.Hide<UILoginWindow>();
        }
    }
}