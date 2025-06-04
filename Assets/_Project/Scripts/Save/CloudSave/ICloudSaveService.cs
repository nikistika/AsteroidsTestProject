using Cysharp.Threading.Tasks;
using GameLogic.SaveLogic.SaveData;

namespace SaveLogic
{
    public interface ICloudSaveService
    {
        public UniTask Initialize();
        public  UniTask SaveData(SaveConfig saveData);
        public UniTask<SaveConfig> LoadData();
    }
}