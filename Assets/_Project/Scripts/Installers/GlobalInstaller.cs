using _Project.Scripts.Addressable;
using _Project.Scripts.Ads;
using _Project.Scripts.Ads.Unity_ads;
using _Project.Scripts.Analytics;
using _Project.Scripts.Analytics.Firebase;
using _Project.Scripts.Enums;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.IAP;
using _Project.Scripts.RemoteConfig;
using _Project.Scripts.Save;
using _Project.Scripts.Save.CloudSave;
using _Project.Scripts.Save.LocalSave;
using _Project.Scripts.Time;
using Zenject;

namespace _Project.Scripts.Installers
{
    public sealed class GlobalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<AssetLoader>().AsSingle();
            Container.BindInterfacesTo<RemoteConfigService>().AsSingle();
            Container.BindInterfacesTo<SceneService>().AsSingle();

            Container.BindInterfacesTo<TimeService>().AsSingle();
            Container.BindInterfacesTo<AdsInitializer>().AsSingle();
            Container.BindInterfacesTo<FirebaseInitializer>().AsSingle();
            Container.BindInterfacesTo<AnalyticsService>().AsSingle();
            Container.BindInterfacesTo<AdsService>().AsSingle();
            Container.BindInterfacesTo<IAPService>().AsSingle();


            Container.Bind<ISave>().WithId(GlobalInstallerIDs.SaveCloudService).To<UnityCloudSaveService>().AsCached();
            Container.Bind<ISave>().WithId(GlobalInstallerIDs.SaveLocalService).To<LocalSaveService>().AsCached();

            Container.BindInterfacesTo<SaveService>().AsSingle();
        }
    }
}