using System;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UILoginWindow : UIWindow
    {
        public event Action NextButtonClickEvent;
        public event Action PreviousButtonClickEvent;
        
        [SerializeField] private Button nextButton;
        [SerializeField] private Button previousButton;
        
        public override void Show()
        {
            nextButton.onClick.AddListener(NextButtonClick);
            previousButton.onClick.AddListener(PreviousButtonClick);
        }

        private void NextButtonClick()
        {
            NextButtonClickEvent?.Invoke();
        }

        private void PreviousButtonClick()
        {
            PreviousButtonClickEvent?.Invoke();
        }

        public override void Hide()
        {
            nextButton.onClick.RemoveListener(NextButtonClick);
            previousButton.onClick.RemoveListener(PreviousButtonClick);
        }
    }
}