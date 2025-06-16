using _Project.Scripts.EntryPoints;
using _Project.Scripts.UI.MenuScene;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class MenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MenuUIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<MenuEntryPoint>().AsSingle();
        }
    }
}