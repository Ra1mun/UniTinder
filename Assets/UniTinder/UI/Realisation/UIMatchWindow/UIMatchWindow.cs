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
        [SerializeField] private Image profileAvatar;
        [SerializeField] private RectTransform matchUserContainer;
        [SerializeField] private MatchUser[] _matchUsers;

        private MatchUser _currentUser;
        private int _currentUserId;
        
        public override void Show()
        {
            chatButton.onClick.AddListener(ChatButtonClick);
            profileButton.onClick.AddListener(ProfileButtonClick);
        }

        public void SetProfileAvatar(Sprite avatar)
        {
            profileAvatar.sprite = avatar;
        }

        private void ShowNewMatchUser(MatchUser view)
        {
            if (_currentUser != null)
            {
                _currentUser.gameObject.SetActive(false);
            }
            
            view.transform.SetParent(matchUserContainer);
            
            view.OnLikeButtonClickEvent += LikeUser;
            view.OnDislikeButtonClickEvent += DislikeUser;
            
            _currentUser = view;
            view.gameObject.SetActive(true);
        }

        private void LikeUser()
        {
            ShowNewMatchUser(_matchUsers[_currentUserId]);
            _currentUserId++;
        }

        private void DislikeUser()
        {
            ShowNewMatchUser(_matchUsers[_currentUserId]);
            _currentUserId++;
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