using System;
using UniTinder.Network;

namespace UniTinder.UI.Realisation
{
    public class UILoginWindowController : IWindowController
    {
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }
        
        public Action GoToRegistrationWindow { get; set; }

        private readonly UILoginWindow _uiLoginWindow;
        private readonly UIService.UIService _uiService;
        private readonly NetworkService netWork;

        public UILoginWindowController(UIService.UIService uiService, Network.NetworkService netWork)
        {
            _uiService = uiService;
            this.netWork = netWork;
            _uiLoginWindow = uiService.Get<UILoginWindow>();
        }
        
        public void ShowWindow()
        {
            _uiLoginWindow.OnSubmitUserData += HandleUserData;
            _uiLoginWindow.OnRegistrationButtonClickEvent += GoToRegistration;

            _uiService.Show<UILoginWindow>();
        }

        private void HandleUserData(string email, string password)
        {
            netWork.ConnectToServer();
            GoToNext();
        }

        private void GoToNext()
        {
            GoToNextWindow?.Invoke();
            
            HideWindow();
        }

        private void GoToRegistration()
        {
            netWork.ConnectToServer();
            GoToRegistrationWindow?.Invoke();
            
            HideWindow();
        }

        public void HideWindow()
        {
            _uiLoginWindow.OnSubmitUserData -= HandleUserData;
            _uiLoginWindow.OnRegistrationButtonClickEvent -= GoToRegistration;

            _uiService.Hide<UILoginWindow>();
        }
    }
}