using System;
using TMPro;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIRegistrationWindow : UIWindow
    {
        public Action GoToNextWindowEvent;
        public event Action SelectInputFieldEvent;
        public event Action<Sprite> OnSubmitAvatar;
        public event Action<Sprite> OnSubmitBackground;
        public event Action<string> OnSubmitNickname;
        public event Action<string> OnSubmitEmail;
        public event Action<string> OnSubmitCity;
        public event Action<string> OnSubmitJob;
        public event Action<int> OnSubmitExperienceTime;

        [Header("Stage first")] 
        [SerializeField] private CanvasGroup firstStage;
        [SerializeField] private Button backgroundChooseButton;
        [SerializeField] private Button avatarChooseButton;
        [SerializeField] private TMP_InputField nicknameInputField;
        [FormerlySerializedAs("backgroundChooseProfile")] [SerializeField] private UIChooseProfileConfiguration backgroundChooseProfileConfiguration;
        [FormerlySerializedAs("avatarChooseProfile")] [SerializeField] private UIChooseProfileConfiguration avatarChooseProfileConfiguration;
        [SerializeField] private Image firstProfileBackground;
        [SerializeField] private Image firstProfileAvatar;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField cityInputField;
        [SerializeField] private Button nextStageButton;

        [Header("Stage second")]
        [SerializeField] private CanvasGroup secondStage;
        [SerializeField] private Button goToFirstStageButton;
        [FormerlySerializedAs("profileBackground")] [SerializeField] private Image secondProfileBackground;
        [FormerlySerializedAs("profileAvatar")] [SerializeField] private Image secondProfileAvatar;
        [SerializeField] private TMP_Text profileNickname;
        [SerializeField] private TMP_InputField jobInputField;
        [FormerlySerializedAs("experience")] [SerializeField] private UIExperienceTime experienceTime;
        [SerializeField] private Button goToThirdStageButton;
        
        [Header("Stage third")]
        [SerializeField] private CanvasGroup thirdStage;
        [SerializeField] private Button goToSecondStageButton;
        [SerializeField] private Button nextButton;
        
        public override void Show()
        {
            nicknameInputField.onSelect.AddListener(SelectInputField);
            emailInputField.onSelect.AddListener(SelectInputField);
            cityInputField.onSelect.AddListener(SelectInputField);
            jobInputField.onSelect.AddListener(SelectInputField);


            backgroundChooseButton.onClick.AddListener(OpenBackgroundChoose);
            avatarChooseButton.onClick.AddListener(OpenAvatarChoose);
            nextStageButton.onClick.AddListener(ShowSecondStage);
            nicknameInputField.onEndEdit.AddListener(NicknameChanged);

            goToFirstStageButton.onClick.AddListener(ShowFirstStage);
            goToThirdStageButton.onClick.AddListener(ShowThirdStage);
            
            goToSecondStageButton.onClick.AddListener(ShowSecondStage);
            nextButton.onClick.AddListener(GoToNextButtonClick);
        }

        public override void Hide()
        {
            OnSubmitBackground?.Invoke(secondProfileBackground.sprite);
            OnSubmitAvatar?.Invoke(secondProfileAvatar.sprite);
            OnSubmitNickname?.Invoke(nicknameInputField.text);
            OnSubmitEmail?.Invoke(emailInputField.text);
            OnSubmitCity?.Invoke(cityInputField.text);
            OnSubmitJob?.Invoke(jobInputField.text);
            OnSubmitExperienceTime?.Invoke(experienceTime.GetExperienceTime());
            
            nicknameInputField.onSelect.RemoveListener(SelectInputField);
            emailInputField.onSelect.RemoveListener(SelectInputField);
            cityInputField.onSelect.RemoveListener(SelectInputField);
            jobInputField.onSelect.RemoveListener(SelectInputField);
            
            backgroundChooseButton.onClick.RemoveListener(OpenBackgroundChoose);
            avatarChooseButton.onClick.RemoveListener(OpenAvatarChoose);
            nextStageButton.onClick.RemoveListener(ShowSecondStage);
            nicknameInputField.onEndEdit.RemoveListener(NicknameChanged);

            goToFirstStageButton.onClick.RemoveListener(ShowFirstStage);
            goToThirdStageButton.onClick.RemoveListener(ShowThirdStage);
            
            goToSecondStageButton.onClick.RemoveListener(ShowSecondStage);
            nextButton.onClick.RemoveListener(GoToNextButtonClick);
        }

        private void SelectInputField(string arg0)
        {
            SelectInputFieldEvent?.Invoke();
        }

        private void OpenBackgroundChoose()
        {
            backgroundChooseProfileConfiguration.gameObject.SetActive(true);
            
            backgroundChooseProfileConfiguration.OnProfileChosen += BackgroundChosen;

            backgroundChooseProfileConfiguration.BackStageButtonClickEvent += CloseProfileConfiguration;
        }

        private void OpenAvatarChoose()
        {
            avatarChooseProfileConfiguration.gameObject.SetActive(true);
            
            avatarChooseProfileConfiguration.OnProfileChosen += AvatarChosen;
            
            avatarChooseProfileConfiguration.BackStageButtonClickEvent += CloseProfileConfiguration;
        }

        private void BackgroundChosen(Sprite sprite)
        {
            firstProfileBackground.sprite = sprite;
            secondProfileBackground.sprite = sprite;
            
            backgroundChooseProfileConfiguration.OnProfileChosen -= BackgroundChosen;
            
            backgroundChooseProfileConfiguration.BackStageButtonClickEvent -= CloseProfileConfiguration;

            CloseProfileConfiguration(backgroundChooseProfileConfiguration.gameObject);
        }
        
        private void AvatarChosen(Sprite sprite)
        {
            firstProfileAvatar.sprite = sprite;
            secondProfileAvatar.sprite = sprite;
            
            avatarChooseProfileConfiguration.OnProfileChosen -= AvatarChosen;
            
            avatarChooseProfileConfiguration.BackStageButtonClickEvent -= CloseProfileConfiguration;
            
            CloseProfileConfiguration(avatarChooseProfileConfiguration.gameObject);
        }
        
        private void CloseProfileConfiguration(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void NicknameChanged(string nickName)
        {
            profileNickname.text = nickName;
        }

        private void ShowFirstStage()
        {
            firstStage.gameObject.SetActive(true);
            
            secondStage.gameObject.SetActive(false);
            
            thirdStage.gameObject.SetActive(false);
        }

        private void ShowSecondStage()
        {
            firstStage.gameObject.SetActive(false);
            
            secondStage.gameObject.SetActive(true);
            
            thirdStage.gameObject.SetActive(false);
        }
        
        private void ShowThirdStage()
        {
            firstStage.gameObject.SetActive(false);
            
            secondStage.gameObject.SetActive(false);
            
            thirdStage.gameObject.SetActive(true);
        }
        
        private void GoToNextButtonClick()
        {
            GoToNextWindowEvent?.Invoke();
        }
    }
}