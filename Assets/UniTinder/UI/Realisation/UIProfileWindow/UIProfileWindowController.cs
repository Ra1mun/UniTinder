using System;


namespace UniTinder.UI.Realisation
{
    public class UIProfileWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIProfileWindow _uiProfileWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIProfileWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiProfileWindow = _uiService.Get<UIProfileWindow>();
        }

        public void ShowWindow()
        {
            _uiProfileWindow.GoToPreviousWindowEvent += GoToPrevious;

            _uiService.Show<UIProfileWindow>();
        }

        private void GoToPrevious()
        {
            GoToPreviousWindow?.Invoke();
            
            HideWindow();
        }

        public void HideWindow()
        {
            _uiProfileWindow.GoToPreviousWindowEvent -= GoToPrevious;
            _uiProfileWindow.RemoveAllInterest();

            _uiService.Hide<UIProfileWindow>();
        }
    }
}