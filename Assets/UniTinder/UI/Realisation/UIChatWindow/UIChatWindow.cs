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
        public Action<string> SendMessageEvent;
        
        [SerializeField] private Button sendMessage;
        [SerializeField] private Button previousButton;
        [SerializeField] private TMP_InputField messageInputField;
        
        public override void Show()
        {
            previousButton.onClick.AddListener(GoToPreviousButtonClick);
            sendMessage.onClick.AddListener(SendMessage);
        }
        public void AddMessage(MessageView view)
        {
            view.transform.SetParent(transform);
        }
        
        private void SendMessage()
        {
            SendMessageEvent?.Invoke(messageInputField.text);
        }
        private void GoToPreviousButtonClick()
        {
            GoToPreviousWindowEvent?.Invoke();
        }
        
        public override void Hide()
        {
            previousButton.onClick.RemoveListener(GoToPreviousButtonClick);
        }
    }
}