using System;

namespace UniTinder.UI.Realisation
{
    public class UIMatchWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIMatchWindow _uiMatchWindow;
        public Action GoToNext { get; set; }
        public Action GoToPrevious { get; set; }

        public UIMatchWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiMatchWindow = _uiService.Get<UIMatchWindow>();
        }

        public void ShowWindow()
        {
            _uiService.Show<UIMatchWindow>();
        }

        public void HideWindow()
        {
            _uiService.Hide<UIMatchWindow>();
        }
    }
}