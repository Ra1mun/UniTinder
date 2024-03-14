using System;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIMatchWindow : UIWindow
    {
        public Action GoToNextWindowEvent;
        public Action GoToPreviousWindowEvent;
        
        [SerializeField] private Button profileButton;
        [SerializeField] private Button chatButton;
        
        public override void Show()
        {
            profileButton.onClick.AddListener(ProfileButtonClick);
            chatButton.onClick.AddListener(ChatButtonClick);
        }

        private void ProfileButtonClick()
        {
            GoToNextWindowEvent?.Invoke();
        }
        
        private void ChatButtonClick()
        {
            GoToPreviousWindowEvent?.Invoke();
        }
        
        public override void Hide()
        {
            profileButton.onClick.AddListener(ProfileButtonClick);
            chatButton.onClick.AddListener(ChatButtonClick);
        }
    }
}