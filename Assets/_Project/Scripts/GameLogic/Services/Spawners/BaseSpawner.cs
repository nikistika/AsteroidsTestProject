using Cysharp.Threading.Tasks;

namespace _Project.Scripts.GameLogic.Services.Spawners
{
    public abstract class BaseSpawner<T>
    {
        protected bool FlagGameOver;
        protected readonly ScreenSize ScreenSize;
        protected readonly GameState GameState;
        
        protected BaseSpawner(
            GameState gameState,
            ScreenSize screenSize)
        {
            GameState = gameState;
            ScreenSize = screenSize;
        }

        public async UniTask StartWork()
        {
            BaseInitialize();
            await Initialize();
        }

        private void BaseInitialize()
        {
            GameState.OnGameOver += GameStateHandler;
            GameState.OnGameContinue += GameContinue;
            GameState.OnGameExit += GameExit;
        }

        protected abstract UniTask Initialize();

        private void GameStateHandler()
        {
            FlagGameOver = true;
        }

        protected abstract UniTask GameContinue();

        private void GameExit()
        {
            GameState.OnGameOver -= GameStateHandler;
            GameState.OnGameContinue -= GameContinue;
            GameState.OnGameExit -= GameExit;
        }
    }
}