using GameLogic.SaveLogic.SaveData;
using Zenject;

namespace Installers
{
    public sealed class GlobalInstaller : MonoInstaller
    {
        private SaveController _saveController;
        private SavePlayerPrefs _savePlayerPrefs;

        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            _savePlayerPrefs = new SavePlayerPrefs();
            Container.Bind<SavePlayerPrefs>().FromInstance(_savePlayerPrefs).AsSingle();
            
            _saveController = new SaveController(_savePlayerPrefs);
            Container.Bind<SaveController>().FromInstance(_saveController).AsSingle();
        }
    }
}