using UniTinder.UI.Realisation;
using UniTinder.UI.Realisation.UIStartWindow;
using Zenject;

namespace UniTinder.UI.UIService
{
    public class UIServiceInstaller : Installer<UIServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<UIRoot>()
                .FromComponentInNewPrefabResource("UIRoot")
                .AsSingle();

            Container
                .Bind<UIStartWindowController>()
                .AsSingle();

            Container
                .Bind<UIWindowsController>()
                .AsSingle();

            Container
                .Bind<UIService>()
                .AsSingle();
        }
    }
}