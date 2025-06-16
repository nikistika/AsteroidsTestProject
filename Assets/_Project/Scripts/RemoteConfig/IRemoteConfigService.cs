using _Project.Scripts.RemoteConfig.ConfigData;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.RemoteConfig
{
    public interface IRemoteConfigService
    {
        public PoolSizeConfig AsteroidPoolSizeConfig { get; set; }
        public PoolSizeConfig UfoPoolSizeConfig { get; set; }
        public PoolSizeConfig MissilePoolSizeConfig { get; set; }
        public SpawnConfig AsteroidSpawnConfig { get; set; }
        public SpawnConfig UFoSpawnConfig { get; set; }
        public UniTask Initialize();
    }
}