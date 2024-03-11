using System;

namespace UniTinder.UI.Realisation
{
    public class UIRegistrationWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIRegistrationWindow _uiRegistrationWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIRegistrationWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiRegistrationWindow = _uiService.Get<UIRegistrationWindow>();
        }

        public void ShowWindow()
        {
            _uiRegistrationWindow.GoToPreviousWindowEvent += GoToPrevious;
            _uiRegistrationWindow.GoToNextWindowEvent += GoToNext;
            
            _uiService.Show<UIRegistrationWindow>();
        }
        
        private void GoToNext()
        {
            GoToNextWindow?.Invoke();
            
            HideWindow();
        }

        private void GoToPrevious()
        {
            GoToPreviousWindow?.Invoke();
            
            HideWindow();
        }

        public void HideWindow()
        {
            _uiRegistrationWindow.GoToPreviousWindowEvent -= GoToPrevious;
            _uiRegistrationWindow.GoToNextWindowEvent -= GoToNext;
            
            _uiService.Hide<UIRegistrationWindow>();
        }
    }
}