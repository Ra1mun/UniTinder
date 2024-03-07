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
        }
    }
}