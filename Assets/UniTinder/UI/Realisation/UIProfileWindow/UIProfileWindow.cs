using System;
using System.Collections.Generic;
using TMPro;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIProfileWindow : UIWindow
    {
        public Action GoToPreviousWindowEvent;
        
        [SerializeField] private Button previousButton;
        [SerializeField] private Image profileBackground;
        [SerializeField] private Image profileAvatar;
        [SerializeField] private TMP_Text profileNickname;
        [SerializeField] private TMP_Text profileJob;
        [SerializeField] private TMP_Text profileExperienceTime;
        [SerializeField] private GridLayoutGroup interestContainer;

        private readonly List<UIInterest> _interests = new List<UIInterest>();
        
        public override void Show()
        {
            previousButton.onClick.AddListener(GoToPreviousButtonClick);
        }

        public void SetProfileBackground(Sprite background)
        {
            profileBackground.sprite = background;
        }
        
        public void SetProfileAvatar(Sprite avatar)
        {
            profileAvatar.sprite = avatar;
        }

        public void SetProfileNickname(string nickname)
        {
            profileNickname.text = nickname;
        }

        public void SetProfileJob(string job)
        {
            profileJob.text = job;
        }

        public void SetProfile(string experienceTime)
        {
            profileExperienceTime.text = experienceTime;
        }

        public void AddInterest(UIInterest prefab)
        {
            prefab.transform.SetParent(interestContainer.transform);
            
            _interests.Add(prefab);
        }

        public void RemoveAllInterest()
        {
            for (int i = 0; i < _interests.Count; i++)
            {
                Destroy(_interests[i].gameObject);
            }

            _interests.Clear();
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