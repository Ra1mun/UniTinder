using System;
using UniTinder.Bootstrap.Interfaces;

namespace UniTinder.ApplicationStartup.Scripts
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