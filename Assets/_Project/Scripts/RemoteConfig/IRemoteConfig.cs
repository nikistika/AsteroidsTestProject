using _Project.Scripts.Enums.ConfigKeys;
using _Project.Scripts.RemoteConfig.ConfigData;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.RemoteConfig
{
    public interface IRemoteConfig
    {
        public UniTask Initialize();
        public UniTask<PoolSizeConfig> GetPoolSizeConfig(PoolSizeKeys key);
        public UniTask<SpawnConfig> GetSpawnConfig(SpawnKey key);
    }
}