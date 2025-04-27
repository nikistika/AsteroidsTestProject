using GameLogic;

namespace Managers
{
    public abstract class BaseSpawnManager<T>
    {
        protected bool FlagGameOver;
        protected ScreenSize ScreenSize;
        protected GameOver GameOver;

        protected BaseSpawnManager(GameOver gameOver,
            ScreenSize screenSize)
        {
            GameOver = gameOver;
            ScreenSize = screenSize;
        }

        public void StartWork()
        {
            BaseInitialize();
            Initialize();
        }

        public abstract T SpawnObject();

        private void BaseInitialize()
        {
            GameOver.OnGameOver += GameOverHandler;
        }

        protected abstract void Initialize();

        private void GameOverHandler()
        {
            GameOver.OnGameOver -= GameOverHandler;
            FlagGameOver = true;
        }
    }
}