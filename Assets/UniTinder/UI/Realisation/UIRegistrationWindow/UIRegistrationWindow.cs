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
        public event Action SelectInputFieldEvent;

        public event Action<InterestType> InterestSelectedEvent;
        public event Action<InterestType> InterestDeselectedEvent;
        public event Action<Sprite, Sprite, string, string, string, string, int> OnSubmitUserDataEvent;

        [Header("Stage first")] 
        [SerializeField] private CanvasGroup firstStage;
        [SerializeField] private TMP_Text backgroundChooseButtonText;
        [SerializeField] private TMP_Text avatarChooseButtonText;
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
        [SerializeField] private InterestButton[] uiInterests;
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
            for (int i = 0; i < uiInterests.Length; i++)
            {
                uiInterests[i].OnInterestSelected += InterestSelected;
                uiInterests[i].OnInterestDeselected += InterestDeselected;
            }
            nextButton.onClick.AddListener(GoToNextButtonClick);
        }

        public override void Hide()
        {
            
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
            for (int i = 0; i < uiInterests.Length; i++)
            {
                uiInterests[i].OnInterestSelected -= InterestSelected;
                uiInterests[i].OnInterestDeselected -= InterestDeselected;
            }
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
            
            backgroundChooseButtonText.text = String.Empty;

            CloseProfileConfiguration(backgroundChooseProfileConfiguration.gameObject);
        }
        
        private void AvatarChosen(Sprite sprite)
        {
            firstProfileAvatar.sprite = sprite;
            secondProfileAvatar.sprite = sprite;
            
            avatarChooseProfileConfiguration.OnProfileChosen -= AvatarChosen;
            
            avatarChooseProfileConfiguration.BackStageButtonClickEvent -= CloseProfileConfiguration;
            
            avatarChooseButtonText.text = String.Empty;
            
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

        private void InterestSelected(InterestType type)
        {
            InterestSelectedEvent?.Invoke(type);
        }

        private void InterestDeselected(InterestType type)
        {
            InterestDeselectedEvent?.Invoke(type);
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
            OnSubmitUserDataEvent?.Invoke(
                secondProfileBackground.sprite,
                secondProfileAvatar.sprite,
                nicknameInputField.text,
                emailInputField.text,
                cityInputField.text,
                jobInputField.text,
                experienceTime.GetExperienceTime());
        }
    }
}