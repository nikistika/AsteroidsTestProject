using _Project.Scripts.RemoteConfig;
using ConfigKeys;
using Cysharp.Threading.Tasks;
using GameLogic.RemoteConfig;

namespace ConfigData
{
    public class RemoteConfigService
    {
        private IRemoteConfig _remoteConfigFirebase;

        public PoolSizeConfig AsteroidPoolSizeConfig { get; private set; }
        public PoolSizeConfig UfoPoolSizeConfig { get; private set; }
        public PoolSizeConfig MissilePoolSizeConfig { get; private set; }
        public SpawnConfig AsteroidSpawnConfig { get; private set; }
        public SpawnConfig UFoSpawnConfig { get; private set; }

        public async UniTask Initialize()
        {
            _remoteConfigFirebase = new RemoteConfigFirebase();
            await _remoteConfigFirebase.Initialize();

            await GetAllConfigs();
        }

        private async UniTask GetAllConfigs()
        {
            var (asteroidPool, ufoPool, missilePool, asteroidSpawn, ufoSpawn) = await UniTask.WhenAll(
                _remoteConfigFirebase.GetPoolSizeConfig(PoolSizeKeys.AsteroidPoolSizeData),
                _remoteConfigFirebase.GetPoolSizeConfig(PoolSizeKeys.UFOPoolSizeData),
                _remoteConfigFirebase.GetPoolSizeConfig(PoolSizeKeys.MissilePoolSizeData),
                _remoteConfigFirebase.GetSpawnConfig(SpawnKey.AsteroidSpawnData),
                _remoteConfigFirebase.GetSpawnConfig(SpawnKey.UFOSpawnData)
            );

            AsteroidPoolSizeConfig = asteroidPool;
            UfoPoolSizeConfig = ufoPool;
            MissilePoolSizeConfig = missilePool;
            AsteroidSpawnConfig = asteroidSpawn;
            UFoSpawnConfig = ufoSpawn;
        }
    }
}