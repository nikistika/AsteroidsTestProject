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

        public EntryPoint(
            SpaceShipSpawnManager spaceShipSpawnManager,
            AsteroidSpawnManager asteroidSpawnManager,
            UISpawnManager uiSpawnManager,
            UFOSpawnManager ufoSpawnManager)
        {
            _spaceShipSpawnManager = spaceShipSpawnManager;
            _asteroidSpawnManager = asteroidSpawnManager;
            _uiSpawnManager = uiSpawnManager;
            _ufoSpawnManager = ufoSpawnManager;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        public void Initialize()
        {
            _spaceShipSpawnManager.StartWork();
            _uiSpawnManager.StartWork();
            _asteroidSpawnManager.StartWork();
            _ufoSpawnManager.StartWork();
        }
    }
}