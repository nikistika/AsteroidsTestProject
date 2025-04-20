using System;
using Characters;
using Cysharp.Threading.Tasks;
using Factories;
using GameLogic;
using SciptableObjects;
using UnityEngine;

namespace Managers
{
    public class UFOSpawnManager : BaseSpawnManager<UFO>
    {
        private WaitForSeconds _waitRespawnUFORange;
        private UFOFactory _ufoFactory;
        private EnemySpawnManagerSO _ufoSpawnData;

        public UFOSpawnManager(GameOver gameOver, ScreenSize screenSize, UFOFactory ufoFactory,
            EnemySpawnManagerSO ufoSpawnData, ScoreManager scoreManager) :
            base(gameOver, screenSize, scoreManager)
        {
            _ufoFactory = ufoFactory;
            _ufoSpawnData = ufoSpawnData;
        }

        public override UFO SpawnObject()
        {
            var ufo = _ufoFactory.SpawnObject();
            ufo.OnReturnUFO += ReturnUFO;
            return ufo;
        }

        protected override void Initialize()
        {
            SpawnUFOAsync().Forget();
        }

        private void ReturnUFO(UFO ufo)
        {
            ufo.OnReturnUFO -= ReturnUFO;
            _ufoFactory.ReturnObject(ufo);
        }

        private async UniTask SpawnUFOAsync()
        {
            while (!FlagGameOver)
            {
                SpawnObject();
                await UniTask.Delay(TimeSpan.FromSeconds(_ufoSpawnData.RespawnRange));
            }
        }
    }
}