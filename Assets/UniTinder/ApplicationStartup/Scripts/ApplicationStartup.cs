using System;
using UniTinder.Bootstrap.Interfaces;
using UniTinder.Camera;
using UniTinder.UI.UIService;
using Zenject;

namespace UniTinder.ApplicationStartup.Scripts
{
    public class ApplicationStartup
    {
        public event Action OnStartInitEvent;
        public event Action OnEndInitEvent;
        
        private readonly IInstantiator _instantiator;
        
        private Bootstrap.Bootstrap _bootstrap;
        
        public ApplicationStartup(
            IInstantiator instantiator)
        {
            _instantiator = instantiator;
            
            StartBootstrap();
        }

        private void StartBootstrap()
        {
            OnStartInitEvent?.Invoke();
            
            _bootstrap = new Bootstrap.Bootstrap();
            
            _bootstrap.AddCommand(_instantiator.Instantiate<InitCameraCommand>());
            _bootstrap.AddCommand(_instantiator.Instantiate<InitUIServiceCommand>());

            _bootstrap.AllCommandsDone += Start;
            
            _bootstrap.StartExecute();
        }

        private void Start()
        {
            _bootstrap.AllCommandsDone -= Start;

            _instantiator.Instantiate<InitApplicationStartupCommand>()
                .Execute();

            OnEndInitEvent?.Invoke();
        }
    }
}