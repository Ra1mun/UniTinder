using System;

namespace UniTinder.UI.Realisation
{
    public class UIChatWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIChatWindow _uiChatWindow;
        public Action GoToNext { get; set; }
        public Action GoToPrevious { get; set; }

        public UIChatWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiChatWindow = _uiService.Get<UIChatWindow>();
        }

        public void ShowWindow()
        {
            _uiService.Show<UIChatWindow>();
        }

        public void HideWindow()
        {
            _uiService.Hide<UIChatWindow>();
        }
    }
}