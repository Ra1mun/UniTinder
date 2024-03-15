using Zenject;

namespace UniTinder.Network
{
    public class NetworkInstaller : Installer<NetworkInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<NetworkService>()
                .AsSingle();
        }
    }
}