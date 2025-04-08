using System.Collections;
using Characters;
using Factories;
using GameLogic;
using Player;
using UnityEngine;

namespace Managers
{
    public class AsteroidSpawmManager : BaseSpawnManager<Asteroid>
    {
        private WaitForSeconds _waitRespawnAsteroidRange;
        
        [SerializeField] private AsteroidFactory _asteroidFactory;
        [SerializeField] private DataSpaceShip _dataSpaceShip;
        [SerializeField] private float _respawnAsteroidRange = 3;
        
        private void Start()
        {
            StartCoroutine(SpawnAsteroidsCoroutine());
        }

        protected override Asteroid SpawnObject()
        {
            var asteroid = _asteroidFactory.SpawnObject();
            asteroid.OnReturnAsteroid += ReturnAsteroid;
            asteroid.OnGetFragments += SpawnAsteroidFragments;
            return asteroid;
        }

        protected override void Initialization()
        {
            _waitRespawnAsteroidRange = new WaitForSeconds(_respawnAsteroidRange);
            _asteroidFactory.Construct(_dataSpaceShip, _camera, _halfHeightCamera, _halfWidthCamera);
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