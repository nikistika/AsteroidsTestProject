using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;
using GameLogic.SaveLogic.SaveData.Time;

namespace _Project.Scripts.Save.CloudSave
{
    public class UnityCloudSaveService : ICloudSaveService
    {
        private IUnityCloudSave _cloudSave;

        private ITimeService _timeService;

        public UnityCloudSaveService(
            ITimeService timeService)
        {
            _timeService = timeService;
        }

        public async UniTask Initialize()
        {
            _cloudSave = new UnityCloudSave();
            await _cloudSave.Initialize();
        }

        public async UniTask SaveData(SaveConfig saveData)
        {
            saveData.SavingTime = _timeService.GetCurrentTime();
            await _cloudSave.SaveData(saveData);
        }

        public async UniTask<SaveConfig> LoadData()
        {
            return await _cloudSave.LoadData();
        }
    }
}