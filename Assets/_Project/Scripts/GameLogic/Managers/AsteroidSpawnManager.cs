using System.Collections;
using Characters;
using Coroutine;
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
        private CoroutinePerformer _coroutinePerformer;

        public AsteroidSpawnManager(GameOver gameOver, Camera camera, 
            float halfHeightCamera, float halfWidthCamera, AsteroidFactory asteroidFactory, 
            EnemySpawnManagerSO asteroidSpawnData, CoroutinePerformer coroutinePerformer) : 
            base(gameOver, camera, halfHeightCamera, halfWidthCamera)
        {
            _asteroidFactory  = asteroidFactory;
            _asteroidSpawnData = asteroidSpawnData;
            _coroutinePerformer = coroutinePerformer;
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
            _waitRespawnAsteroidRange  = new WaitForSeconds(_asteroidSpawnData.RespawnRange);
            _coroutinePerformer.StartCoroutine(SpawnAsteroidsCoroutine());
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
                    fragment.MoveFragment(i, fragment);
                }
            }
        }

        private void ReturnAsteroid(Asteroid asteroid)
        {
            asteroid.OnReturnAsteroid -= ReturnAsteroid;
            asteroid.OnGetFragments -= SpawnAsteroidFragments;
            _asteroidFactory.ReturnObject(asteroid);
        }

        private IEnumerator SpawnAsteroidsCoroutine()
        {
            while (!_flagGameOver)
            {
                SpawnObject();
                yield return _waitRespawnAsteroidRange;
            }
        }
    }
}