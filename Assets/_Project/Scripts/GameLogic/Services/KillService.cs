using _Project.Scripts.Analytics;
using Zenject;

namespace _Project.Scripts.GameLogic.Services
{
    public class KillService : IInitializable, IKillService
    {
        private readonly GameState _gameState;
        private readonly IAnalyticsService _analyticsService;

        private int _quantityMissile;
        private int _quantityLaser;
        private int _quantityKillAsteroids;
        private int _quantityKillUfo;

        public KillService(
            GameState gameState,
            IAnalyticsService analyticsService)
        {
            _gameState = gameState;
            _analyticsService = analyticsService;
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
            _analyticsService.GameOverEvent(_quantityMissile, _quantityLaser, _quantityKillAsteroids,
                _quantityKillUfo);
        }
    }
}