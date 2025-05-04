using GameLogic.Analytics;
using GameLogic.SaveLogic.SaveData;
using Zenject;

namespace Installers
{
    public sealed class GlobalInstaller : MonoInstaller
    {
        private SaveController _saveController;
        private SavePlayerPrefs _savePlayerPrefs;
        private FirebaseInitializer _firebaseInitializer;
        private AnalyticsController _analyticsController;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            _savePlayerPrefs = new SavePlayerPrefs();
            Container.Bind<SavePlayerPrefs>().FromInstance(_savePlayerPrefs).AsSingle();
            
            _saveController = new SaveController(_savePlayerPrefs);
            Container.Bind<SaveController>().FromInstance(_saveController).AsSingle();
            
            _analyticsController = new AnalyticsController();
            Container.Bind<AnalyticsController>().FromInstance(_analyticsController).AsSingle();
            
            Container.Bind<IInitializable>().FromInstance(_saveController).AsCached();
            Container.Bind<IInitializable>().FromInstance(_analyticsController).AsCached();
            
            Container.BindInterfacesTo<FirebaseInitializer>().AsSingle();

        }
    }
}