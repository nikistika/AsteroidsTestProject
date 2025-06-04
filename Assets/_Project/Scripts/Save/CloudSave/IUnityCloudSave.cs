using Cysharp.Threading.Tasks;
using GameLogic.SaveLogic.SaveData;

namespace SaveLogic
{
    public interface IUnityCloudSave
    {
        public UniTask Initialize();
        public UniTask SaveData(SaveConfig saveData);
        public UniTask<SaveConfig> LoadData();

    }
}