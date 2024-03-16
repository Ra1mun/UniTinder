using System;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.UIService
{
    public class InterestButton : MonoBehaviour
    {
        public event Action<InterestType> OnInterestSelected;
        public event Action<InterestType> OnInterestDeselected;
        
        [SerializeField] private InterestType _interestType;
        [SerializeField] private Button button;
        [SerializeField] private Image image;

        private bool _isActive;

        private void OnEnable()
        {
            button.onClick.AddListener(OnInterestValueChanged);
        }

        private void OnInterestValueChanged()
        {
            if (!_isActive)
            {
                OnInterestSelected?.Invoke(_interestType);
                image.color = button.colors.selectedColor;
                _isActive = true;
            }
            else
            {
                OnInterestDeselected?.Invoke(_interestType);
                image.color = button.colors.normalColor;
                _isActive = false;
            }
        }

        private void OnDisable()
        {
            button.onClick.RemoveListener(OnInterestValueChanged);
        }
    }

    public enum InterestType
    {
        Games,
        Football,
        Painting,
        Music,
        Piano,
    }
}
