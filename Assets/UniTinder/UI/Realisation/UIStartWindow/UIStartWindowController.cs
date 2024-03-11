using System;

namespace UniTinder.UI.Realisation
{
    public class UIStartWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIStartWindow _uiStartWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIStartWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiStartWindow = _uiService.Get<UIStartWindow>();
        }
        
        public void ShowWindow()
        {
            _uiStartWindow.GoToNextWindowEvent += GoToNext;
            
            _uiService.Show<UIStartWindow>();
        }
        
        private void GoToNext()
        {
            GoToNextWindow?.Invoke();
            
            HideWindow();
        }

        public void HideWindow()
        {
            _uiStartWindow.GoToNextWindowEvent -= GoToNext;
            
            _uiService.Hide<UIStartWindow>();
        }
    }
}