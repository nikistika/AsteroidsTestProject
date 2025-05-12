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
            await BaseInitialize();
            await Initialize();
        }

        private async UniTask BaseInitialize()
        {
            GameOver.OnGameOver += GameOverHandler;
        }

        protected abstract UniTask Initialize();

        private void GameOverHandler()
        {
            GameOver.OnGameOver -= GameOverHandler;
            FlagGameOver = true;
        }
    }
}