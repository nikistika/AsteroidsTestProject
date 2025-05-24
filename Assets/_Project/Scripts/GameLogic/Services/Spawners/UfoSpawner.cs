using System;
using Characters;
using ConfigData;
using Cysharp.Threading.Tasks;
using Factories;
using GameLogic;
using GameLogic.Enums;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class UfoSpawner : BaseSpawner<UFO>
    {
        private WaitForSeconds _waitRespawnUFORange;
        private readonly UFOFactory _ufoFactory;
        private readonly RemoteConfigController _remoteConfigController;

        public UfoSpawner(
            GameState gameState,
            ScreenSize screenSize,
            UFOFactory ufoFactory,
            RemoteConfigController remoteConfigController) :
            base(gameState, screenSize)
        {
            _ufoFactory = ufoFactory;
            _remoteConfigController = remoteConfigController;
        }

        private void SpawnObject()
        {
            UFO ufo = _ufoFactory.SpawnObject();
            ufo.OnReturnUFO += ReturnUFO;
        }

        protected override async UniTask Initialize()
        {
            await SpawnUFOs();
        }

        protected override async UniTask GameContinue()
        {
            FlagGameOver = false;
            await SpawnUFOs();
        }

        private void ReturnUFO(UFO ufo)
        {
            ufo.OnReturnUFO -= ReturnUFO;
            _ufoFactory.ReturnObject(ufo);
        }

        private async UniTask SpawnUFOs()
        {
            while (!FlagGameOver)
            {
                SpawnObject();
                await UniTask.Delay(TimeSpan.FromSeconds(_remoteConfigController.UFoSpawnData.RespawnRange));
            }
        }
    }
}