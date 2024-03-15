using System;
using TMPro;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIChatWindow : UIWindow
    {
        public Action GoToPreviousWindowEvent;
        public Action SendMessageEvent;
        
        [SerializeField] private Button sendMessage;
        [SerializeField] private Button previousButton;
        public override void Show()
        {
            previousButton.onClick.AddListener(GoToPreviousButtonClick);
            sendMessage.onClick.AddListener(SendMessage);
        }
        private void SendMessage()
        {
            SendMessageEvent?.Invoke();
        }
        private void GoToPreviousButtonClick()
        {
            GoToPreviousWindowEvent?.Invoke();
        }
        
        public override void Hide()
        {
            previousButton.onClick.RemoveListener(GoToPreviousButtonClick);
        }
        public void AddMessage(MessageView view)
        {
            view.transform.SetParent(transform);
        }
    }
}