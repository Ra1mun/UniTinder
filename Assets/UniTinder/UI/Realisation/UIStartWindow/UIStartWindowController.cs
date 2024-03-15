using System;
using UniTinder.Network;

namespace UniTinder.UI.Realisation
{
    public class UIStartWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly NetworkService _networkService;
        private readonly UIStartWindow _uiStartWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIStartWindowController(UIService.UIService uiService,
            Network.NetworkService networkService)
        {
            _uiService = uiService;
            _networkService = networkService;

            _uiStartWindow = _uiService.Get<UIStartWindow>();
        }
        
        public void ShowWindow()
        {
            _uiStartWindow.GoToNextWindowEvent += GoToNext;
            
            _uiService.Show<UIStartWindow>();
        }
        
        private void GoToNext()
        {
            _networkService.ConnectToServer();
            
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