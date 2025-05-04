using GameLogic.Analytics;
using Managers;
using UI;
using Zenject;

namespace GameLogic
{
    public class EntryPoint : IInitializable
    {
        private readonly AsteroidSpawnManager _asteroidSpawnManager;
        private readonly UFOSpawnManager _ufoSpawnManager;
        private readonly SpaceShipSpawnManager _spaceShipSpawnManager;
        private readonly UISpawnManager _uiSpawnManager;
        private readonly AnalyticsController _analyticsController;

        public EntryPoint(
            SpaceShipSpawnManager spaceShipSpawnManager,
            AsteroidSpawnManager asteroidSpawnManager,
            UISpawnManager uiSpawnManager,
            UFOSpawnManager ufoSpawnManager,
            AnalyticsController analyticsController)
        {
            _spaceShipSpawnManager = spaceShipSpawnManager;
            _asteroidSpawnManager = asteroidSpawnManager;
            _uiSpawnManager = uiSpawnManager;
            _ufoSpawnManager = ufoSpawnManager;
            _analyticsController = analyticsController;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Initialize()
        {
            _spaceShipSpawnManager.StartWork();
            _uiSpawnManager.StartWork();
            _asteroidSpawnManager.StartWork();
            _ufoSpawnManager.StartWork();
            _analyticsController.StartGameEvent();
        }
    }
}