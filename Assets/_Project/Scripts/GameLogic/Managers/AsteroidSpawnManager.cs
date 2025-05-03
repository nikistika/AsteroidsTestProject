using System;
using Characters;
using Cysharp.Threading.Tasks;
using Factories;
using GameLogic;
using SciptableObjects;
using UnityEngine;

namespace Managers
{
    public class AsteroidSpawnManager : BaseSpawnManager<Asteroid>
    {
        private WaitForSeconds _waitRespawnAsteroidRange;
        private AsteroidFactory _asteroidFactory;
        private EnemySpawnManagerSO _asteroidSpawnData;

        public AsteroidSpawnManager(GameOver gameOver,
            ScreenSize screenSize, AsteroidFactory asteroidFactory,
            EnemySpawnManagerSO asteroidSpawnData) :
            base(gameOver, screenSize)
        {
            _asteroidFactory = asteroidFactory;
            _asteroidSpawnData = asteroidSpawnData;
        }

        public override Asteroid SpawnObject()
        {
            var asteroid = _asteroidFactory.SpawnObject();
            asteroid.OnReturnAsteroid += ReturnAsteroid;
            asteroid.OnGetFragments += SpawnAsteroidFragments;
            return asteroid;
        }

        protected override void Initialize()
        {
            SpawnAsteroidsAsync().Forget();
        }

        private void SpawnAsteroidFragments(int quantity, Asteroid objectParent)
        {
            for (var i = 1; i <= quantity; i++)
            {
                var fragment = SpawnObject();
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

        private async UniTask SpawnAsteroidsAsync()
        {
            while (!FlagGameOver)
            {
                SpawnObject();
                await UniTask.Delay(TimeSpan.FromSeconds(_asteroidSpawnData.RespawnRange));
            }
        }
    }
}