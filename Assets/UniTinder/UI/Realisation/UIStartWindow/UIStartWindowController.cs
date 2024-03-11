﻿using System;

namespace UniTinder.UI.Realisation
{
    public class UIStartWindowController : IWindowController
    {
        private readonly UIService.UIService _uiService;
        private readonly UIStartWindow _uiStartWindow;
        public Action GoToNext { get; set; }
        public Action GoToPrevious { get; set; }

        public UIStartWindowController(UIService.UIService uiService)
        {
            _uiService = uiService;

            _uiStartWindow = _uiService.Get<UIStartWindow>();
        }
        
        public void ShowWindow()
        {
            _uiService.Show<UIStartWindow>();
        }

        public void HideWindow()
        {
            _uiService.Hide<UIStartWindow>();
        }
    }
}