using System;
using UniTinder.Bootstrap.Interfaces;

namespace UniTinder.UI.UIService
{
    public class InitUIServiceCommand : ICommand
    {
        private readonly UIService _uiService;
        public Action Done { get; set; }
        
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