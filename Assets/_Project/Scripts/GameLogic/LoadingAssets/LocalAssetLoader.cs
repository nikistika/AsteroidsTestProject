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
            return await InstantiateAsset<GameplayUIView>(_uiGameId);
        }

        public void DestroyGameplayUIView()
        {
            ReleaseInstanceAsset(_uiGameId);
        }
        
        public async Task<SpaceShip> CreateSpaceShip()
        {
            return await InstantiateAsset<SpaceShip>(_spaceShipId);
        }

        public void DestroySpaceShip()
        {
            ReleaseInstanceAsset(_spaceShipId);
        }
        
        public async Task<Missile> CreateMissile()
        {
            return await InstantiateAsset<Missile>(_missileId);
        }

        public void DestroyMissile()
        {
            ReleaseInstanceAsset(_missileId);
        }
        
        public async Task<Asteroid> CreateAsteroid()
        {
            return await InstantiateAsset<Asteroid>(_asteroidEnemyId);
        }

        public void DestroyAsteroid()
        {
            ReleaseInstanceAsset(_asteroidEnemyId);
        }
        
        public async Task<UFO> CreateUFO()
        {
            return await InstantiateAsset<UFO>(_ufoEnemy);
        }

        public void DestroyUFO()
        {
            ReleaseInstanceAsset(_ufoEnemy);
        }
        
        
    }
}