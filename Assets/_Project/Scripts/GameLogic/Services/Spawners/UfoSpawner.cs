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

namespace Service
{
    public class UfoSpawner : BaseSpawner<UFO>
    {
        private WaitForSeconds _waitRespawnUFORange;
        private readonly UFOFactory _ufoFactory;
        private readonly RemoteConfigService _remoteConfigService;

        public UfoSpawner(
            GameState gameState,
            ScreenSize screenSize,
            UFOFactory ufoFactory,
            RemoteConfigService remoteConfigService) :
            base(gameState, screenSize)
        {
            _ufoFactory = ufoFactory;
            _remoteConfigService = remoteConfigService;
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
                await UniTask.Delay(TimeSpan.FromSeconds(_remoteConfigService.UFoSpawnConfig.RespawnRange));
            }
        }
    }
}