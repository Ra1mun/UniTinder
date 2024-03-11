using System;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIRegistrationWindow : UIWindow
    {
        public Action GoToNextWindowEvent;
        public Action GoToPreviousWindowEvent;
        
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        
        public override void Show()
        {
            nextButton.onClick.AddListener(GoToNextButtonClick);
            previousButton.onClick.AddListener(GoToPreviousButtonClick);
        }

        private void GoToNextButtonClick()
        {
            GoToNextWindowEvent?.Invoke();
        }
        
        private void GoToPreviousButtonClick()
        {
            GoToPreviousWindowEvent?.Invoke();
        }
        
        public override void Hide()
        {
            nextButton.onClick.RemoveListener(GoToNextButtonClick);
            previousButton.onClick.RemoveListener(GoToPreviousButtonClick);
        }
    }
}