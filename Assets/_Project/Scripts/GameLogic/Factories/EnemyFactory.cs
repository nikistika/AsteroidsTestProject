using ConfigData;
using GameLogic;
using LoadingAssets;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Factories
{
    public abstract class EnemyFactory<T> : BaseFactory<T> where T : MonoBehaviour
    {
        protected readonly ScoreService ScoreService;
        protected readonly GameState GameState;
        protected readonly KillService KillService;

        protected EnemyFactory(
            ScoreService scoreService,
            GameState gameState,
            ScreenSize screenSize,
            KillService killService,
            IAssetLoader assetLoader,
            RemoteConfigService remoteConfigService) :
            base(screenSize, assetLoader, remoteConfigService)
        {
            GameState = gameState;
            ScoreService = scoreService;
            KillService = killService;
        }

        protected Vector2 GetRandomSpawnPosition()
        {
            var randomIndex = Random.Range(1, 5);

            switch (randomIndex)
            {
                case 1:
                    return new Vector2(Random.Range(-ScreenSize.HalfWidthCamera, ScreenSize.HalfWidthCamera),
                        ScreenSize.HalfHeightCamera + 0.5f);
                case 2:
                    return new Vector2(Random.Range(-ScreenSize.HalfWidthCamera, ScreenSize.HalfWidthCamera),
                        -ScreenSize.HalfHeightCamera - 0.5f);
                case 3:
                    return new Vector2(ScreenSize.HalfWidthCamera + 0.5f,
                        Random.Range(-ScreenSize.HalfHeightCamera, ScreenSize.HalfHeightCamera));
                case 4:
                    return new Vector2(-ScreenSize.HalfWidthCamera - 0.5f,
                        Random.Range(-ScreenSize.HalfHeightCamera, ScreenSize.HalfHeightCamera));
            }

            return new Vector2(0, 0);
        }
    }
}