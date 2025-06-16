using _Project.Scripts.Enums.ConfigKeys;
using _Project.Scripts.RemoteConfig.ConfigData;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.RemoteConfig
{
    public class RemoteConfigService : IRemoteConfigService
    {
        private IRemoteConfig _remoteConfigFirebase;

        public PoolSizeConfig AsteroidPoolSizeConfig
        {
            get => _asteroidPoolSizeConfig;
            private set => _asteroidPoolSizeConfig = value;
        }

        PoolSizeConfig IRemoteConfigService.AsteroidPoolSizeConfig
        {
            get => _asteroidPoolSizeConfig;
            set => _asteroidPoolSizeConfig = value;
        }

        public PoolSizeConfig UfoPoolSizeConfig
        {
            get => _ufoPoolSizeConfig;
            private set => _ufoPoolSizeConfig = value;
        }

        PoolSizeConfig IRemoteConfigService.UfoPoolSizeConfig
        {
            get => _ufoPoolSizeConfig;
            set => _ufoPoolSizeConfig = value;
        }

        public PoolSizeConfig MissilePoolSizeConfig
        {
            get => _missilePoolSizeConfig;
            private set => _missilePoolSizeConfig = value;
        }

        PoolSizeConfig IRemoteConfigService.MissilePoolSizeConfig
        {
            get => _missilePoolSizeConfig;
            set => _missilePoolSizeConfig = value;
        }

        public SpawnConfig AsteroidSpawnConfig
        {
            get => _asteroidSpawnConfig;
            private set => _asteroidSpawnConfig = value;
        }

        SpawnConfig IRemoteConfigService.AsteroidSpawnConfig
        {
            get => _asteroidSpawnConfig;
            set => _asteroidSpawnConfig = value;
        }

        public SpawnConfig UFoSpawnConfig
        {
            get => _ufoSpawnConfig;
            private set => _ufoSpawnConfig = value;
        }

        SpawnConfig IRemoteConfigService.UFoSpawnConfig
        {
            get => _ufoSpawnConfig;
            set => _ufoSpawnConfig = value;
        }

        private PoolSizeConfig _asteroidPoolSizeConfig;
        private PoolSizeConfig _ufoPoolSizeConfig;
        private PoolSizeConfig _missilePoolSizeConfig;
        private SpawnConfig _asteroidSpawnConfig;
        private SpawnConfig _ufoSpawnConfig;

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