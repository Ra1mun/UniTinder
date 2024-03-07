using UniTinder.ApplicationStartup.Scripts;
using UniTinder.SceneObjectStorage.Scripts;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        UIServiceInstaller.Install(Container);
        
        Container
            .Bind<SceneObjectStorage>()
            .AsSingle();
        
        Container
            .Bind<ApplicationStartup>()
            .AsSingle()
            .NonLazy();
    }
}