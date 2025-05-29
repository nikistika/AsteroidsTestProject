using ConfigData;
using GameLogic.Ads;
using GameLogic.Ads.Unity_ads;
using GameLogic.Analytics;
using GameLogic.SaveLogic.SaveData;
using IAP;
using LoadingAssets;
using Managers;
using Zenject;

namespace Installers
{
    public sealed class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<SavePlayerPrefs>().AsSingle();
            Container.Bind<IAssetLoader>().To<LocalAssetLoader>().AsSingle();
            Container.Bind<RemoteConfigService>().AsSingle();
            Container.Bind<ISceneService>().To<SceneService>().AsSingle();

            Container.BindInterfacesAndSelfTo<AdsInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveController>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsController>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdsController>().AsSingle();
            Container.BindInterfacesAndSelfTo<IAPService>().AsSingle();
        }
    }
}