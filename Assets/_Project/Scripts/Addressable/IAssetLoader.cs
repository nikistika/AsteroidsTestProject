using System.Threading.Tasks;
using _Project.Scripts.Characters.Enemies;
using _Project.Scripts.Characters.Player;
using _Project.Scripts.UI.GameScene;
using _Project.Scripts.UI.MenuScene;
using Shooting;

namespace LoadingAssets
{
    public interface IAssetLoader
    {
        public Task<GameplayUIView> CreateGameplayUIView(); 
        public void DestroyGameplayUIView();
        
        public Task<MenuUIView> CreateMenuUIView(); 
        public void DestroyMenuUIView();
        
        public Task<SpaceShip> CreateSpaceShip(); 
        public void DestroySpaceShip(); 
        
        public Task<Missile> CreateMissile();

        public void DestroyMissile();

        public Task<Asteroid> CreateAsteroid();

        public void DestroyAsteroid();

        public Task<UFO> CreateUFO();

        public void DestroyUFO();
    }
}