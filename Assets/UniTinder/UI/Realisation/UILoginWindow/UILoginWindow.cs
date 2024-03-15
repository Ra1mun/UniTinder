using System;
using TMPro;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UILoginWindow : UIWindow
    {
        public event Action<string, string> OnSubmitUserData;
        public event Action OnRegistrationButtonClickEvent;

        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private Button nextButton;
        [SerializeField] private Button registrationButton;

        public override void Show()
        {
            nextButton.onClick.AddListener(NextButtonClick);
            registrationButton.onClick.AddListener(RegistrationButtonClick);
        }

        private void NextButtonClick()
        {
            OnSubmitUserData?.Invoke(emailInputField.text, passwordInputField.text);
        }

        private void RegistrationButtonClick()
        {
            OnRegistrationButtonClickEvent?.Invoke();
        }

        public override void Hide()
        {
            nextButton.onClick.RemoveListener(NextButtonClick);
            registrationButton.onClick.AddListener(RegistrationButtonClick);
        }
    }
}