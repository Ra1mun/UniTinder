using System;
using UniTinder.Bootstrap.Interfaces;
using UniTinder.UI.Realisation;

namespace UniTinder.Application
{
    public class InitApplicationStartupCommand : ICommand
    {
        public Action Done { get; set; }
        
        private readonly UIWindowsController _uiWindowsController;

        public InitApplicationStartupCommand(UIWindowsController uiWindowsController)
        {
            _uiWindowsController = uiWindowsController;
        }

        public void Execute()
        {
            _uiWindowsController.ShowFirstWindow();
            
            Done?.Invoke();
        }
    }
}