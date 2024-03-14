using UniTinder.UI.Realisation;
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
                .Bind<UIService>()
                .AsSingle();

            Container
                .Bind<UIStartWindowController>()
                .AsSingle();
            
            Container
                .Bind<UILoginWindowController>()
                .AsSingle();

            Container
                .Bind<UIRegistrationWindowController>()
                .AsSingle();

            Container
                .Bind<UIProfileWindowController>()
                .AsSingle();

            Container
                .Bind<UIMatchWindowController>()
                .AsSingle();

            Container
                .Bind<UIChatWindowController>()
                .AsSingle();

            Container
                .Bind<UIWindowsController>()
                .AsSingle();
        }
    }
}