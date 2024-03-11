using System;
using UniTinder.Bootstrap.Interfaces;
using UnityEngine;

namespace UniTinder.UI.UIService
{
    public class InitUIServiceCommand : ICommand
    {
        public Action Done { get; set; }
        
        private readonly UIService _uiService;
        
        public InitUIServiceCommand(UIService uiService)
        {
            _uiService = uiService;
        }

        public void Execute()
        {
            _uiService.LoadWindows();
            _uiService.InitWindows();
            
            Done?.Invoke();
        }
    }
}