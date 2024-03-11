using System;

namespace UniTinder.UI.Realisation
{
    public class UIRegistrationWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIRegistrationWindow _uiRegistrationWindow;
        public Action GoToNext { get; set; }
        public Action GoToPrevious { get; set; }

        public UIRegistrationWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiRegistrationWindow = _uiService.Get<UIRegistrationWindow>();
        }

        public void ShowWindow()
        {
            _uiService.Show<UIRegistrationWindow>();
        }

        public void HideWindow()
        {
            _uiService.Hide<UIRegistrationWindow>();
        }
    }
}