using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.RemoteConfig;
using GameLogic;
using LoadingAssets;
using UnityEngine;

namespace _Project.Scripts.GameLogic.Factories
{
    public abstract class EnemyFactory<T> : BaseFactory<T> where T : MonoBehaviour
    {
        protected readonly IScoreService ScoreService;
        protected readonly GameState GameState;
        protected readonly IKillService KillService;
        protected readonly IRandomService RandomService;

        protected EnemyFactory(
            IScoreService scoreService,
            GameState gameState,
            ScreenSize screenSize,
            IKillService killService,
            IAssetLoader assetLoader,
            RemoteConfigService remoteConfigService,
            IRandomService randomService) :
            base(screenSize, assetLoader, remoteConfigService)
        {
            GameState = gameState;
            ScoreService = scoreService;
            KillService = killService;
            RandomService = randomService;
        }
        
        //TODO: RandomMethod
        protected Vector2 GetRandomSpawnPosition()
        {
            return RandomService.GetRandomSpawnPosition();
        }
    }
}