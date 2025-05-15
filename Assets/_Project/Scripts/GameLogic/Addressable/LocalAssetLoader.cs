using System.Threading.Tasks;
using Characters;
using Player;
using Shooting;
using UI.View;

namespace LoadingAssets
{
    public class LocalAssetLoader : BaseAssetLoader, IAssetLoader
    {
        private string _uiGameId = "UIGame";
        private string _spaceShipId = "SpaceShip";
        private string _missileId = "Missile";
        private string _asteroidEnemyId = "AsteroidEnemy";
        private string _ufoEnemy = "UFOEnemy";
        
        public async Task<GameplayUIView> CreateGameplayUIView()
        {
            if (CachedComponent.TryGetValue(_uiGameId, out var cachedComponent))
            {
                return cachedComponent as GameplayUIView;
            }
    
            return await LoadPrefab<GameplayUIView>(_uiGameId);
        }

        public void DestroyGameplayUIView()
        {
            ReleasePrefab(_uiGameId);
        }

        public async Task<SpaceShip> CreateSpaceShip()
        {
            if (CachedComponent.TryGetValue(_spaceShipId, out var cachedComponent))
            {
                return cachedComponent as SpaceShip;
            }

            return await LoadPrefab<SpaceShip>(_spaceShipId);
        }

        public void DestroySpaceShip()
        {
            ReleasePrefab(_spaceShipId);
        }

        public async Task<Missile> CreateMissile()
        {
            if (CachedComponent.TryGetValue(_missileId, out var cachedComponent))
            {
                return cachedComponent as Missile;
            }

            return await LoadPrefab<Missile>(_missileId);
        }

        public void DestroyMissile()
        {
            ReleasePrefab(_missileId);
        }

        public async Task<Asteroid> CreateAsteroid()
        {
            if (CachedComponent.TryGetValue(_asteroidEnemyId, out var cachedComponent))
            {
                return cachedComponent as Asteroid;
            }
            return await LoadPrefab<Asteroid>(_asteroidEnemyId);
        }

        public void DestroyAsteroid()
        {
            ReleasePrefab(_asteroidEnemyId);
        }

        public async Task<UFO> CreateUFO()
        {
            if (CachedComponent.TryGetValue(_ufoEnemy, out var cachedComponent))
            {
                return cachedComponent as UFO;
            }
            return await LoadPrefab<UFO>(_ufoEnemy);
        }

        public void DestroyUFO()
        {
            ReleasePrefab(_ufoEnemy);
        }
    }
}