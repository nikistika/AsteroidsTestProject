using GameLogic;
using GameLogic.Analytics;
using UnityEngine;
using Zenject;

namespace Managers
{
    public class KillManager : IInitializable
    {
        private GameOver _gameOver;
        private readonly AnalyticsController _analyticsController;

        private int _quantityMissile;
        private int _quantityLaser;
        private int _quantityKillAsteroids;
        private int _quantityKillUfo;

        public KillManager(
            GameOver gameOver,
            AnalyticsController analyticsController)
        {
            _gameOver = gameOver;
            _analyticsController = analyticsController;
        }

        public void Initialize()
        {
            _gameOver.OnGameOver += GameOver;
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

        private void GameOver()
        {
            _gameOver.OnGameOver -= GameOver;
            _analyticsController.GameOverEvent(_quantityMissile, _quantityLaser, _quantityKillAsteroids, _quantityKillUfo);
        }
    }
}