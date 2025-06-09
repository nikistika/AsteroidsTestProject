using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Save.CloudSave
{
    public interface ICloudSaveService
    {
        public UniTask Initialize();
        public UniTask SaveData(SaveConfig saveData);
        public UniTask<SaveConfig> LoadData();
    }
}