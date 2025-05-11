using System;
using Characters;
using Cysharp.Threading.Tasks;
using Factories;
using GameLogic;
using GameLogic.Enums;
using SciptableObjects;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class AsteroidSpawner : BaseSpawner<Asteroid>
    {
        private WaitForSeconds _waitRespawnAsteroidRange;
        private AsteroidFactory _asteroidFactory;
        private EnemySpawnManagerSO _asteroidSpawnData;

        public AsteroidSpawner(
            GameOver gameOver,
            ScreenSize screenSize, 
            AsteroidFactory asteroidFactory,
            [Inject (Id = GameInstallerIDs.AsteroidSizeData)] EnemySpawnManagerSO asteroidSpawnData) :
            base(gameOver, screenSize)
        {
            _asteroidFactory = asteroidFactory;
            _asteroidSpawnData = asteroidSpawnData;
        }

        public async UniTask<Asteroid> SpawnObject()
        {
            var asteroid = await _asteroidFactory.SpawnObject();
            asteroid.OnReturnAsteroid += ReturnAsteroid;
            asteroid.OnGetFragments += SpawnAsteroidFragments;
            return asteroid;
        }

        protected override async UniTask Initialize()
        {
            SpawnAsteroids().Forget();
            await UniTask.CompletedTask;
        }

        private async void SpawnAsteroidFragments(int quantity, Asteroid objectParent)
        {
            for (var i = 1; i <= quantity; i++)
            {
                var fragment =  await SpawnObject();
                if (fragment != null)
                {
                    fragment.IsObjectParent(false);
                    fragment.transform.position = objectParent.transform.position;
                    fragment.transform.localScale = objectParent.transform.localScale / 2;
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
                await UniTask.Delay(TimeSpan.FromSeconds(_asteroidSpawnData.RespawnRange));
            }
        }
    }
}