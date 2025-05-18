using Cysharp.Threading.Tasks;
using GameLogic;

namespace Managers
{
    public abstract class BaseSpawner<T>
    {
        protected bool FlagGameOver;
        protected ScreenSize ScreenSize;
        protected GameOver GameOver;

        protected BaseSpawner(
            GameOver gameOver,
            ScreenSize screenSize)
        {
            GameOver = gameOver;
            ScreenSize = screenSize;
        }

        public async UniTask StartWork()
        {
            BaseInitialize();
            await Initialize();
        }

        private void BaseInitialize()
        {
            GameOver.OnGameOver += GameOverHandler;
            GameOver.OnContinueGame += GameContinue;
            GameOver.OnGameExit += GameExit;
        }

        protected abstract UniTask Initialize();

        private void GameOverHandler()
        {
            FlagGameOver = true;
        }

        private void GameContinue()
        {
            FlagGameOver = false;
        }

        private void GameExit()
        {
            GameOver.OnGameOver -= GameOverHandler;
            GameOver.OnContinueGame -= GameContinue;
            GameOver.OnGameExit -= GameExit;
        }
    }
}