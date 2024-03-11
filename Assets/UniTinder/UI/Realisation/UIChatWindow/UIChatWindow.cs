using System;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIChatWindow : UIWindow
    {
        public Action GoToPreviousWindowEvent;
        
        [SerializeField] private Button previousButton;
        
        public override void Show()
        {
            previousButton.onClick.AddListener(GoToPreviousButtonClick);
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