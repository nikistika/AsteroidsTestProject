using Cysharp.Threading.Tasks;
using GameLogic;

namespace Managers
{
    public abstract class BaseSpawner<T>
    {
        protected bool FlagGameOver;
        protected ScreenSize ScreenSize;
        protected GameState GameState;

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