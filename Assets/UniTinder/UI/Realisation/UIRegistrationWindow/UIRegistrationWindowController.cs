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
            _uiRegistrationWindow.GoToNextWindowEvent += GoToNext;
            _uiRegistrationWindow.SelectInputFieldEvent += ShowKeyboard;
            
            _uiService.Show<UIRegistrationWindow>();
        }

        private void ShowKeyboard()
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }

        private void HandleBackground(Sprite sprite)
        {
            //Check write data
            
            
        }
        
        private void GoToNext()
        {
            GoToNextWindow?.Invoke();
            
            HideWindow();
        }

        public void HideWindow()
        {
            _uiRegistrationWindow.GoToNextWindowEvent -= GoToNext;
            
            _uiService.Hide<UIRegistrationWindow>();
        }
    }
}