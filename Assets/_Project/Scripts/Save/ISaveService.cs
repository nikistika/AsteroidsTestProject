using System;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Save
{
    public interface ISaveService
    {
        public event Action OnSaveDataChanged;

        public SaveData CurrentSaveData { get; set; }
        public UniTask Initialize();
        public UniTask SaveData(SaveData data);
        public UniTask<SaveData> GetData();

    }
}