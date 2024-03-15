using System;
using UnityEngine;

namespace UniTinder.UI.Realisation
{
    public class UIChatWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIChatWindow _uiChatWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIChatWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiChatWindow = _uiService.Get<UIChatWindow>();
        }

        public void ShowWindow()
        {
            _uiChatWindow.GoToPreviousWindowEvent += GoToPrevious;
            _uiChatWindow.SendMessageEvent += SendMessage;
            
            _uiService.Show<UIChatWindow>();
        }

        private void SendMessage(string text)
        {
            if (text == null)
            {
                Debug.LogError("Message is null");
                
                return;
            }
        }

        private void GoToPrevious()
        {
            GoToPreviousWindow?.Invoke();
            
            HideWindow();
        }
        
        public void HideWindow()
        {
            _uiChatWindow.GoToPreviousWindowEvent -= GoToPrevious;
            _uiChatWindow.SendMessageEvent -= SendMessage;
            
            _uiService.Hide<UIChatWindow>();
        }
    }
}