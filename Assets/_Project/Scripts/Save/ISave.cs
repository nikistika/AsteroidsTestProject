using Cysharp.Threading.Tasks;

namespace _Project.Scripts.Save
{
    public interface ISave
    {
        public UniTask Initialize();
        public UniTask SaveData(SaveData saveData);
        public UniTask<SaveData> GetData();
    }
}