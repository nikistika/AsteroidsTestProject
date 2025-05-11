using GameLogic.Analytics;
using GameLogic.SaveLogic.SaveData;
using LoadingAssets;
using Zenject;

namespace Installers
{
    public sealed class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SavePlayerPrefs>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsController>().AsSingle();
            Container.Bind<IAssetLoader>().To<LocalAssetLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseInitializer>().AsSingle();
        }
    }
}