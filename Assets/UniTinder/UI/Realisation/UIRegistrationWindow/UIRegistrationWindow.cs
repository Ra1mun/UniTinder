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
        public Action<Sprite> OnSubmitAvatar;
        public Action<Sprite> OnSubmitBackground;
        public Action<string> OnSubmitNickname;
        public Action<string> OnSubmitEmail;
        public Action<string> OnSubmitCity;
        public Action<string> OnSubmitBirthday;
        public Action<string> OnSubmitJob;
        public Action<int> OnSubmitExperienceTime;

        [Header("Stage first")] 
        [SerializeField] private CanvasGroup firstStage;
        [SerializeField] private Button backgroundChooseButton;
        [SerializeField] private Button avatarChooseButton;
        [SerializeField] private TMP_InputField nicknameInputField;
        [SerializeField] private UIChooseProfile backgroundChooseProfile;
        [SerializeField] private UIChooseProfile avatarChooseProfile;
        [SerializeField] private Image firstProfileBackground;
        [SerializeField] private Image firstProfileAvatar;
        [SerializeField] private DateInputField[] dateInputFields;
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField cityInputField;
        [SerializeField] private Button nextStageButton;

        [Header("Stage second")]
        [SerializeField] private CanvasGroup secondStage;
        [SerializeField] private Button previousStageButton;
        [FormerlySerializedAs("profileBackground")] [SerializeField] private Image secondProfileBackground;
        [FormerlySerializedAs("profileAvatar")] [SerializeField] private Image secondProfileAvatar;
        [SerializeField] private TMP_Text profileNickname;
        [SerializeField] private TMP_InputField jobInputField;
        [SerializeField] private UIExperience experience;
        [SerializeField] private Button nextButton;
        
        public override void Show()
        {
            backgroundChooseButton.onClick.AddListener(OpenBackgroundChoose);
            avatarChooseButton.onClick.AddListener(OpenAvatarChoose);
            nextStageButton.onClick.AddListener(ShowNextStage);
            nicknameInputField.onEndEdit.AddListener(NicknameChanged);
            
            previousStageButton.onClick.AddListener(ShowPreviousStage);
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
            OnSubmitExperienceTime?.Invoke(experience.GetExperienceTime());
        }

        private void OpenBackgroundChoose()
        {
            backgroundChooseProfile.gameObject.SetActive(true);
            
            backgroundChooseProfile.OnProfileChosen += BackgroundChosen;

            backgroundChooseProfile.BackStageButtonClickEvent += CloseProfileChoose;
        }

        private void OpenAvatarChoose()
        {
            avatarChooseProfile.gameObject.SetActive(true);
            
            avatarChooseProfile.OnProfileChosen += AvatarChosen;
            
            avatarChooseProfile.BackStageButtonClickEvent += CloseProfileChoose;
        }

        private void BackgroundChosen(Sprite sprite)
        {
            firstProfileBackground.sprite = sprite;
            secondProfileBackground.sprite = sprite;
            
            backgroundChooseProfile.OnProfileChosen -= BackgroundChosen;
            
            backgroundChooseProfile.BackStageButtonClickEvent -= CloseProfileChoose;

            CloseProfileChoose(backgroundChooseProfile.gameObject);
        }
        
        private void AvatarChosen(Sprite sprite)
        {
            firstProfileAvatar.sprite = sprite;
            secondProfileAvatar.sprite = sprite;
            
            avatarChooseProfile.OnProfileChosen -= AvatarChosen;
            
            avatarChooseProfile.BackStageButtonClickEvent -= CloseProfileChoose;
            
            CloseProfileChoose(avatarChooseProfile.gameObject);
        }
        
        private void CloseProfileChoose(GameObject obj)
        {
            obj.SetActive(false);
        }

        private void NicknameChanged(string nickName)
        {
            profileNickname.text = nickName;
        }

        private void ShowNextStage()
        {
            firstStage.gameObject.SetActive(false);
            
            secondStage.gameObject.SetActive(true);
        }

        private void ShowPreviousStage()
        {
            firstStage.gameObject.SetActive(true);
            
            secondStage.gameObject.SetActive(false);
        }
        
        private void GoToNextButtonClick()
        {
            GoToNextWindowEvent?.Invoke();
        }
    }
    
    [Serializable]
    public class DateInputField
    {
        [SerializeField] private TMP_InputField dateInputField;
        [SerializeField] private Date date;
    }

    public enum Date
    {
        Days,
        Month,
        Year
    }
}