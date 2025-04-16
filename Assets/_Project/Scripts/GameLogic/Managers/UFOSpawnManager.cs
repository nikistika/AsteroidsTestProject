using System.Collections;
using Characters;
using Coroutine;
using Factories;
using GameLogic;
using SciptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Managers
{
    public class UFOSpawnManager : BaseSpawnManager<UFO>
    {
        private WaitForSeconds _waitRespawnUFORange;
        private UFOFactory _ufoFactory;
        private EnemySpawnManagerSO _ufoSpawnData;
        private CoroutinePerformer _coroutinePerformer;

        public UFOSpawnManager(GameOver gameOver, Camera camera, 
            float halfHeightCamera, float halfWidthCamera, UFOFactory ufoFactory, 
            EnemySpawnManagerSO ufoSpawnData, CoroutinePerformer coroutinePerformer, ScoreManager scoreManager) : 
            base(gameOver, camera, halfHeightCamera, halfWidthCamera, scoreManager)
        {
            _ufoFactory = ufoFactory;
            _ufoSpawnData = ufoSpawnData;
            _coroutinePerformer = coroutinePerformer;
        }

        public override UFO SpawnObject()
        {
            var ufo = _ufoFactory.SpawnObject();
            ufo.OnReturnUFO += ReturnUFO;
            return ufo;
        }

        protected override void Initialize()
        {
            _waitRespawnUFORange = new WaitForSeconds(_ufoSpawnData.RespawnRange);
            _coroutinePerformer.StartCoroutine(SpawnUFOCoroutine());
        }

        private void ReturnUFO(UFO ufo)
        {
            ufo.OnReturnUFO -= ReturnUFO;
            _ufoFactory.ReturnObject(ufo);
        }

        private IEnumerator SpawnUFOCoroutine()
        {
            while (!FlagGameOver)
            {
                SpawnObject();
                yield return _waitRespawnUFORange;
            }
        }
    }
}