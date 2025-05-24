using ConfigKeys;
using Cysharp.Threading.Tasks;
using GameLogic.RemoteConfig;

namespace ConfigData
{
    public class RemoteConfigController
    {
        private RemoteConfigFirebase _remoteConfigFirebase;

        public PoolSizeData AsteroidPoolSizeData { get; private set; }
        public PoolSizeData UFOPoolSizeData { get; private set; }
        public PoolSizeData MissilePoolSizeData { get; private set; }
        public SpawnData AsteroidSpawnData { get; private set; }
        public SpawnData UFoSpawnData { get; private set; }

        public async UniTask Initialize()
        {
            _remoteConfigFirebase = new RemoteConfigFirebase();
            await _remoteConfigFirebase.Initialize();

            await GetAllConfigs();
        }

        private async UniTask GetAllConfigs()
        {
            AsteroidPoolSizeData = await _remoteConfigFirebase.GetPoolSizeConfig(PoolSizeKeys.AsteroidPoolSizeData);
            UFOPoolSizeData = await _remoteConfigFirebase.GetPoolSizeConfig(PoolSizeKeys.UFOPoolSizeData);
            MissilePoolSizeData = await _remoteConfigFirebase.GetPoolSizeConfig(PoolSizeKeys.MissilePoolSizeData);

            AsteroidSpawnData = await _remoteConfigFirebase.GetSpawnConfig(SpawnKey.AsteroidSpawnData);
            UFoSpawnData = await _remoteConfigFirebase.GetSpawnConfig(SpawnKey.UFOSpawnData);
        }
    }
}