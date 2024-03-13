using System;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIChooseProfile : MonoBehaviour
    {
        public event Action<Sprite> OnProfileChosen;
        public event Action<GameObject> BackStageButtonClickEvent;
        
        [SerializeField] private UIProfileButton[] uiChooseButtons;
        [SerializeField] private Button backStage;

        private void OnEnable()
        {
            for (int i = 0; i < uiChooseButtons.Length; i++)
            {
                uiChooseButtons[i].OnButtonClickedEvent += ProfileChosen;
            }
            
            backStage.onClick.AddListener(BackStageButtonClick);
        }

        private void ProfileChosen(Image image)
        {
            OnProfileChosen?.Invoke(image.sprite);
        }

        private void BackStageButtonClick()
        {
            BackStageButtonClickEvent?.Invoke(gameObject);
        }

        private void OnDisable()
        {
            for (int i = 0; i < uiChooseButtons.Length; i++)
            {
                uiChooseButtons[i].OnButtonClickedEvent -= ProfileChosen;
            }
            
            backStage.onClick.RemoveListener(BackStageButtonClick);
        }
    }
    
    
}