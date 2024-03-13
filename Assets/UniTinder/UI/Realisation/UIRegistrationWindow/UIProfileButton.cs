using System;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIProfileButton : MonoBehaviour
    {
        public event Action<Image> OnButtonClickedEvent;
        
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            OnButtonClickedEvent?.Invoke(_image);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClick);
        }
    }
}