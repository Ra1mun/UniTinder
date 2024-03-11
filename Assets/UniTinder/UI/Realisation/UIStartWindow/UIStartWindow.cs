using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniTinder.UI.UIService;
using UnityEngine;
using UnityEngine.UI;

namespace UniTinder.UI.Realisation
{
    public class UIStartWindow : UIWindow
    {
        public Action GoToNextWindowEvent;

        [SerializeField] private Button goToNextButton;

        public override void Show()
        {
            goToNextButton.onClick.AddListener(GoToNextButtonClick);
        }

        private void GoToNextButtonClick()
        {
            GoToNextWindowEvent?.Invoke();
        }

        public override void Hide()
        {
            goToNextButton.onClick.RemoveListener(GoToNextButtonClick);
        }
    }
}
