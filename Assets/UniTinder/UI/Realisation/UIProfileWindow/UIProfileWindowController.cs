using System;


namespace UniTinder.UI.Realisation
{
    public class UIProfileWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIProfileWindow _uiProfileWindow;
        public Action GoToNext { get; set; }
        public Action GoToPrevious { get; set; }

        public UIProfileWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiProfileWindow = _uiService.Get<UIProfileWindow>();
        }

        public void ShowWindow()
        {
            _uiService.Show<UIProfileWindow>();
        }

        public void HideWindow()
        {
            _uiService.Hide<UIProfileWindow>();
        }
    }
}