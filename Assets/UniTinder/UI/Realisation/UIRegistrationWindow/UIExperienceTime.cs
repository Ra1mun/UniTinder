using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIExperienceTime : MonoBehaviour
    {
        [SerializeField] private Button addButton;
        [SerializeField] private Button minusButton;
        [SerializeField] private TMP_Text experienceTimeText;

        private void OnEnable()
        {
            addButton.onClick.AddListener(AddExperienceTime);
            minusButton.onClick.AddListener(MinusExperienceTime);
        }

        private void AddExperienceTime()
        {
            OnExperienceChange(1);
        }

        private void MinusExperienceTime()
        {
            OnExperienceChange(-1);
        }

        private void OnExperienceChange(int value)
        {
            if (experienceTimeText.text.Length > 2)
            {
                experienceTimeText.text = "0";

                return;
            }
            
            var currentTime = Convert.ToInt32(experienceTimeText.text);
            if (currentTime + value < 0)
            {
                return;
            }
            
            experienceTimeText.text = (currentTime + value).ToString();
        }

        public int GetExperienceTime()
        {
            return Convert.ToInt32(experienceTimeText.text);
        }

        private void OnDisable()
        {
            addButton.onClick.RemoveListener(AddExperienceTime);
            minusButton.onClick.RemoveListener(MinusExperienceTime);
        }
    }
}