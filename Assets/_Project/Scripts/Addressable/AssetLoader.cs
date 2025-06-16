using System.Threading.Tasks;
using _Project.Scripts.Audio;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.Characters.Player;
using _Project.Scripts.GameLogic.Shootnig;
using _Project.Scripts.UI.GameScene;
using _Project.Scripts.UI.MenuScene;

namespace _Project.Scripts.Addressable
{
    public class AssetLoader : BaseAssetLoader, IAssetLoader
    {
        private const string UIMenuId = "UIMenu";
        private const string UIGameId = "UIGame";
        private const string SpaceShipId = "SpaceShip";
        private const string MissileId = "Missile";
        private const string AsteroidEnemyId = "AsteroidEnemy";
        private const string UfoEnemy = "UFOEnemy";
        private const string AudioController = "AudioController";

        public Task<GameplayUIView> CreateGameplayUIView() => LoadAssetComponent<GameplayUIView>(UIGameId);
        public void DestroyGameplayUIView() => ReleasePrefab(UIGameId);

        public Task<MenuUIView> CreateMenuUIView() => LoadAssetComponent<MenuUIView>(UIMenuId);
        public void DestroyMenuUIView() => ReleasePrefab(UIMenuId);

        public Task<SpaceShip> CreateSpaceShip() => LoadAssetComponent<SpaceShip>(SpaceShipId);
        public void DestroySpaceShip() => ReleasePrefab(SpaceShipId);

        public Task<Missile> CreateMissile() => LoadAssetComponent<Missile>(MissileId);
        public void DestroyMissile() => ReleasePrefab(MissileId);

        public Task<Asteroid> CreateAsteroid() => LoadAssetComponent<Asteroid>(AsteroidEnemyId);
        public void DestroyAsteroid() => ReleasePrefab(AsteroidEnemyId);

        public Task<UFO> CreateUFO() => LoadAssetComponent<UFO>(UfoEnemy);
        public void DestroyUFO() => ReleasePrefab(UfoEnemy);
        
        public Task<AudioController> CreateAudioSources() => LoadAssetComponent<AudioController>(AudioController);
        public void DestroyAudioSources() => ReleasePrefab(AudioController);
        
    }
}