using _Project.Scripts.Time;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Save.CloudSave
{
    public class UnityCloudSaveService : ISave
    {
        private ISave _cloudSave;

        private readonly ITimeService _timeService;

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

        public async UniTask SaveData(SaveData saveData)
        {
            saveData.SavingTime = _timeService.GetCurrentTime();
            await _cloudSave.SaveData(saveData);
        }

        public async UniTask<SaveData> GetData()
        {
            return await _cloudSave.GetData();
        }
    }
}