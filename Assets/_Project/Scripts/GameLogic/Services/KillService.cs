using GameLogic;
using GameLogic.Analytics;
using Zenject;

namespace Managers
{
    public class KillService : IInitializable
    {
        private GameState _gameState;
        private readonly AnalyticsController _analyticsController;

        private int _quantityMissile;
        private int _quantityLaser;
        private int _quantityKillAsteroids;
        private int _quantityKillUfo;

        public KillService(
            GameState gameState,
            AnalyticsController analyticsController)
        {
            _gameState = gameState;
            _analyticsController = analyticsController;
        }

        public void Initialize()
        {
            _gameState.OnGameOver += GameState;
        }

        public void AddMissile(int missile)
        {
            _quantityMissile += missile;
        }

        public void AddLaser(int laser)
        {
            _quantityLaser += laser;
        }

        public void AddAsteroid(int asteroid)
        {
            _quantityKillAsteroids += asteroid;
        }

        public void AddUFO(int ufo)
        {
            _quantityKillUfo += ufo;
        }

        private void GameState()
        {
            _gameState.OnGameOver -= GameState;
            _analyticsController.GameOverEvent(_quantityMissile, _quantityLaser, _quantityKillAsteroids,
                _quantityKillUfo);
        }
    }
}