using System.Threading.Tasks;
using Characters;
using Player;
using Shooting;
using UI.View;

namespace LoadingAssets
{
    public partial class LocalAssetLoader : BaseAssetLoader, IAssetLoader
    {
        private string _uiGameId = "UIGame";
        private string _spaceShipId = "SpaceShip";
        private string _missileId = "Missile";
        private string _asteroidEnemyId = "AsteroidEnemy";
        private string _ufoEnemy = "UFOEnemy";

        public async Task<GameplayUIView> CreateGameplayUIView()
        {
            return await LoadPrefab<GameplayUIView>(_uiGameId);
        }

        public void DestroyGameplayUIView()
        {
            ReleasePrefab(_uiGameId);
        }

        public async Task<SpaceShip> CreateSpaceShip()
        {
            // if (CachedComponent[_spaceShipId] != null)
            // {
            //     return CachedComponent[_spaceShipId].GetComponent<SpaceShip>();
            // }

            return await LoadPrefab<SpaceShip>(_spaceShipId);
        }

        public void DestroySpaceShip()
        {
            ReleasePrefab(_spaceShipId);
        }

        public async Task<Missile> CreateMissile()
        {
            // if (CachedComponent[_missileId] != null)
            // {
            //     return CachedComponent[_missileId] as Missile;
            // }

            return await LoadPrefab<Missile>(_missileId);
        }

        public void DestroyMissile()
        {
            ReleasePrefab(_missileId);
        }

        public async Task<Asteroid> CreateAsteroid()
        {
            return await LoadPrefab<Asteroid>(_asteroidEnemyId);
        }

        public void DestroyAsteroid()
        {
            ReleasePrefab(_asteroidEnemyId);
        }

        public async Task<UFO> CreateUFO()
        {
            return await LoadPrefab<UFO>(_ufoEnemy);
        }

        public void DestroyUFO()
        {
            ReleasePrefab(_ufoEnemy);
        }
    }
}