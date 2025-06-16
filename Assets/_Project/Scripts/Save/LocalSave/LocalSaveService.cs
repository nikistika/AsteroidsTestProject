using _Project.Scripts.Time;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Save.LocalSave
{
    public class LocalSaveService : ISave
    {
        private ISave _localSavePlayerPrefs;

        private readonly ITimeService _timeService;

        public UniTask Initialize()
        {
            _localSavePlayerPrefs = new LocalSavePlayerPrefs();
            return UniTask.CompletedTask;
        }

        public LocalSaveService(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public UniTask SaveData(SaveData saveData)
        {
            saveData.SavingTime = _timeService.GetCurrentTime();
            _localSavePlayerPrefs.SaveData(saveData);
            return UniTask.CompletedTask;
        }

        public async UniTask<SaveData> GetData()
        {
            try
            {
                var saveData = await _localSavePlayerPrefs.GetData();
                return saveData ?? new SaveData();
            }
            catch
            {
                return new SaveData();
            }
        }
    }
}