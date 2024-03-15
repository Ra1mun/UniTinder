using System;
using UniTinder.Network;
using UnityEngine;

namespace UniTinder.UI.Realisation
{
    public class UIRegistrationWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly NetworkService network;
        private readonly UIRegistrationWindow _uiRegistrationWindow;
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        public UIRegistrationWindowController(UIService.UIService uiService, Network.NetworkService network)
        {
            _uiService = uiService;
            this.network = network;
            _uiRegistrationWindow = _uiService.Get<UIRegistrationWindow>();
        }

        public void ShowWindow()
        {
            _uiRegistrationWindow.GoToNextWindowEvent += GoToNext;
            
            _uiRegistrationWindow.SelectInputFieldEvent += ShowKeyboard;
            
            _uiRegistrationWindow.OnSubmitNickname += HandleNickname;
            _uiRegistrationWindow.OnSubmitEmail += HandleEmail;
            _uiRegistrationWindow.OnSubmitCity += HandleCity;
            _uiRegistrationWindow.OnSubmitJob += HandleJob;
            
            _uiService.Show<UIRegistrationWindow>();
        }

        private void ShowKeyboard()
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }

        private void HandleNickname(string nickname)
        {
            
        }
        
        private void HandleEmail(string email)
        {
            
        }
        
        private void HandleCity(string cityName)
        {
            
        }

        private void HandleJob(string jobName)
        {
            
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
            
            _uiRegistrationWindow.SelectInputFieldEvent -= ShowKeyboard;
            
            _uiRegistrationWindow.OnSubmitNickname -= HandleNickname;
            _uiRegistrationWindow.OnSubmitEmail -= HandleEmail;
            _uiRegistrationWindow.OnSubmitCity -= HandleCity;
            _uiRegistrationWindow.OnSubmitJob -= HandleJob;
            
            _uiService.Hide<UIRegistrationWindow>();
        }
    }
}