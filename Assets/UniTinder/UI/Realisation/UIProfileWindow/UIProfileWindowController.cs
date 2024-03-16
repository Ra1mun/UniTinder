using System;


namespace UniTinder.UI.Realisation
{
    public class UIProfileWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIRegistrationWindowController _uiRegistrationWindowController;
        private readonly UIProfileWindow _uiProfileWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        private SessionData.SessionData _sessionData;

        public UIProfileWindowController(UIService.UIService uiService,
            UIRegistrationWindowController uiRegistrationWindowController)
        {
            _uiService = uiService;
            _sessionData = uiRegistrationWindowController.SessionData;

            _uiProfileWindow = _uiService.Get<UIProfileWindow>();
        }

        public void ShowWindow()
        {
            _uiProfileWindow.SetProfileNickname(_sessionData.GetNickname());
            _uiProfileWindow.SetProfileAvatar(_sessionData.GetAvatar());
            _uiProfileWindow.SetProfileBackground(_sessionData.GetBackground());
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