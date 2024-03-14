using System;

namespace UniTinder.UI.Realisation
{
    public class UILoginWindowController : IWindowController
    {
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        private readonly UILoginWindow _uiLoginWindow;
        private readonly UIService.UIService _uiService;

        public UILoginWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiLoginWindow = uiService.Get<UILoginWindow>();
        }
        
        public void ShowWindow()
        {
            _uiLoginWindow.NextButtonClickEvent += GoToNext;
            _uiLoginWindow.PreviousButtonClickEvent += GoToPrevious;
            
            _uiService.Show<UILoginWindow>();
        }

        private void GoToNext()
        {
            HideWindow();
            
            GoToNextWindow?.Invoke();
        }

        private void GoToPrevious()
        {
            HideWindow();
            
            GoToPreviousWindow?.Invoke();
        }

        public void HideWindow()
        {
            _uiLoginWindow.NextButtonClickEvent -= GoToNext;
            _uiLoginWindow.PreviousButtonClickEvent -= GoToPrevious;
            
            _uiService.Hide<UILoginWindow>();
        }
    }
}