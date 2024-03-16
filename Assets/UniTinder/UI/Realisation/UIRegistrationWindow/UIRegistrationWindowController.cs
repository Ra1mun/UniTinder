using System;
using UnityEngine;

namespace UniTinder.UI.Realisation
{
    public class UIRegistrationWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIRegistrationWindow _uiRegistrationWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIRegistrationWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiRegistrationWindow = _uiService.Get<UIRegistrationWindow>();
        }

        public void ShowWindow()
        {
            _uiRegistrationWindow.OnSubmitUserDataEvent += HandleUserDataEvent;
            
            _uiService.Show<UIRegistrationWindow>();
        }

        private void ShowKeyboard()
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }

        private void HandleUserDataEvent(
            Sprite background,
            Sprite avatar,
            string nickname,
            string email,
            string city,
            string job,
            int experienceTime)
        {
            

            GoToNext();
        }
        
        private void GoToNext()
        {
            GoToNextWindow?.Invoke();
            
            HideWindow();
        }

        public void HideWindow()
        {
            _uiRegistrationWindow.SelectInputFieldEvent -= ShowKeyboard;
            
            _uiRegistrationWindow.OnSubmitUserDataEvent += HandleUserDataEvent;

            _uiService.Hide<UIRegistrationWindow>();
        }
    }
}