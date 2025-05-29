using _Project.Scripts.UI.MenuScene;
using EntryPoints;
using Zenject;

namespace Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuUISpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuEntryPoint>().AsSingle();
        }
    }
}