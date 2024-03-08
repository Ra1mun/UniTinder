using System;

namespace UniTinder.UI.Realisation.UIStartWindow
{
    public class UIStartWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIStartWindow _uiStartWindow;
        public Action GoToNext { get; set; }
        public Action GoToPrevious { get; set; }

        public UIStartWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiStartWindow = _uiService.Get<UIStartWindow>();
        }
        
        public void ShowWindow()
        {
            throw new NotImplementedException();
        }

        public void HideWindow()
        {
            throw new NotImplementedException();
        }
    }
}