using _Project.Scripts.EntryPoints;
using Zenject;

namespace _Project.Scripts.Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapEntryPoint>().AsSingle();
        }
    }
}