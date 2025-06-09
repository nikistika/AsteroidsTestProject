using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Save
{
    public interface ISaveService
    {
        public SaveConfig CurrentSaveData { get; set; }
        public UniTask Initialize();
        public UniTask SaveData(SaveConfig data);
        public UniTask<SaveConfig> GetData();

    }
}