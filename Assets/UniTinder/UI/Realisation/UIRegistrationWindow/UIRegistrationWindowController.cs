using System;
using System.Collections.Generic;
using UniTinder.UI.UIService;
using UniTinder.Network;
using UnityEngine;

namespace UniTinder.UI.Realisation
{
    public class UIRegistrationWindowController : IWindowController
    {
        public Action GoToNextWindow { get; set; }
        public Action GoToPreviousWindow { get; set; }

        private readonly UIService.UIService _uiService;
        private readonly NetworkService network;
        private readonly UIRegistrationWindow _uiRegistrationWindow;
        private readonly List<InterestType> _selectedInterest = new List<InterestType>();

        public UIRegistrationWindowController(UIService.UIService uiService, NetworkService network)
        {
            _uiService = uiService;
            this.network = network;
            _uiRegistrationWindow = _uiService.Get<UIRegistrationWindow>();
        }

        public void ShowWindow()
        {
            _uiRegistrationWindow.InterestSelectedEvent += AddInterest;
            _uiRegistrationWindow.InterestDeselectedEvent += RemoveInterest;
            _uiRegistrationWindow.OnSubmitUserDataEvent += HandleUserDataEvent;

            ClientHandle.GoToMainWindow += GoToNext;

            _uiService.Show<UIRegistrationWindow>();
        }

        private void ShowKeyboard()
        {
            TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
        }

        private void AddInterest(InterestType type)
        {
            if (!_selectedInterest.Contains(type))
            {
                _selectedInterest.Add(type);
            }
        }

        private void RemoveInterest(InterestType type)
        {
            if (_selectedInterest.Contains(type))
            {
                _selectedInterest.Remove(type);
            }
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

            network.RegisterNewUser(nickname, email, city, job, experienceTime);
        }
        
        private void GoToNext(bool check)
        {
            if (check)
            {
                GoToNextWindow?.Invoke();

                HideWindow();
            }

        }

        public void HideWindow()
        {
            _uiRegistrationWindow.SelectInputFieldEvent -= ShowKeyboard;
            
            _uiRegistrationWindow.OnSubmitUserDataEvent -= HandleUserDataEvent;

            ClientHandle.GoToMainWindow -= GoToNext;

            _uiService.Hide<UIRegistrationWindow>();
        }
    }
}