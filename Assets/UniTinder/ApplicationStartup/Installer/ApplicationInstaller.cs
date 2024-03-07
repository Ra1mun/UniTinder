using UniTinder.ApplicationStartup.Scripts;
using UnityEngine;
using Zenject;

public class ApplicationInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .Bind<ApplicationStartup>()
            .AsSingle();
    }
}