using _Project.Scripts.Ads;
using _Project.Scripts.Ads.Unity_ads;
using _Project.Scripts.Analytics;
using _Project.Scripts.Analytics.Firebase;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.IAP;
using _Project.Scripts.RemoteConfig;
using _Project.Scripts.Save.CloudSave;
using _Project.Scripts.Save.LocalSave;
using GameLogic.SaveLogic.SaveData.Save;
using GameLogic.SaveLogic.SaveData.Time;
using LoadingAssets;
using Zenject;

namespace Installers
{
    public sealed class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<LocalSavePlayerPrefs>().AsSingle();
            Container.Bind<IAssetLoader>().To<LocalAssetLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<RemoteConfigService>().AsSingle();
            Container.Bind<ISceneService>().To<SceneService>().AsSingle();

            Container.BindInterfacesAndSelfTo<TimeService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UnityCloudSaveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdsInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<FirebaseInitializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<LocalSaveService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AnalyticsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AdsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<IAPService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveService>().AsSingle();
        }
    }
}