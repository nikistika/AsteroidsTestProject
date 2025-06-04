using SaveLogic;
using ConfigData;
using GameLogic.Ads;
using GameLogic.Ads.Unity_ads;
using GameLogic.Analytics;
using GameLogic.SaveLogic.SaveData;
using GameLogic.SaveLogic.SaveData.Time;
using IAP;
using LoadingAssets;
using Service;
using Zenject;

namespace Installers
{
    public sealed class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LocalSavePlayerPrefs>().AsSingle();
            Container.Bind<IAssetLoader>().To<LocalAssetLoader>().AsSingle();
            Container.Bind<RemoteConfigService>().AsSingle();
            Container.Bind<ISceneService>().To<SceneService>().AsSingle();

            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityCloudSaveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdsInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<LocalLocalSaveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<IAPService>().AsSingle();
        }
    }
}