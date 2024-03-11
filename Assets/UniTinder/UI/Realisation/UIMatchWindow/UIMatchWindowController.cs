using System;

namespace UniTinder.UI.Realisation
{
    public class UIMatchWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIMatchWindow _uiMatchWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIMatchWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiMatchWindow = _uiService.Get<UIMatchWindow>();
        }

        public void ShowWindow()
        {
            _uiMatchWindow.GoToPreviousWindowEvent += GoToPrevious;
            _uiMatchWindow.GoToNextWindowEvent += GoToNext;
            
            _uiService.Show<UIMatchWindow>();
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
            _uiMatchWindow.GoToPreviousWindowEvent -= GoToPrevious;
            _uiMatchWindow.GoToNextWindowEvent -= GoToNext;
            
            _uiService.Hide<UIMatchWindow>();
        }
    }
}