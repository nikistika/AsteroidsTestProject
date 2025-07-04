using System;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.GameLogic.Factories;
using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;

namespace _Project.Scripts.GameLogic.Services.Spawners
{
    public class AsteroidSpawner : BaseSpawner<Asteroid>
    {
        private readonly AsteroidFactory _asteroidFactory;
        private readonly IRemoteConfigService _remoteConfigService;

        public AsteroidSpawner(
            GameState gameState,
            ScreenSize screenSize,
            AsteroidFactory asteroidFactory,
            IRemoteConfigService remoteConfigService) :
            base(gameState, screenSize)
        {
            _asteroidFactory = asteroidFactory;
            _remoteConfigService = remoteConfigService;
        }

        private Asteroid SpawnObject()
        {
            var asteroid = _asteroidFactory.SpawnObject();
            asteroid.OnReturnAsteroid += ReturnAsteroid;
            asteroid.OnGetFragments += SpawnAsteroidFragments;
            return asteroid;
        }

        protected override async UniTask Initialize()
        {
            await SpawnAsteroids();
        }

        protected override async UniTask GameContinue()
        {
            FlagGameOver = false;
            await SpawnAsteroids();
        }

        private void SpawnAsteroidFragments(int quantity, Asteroid objectParent)
        {
            float parentScale = objectParent.transform.localScale.x;

            for (var i = 1; i <= quantity; i++)
            {
                var fragment = SpawnObject();
                if (fragment != null)
                {
                    fragment.IsObjectParent(false);
                    fragment.transform.position = objectParent.transform.position;
                    fragment.transform.localScale /= 2;
                    fragment.MoveFragment(fragment);
                }
            }
        }

        private void ReturnAsteroid(Asteroid asteroid)
        {
            asteroid.OnReturnAsteroid -= ReturnAsteroid;
            asteroid.OnGetFragments -= SpawnAsteroidFragments;
            _asteroidFactory.ReturnObject(asteroid);
        }

        private async UniTask SpawnAsteroids()
        {
            while (!FlagGameOver)
            {
                SpawnObject();
                await UniTask.Delay(TimeSpan.FromSeconds(_remoteConfigService.AsteroidSpawnConfig.RespawnRange));
            }
        }
    }
}