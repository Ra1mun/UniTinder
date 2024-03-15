using System;

namespace UniTinder.UI.Realisation
{
    public class UIMatchWindowController : IWindowController
    {
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }
        
        private readonly UIService.UIService _uiService;
        private readonly UIMatchWindow _uiMatchWindow;

        public Action GoToChatWindow { get; set; }
        
        public Action GoToProfileWindow { get; set; }

        public UIMatchWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiMatchWindow = _uiService.Get<UIMatchWindow>();
        }

        public void ShowWindow()
        {
            _uiMatchWindow.OnChatButtonClickEvent += GoToChat;
            _uiMatchWindow.OnProfileButtonClickEvent += GoToProfile;
            
            _uiService.Show<UIMatchWindow>();
        }

        private void GoToChat()
        {
            GoToChatWindow?.Invoke();
            
            HideWindow();
        }

        private void GoToProfile()
        {
            GoToProfileWindow?.Invoke();
            
            HideWindow();
        }

        public void HideWindow()
        {
            _uiMatchWindow.OnChatButtonClickEvent += GoToChat;
            _uiMatchWindow.OnProfileButtonClickEvent += GoToProfile;
            
            _uiService.Hide<UIMatchWindow>();
        }
    }
}