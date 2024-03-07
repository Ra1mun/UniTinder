using UniTinder.Camera;
using UniTinder.UI.UIService;
using Zenject;

namespace UniTinder.ApplicationStartup.Scripts
{
    public class ApplicationStartup
    {
        private readonly IInstantiator _instantiator;

        public ApplicationStartup(IInstantiator instantiator)
        {
            _instantiator = instantiator;
            
            StartBootstrap();
        }

        private void StartBootstrap()
        {
            var bootstrap = new Bootstrap.Bootstrap();
            
            bootstrap.AddCommand(_instantiator.Instantiate<InitCameraCommand>());
            bootstrap.AddCommand(_instantiator.Instantiate<InitUIServiceCommand>());
            
            bootstrap.StartExecute();
        }
    }
}