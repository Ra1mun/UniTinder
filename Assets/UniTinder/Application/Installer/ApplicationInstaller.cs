using UniTinder.Network;
using UniTinder.UI.UIService;
using UnityEngine;
using Zenject;

namespace UniTinder.Application.Installer
{
    public class ApplicationInstaller : MonoInstaller
    {
        [SerializeField] private string ip;
        [SerializeField] private int port;
    
        public override void InstallBindings()
        {
            var developmentSettings = new DevelopmentSettings(
                ip,
                port);

            Container
                .Bind<DevelopmentSettings>()
                .FromInstance(developmentSettings)
                .AsSingle();
        
            NetworkInstaller.Install(Container);
        
            UIServiceInstaller.Install(Container);

            Container
                .Bind<SceneObjectStorage.Scripts.SceneObjectStorage>()
                .AsSingle();
        
            Container
                .Bind<ApplicationStartup>()
                .AsSingle()
                .NonLazy();
        }
    }
    
    public struct DevelopmentSettings
    {
        public readonly string IP;
        public readonly int Port;
        
        public DevelopmentSettings(string ip, int port)
        {
            IP = ip;
            Port = port;
        }
    }
}