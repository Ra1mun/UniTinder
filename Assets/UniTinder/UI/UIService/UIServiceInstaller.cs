using UniTinder.UI;
using UnityEngine;
using Zenject;

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