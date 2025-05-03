using GameLogic;
using Zenject;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        private BootstrapEntryPoint _bootstrapEntryPoint;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<BootstrapEntryPoint>().AsSingle().NonLazy();
        }
    }
}