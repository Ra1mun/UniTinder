using System;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIMatchWindow : UIWindow
    {
        public event Action OnChatButtonClickEvent;
        public event Action OnProfileButtonClickEvent;
        
        [SerializeField] private Button chatButton;
        [SerializeField] private Button profileButton;
        [SerializeField] private RectTransform matchUserContainer;

        private MatchUser _currentUser;
        
        public override void Show()
        {
            chatButton.onClick.AddListener(ChatButtonClick);
            profileButton.onClick.AddListener(ProfileButtonClick);
        }

        public void ShowNewMatchUser(MatchUser view)
        {
            if (_currentUser != null)
            {
                _currentUser.gameObject.SetActive(false);
            }
            
            view.transform.SetParent(matchUserContainer);
            _currentUser = view;
            view.gameObject.SetActive(true);
        }

        private void ChatButtonClick()
        {
            OnChatButtonClickEvent?.Invoke();
        }
        
        private void ProfileButtonClick()
        {
            OnProfileButtonClickEvent?.Invoke();
        }
        
        public override void Hide()
        {
            chatButton.onClick.RemoveListener(ChatButtonClick);
            profileButton.onClick.RemoveListener(ProfileButtonClick);
        }
    }
}