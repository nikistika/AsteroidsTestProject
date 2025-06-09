using _Project.Scripts.Characters.Player;
using _Project.Scripts.RemoteConfig;
using Cysharp.Threading.Tasks;
using GameLogic;
using LoadingAssets;
using Shooting;
using UnityEngine;

namespace _Project.Scripts.GameLogic.Factories
{
    public class MissileFactory : BaseFactory<Missile>
    {
        private readonly SpaceShip _spaceShip;
        private readonly ShootingMissile _shootingMissile;
        private readonly RemoteConfigService _remoteConfigService;

        public MissileFactory(
            ScreenSize screenSize,
            SpaceShip spaceShip,
            ShootingMissile shootingMissile,
            IAssetLoader assetLoader,
            RemoteConfigService remoteConfigService) :
            base(screenSize, assetLoader, remoteConfigService)
        {
            _spaceShip = spaceShip;
            _shootingMissile = shootingMissile;
            _remoteConfigService = remoteConfigService;
        }
        
        protected override void InitializeFactory()
        {
            DefaultPoolSize = _remoteConfigService.MissilePoolSizeConfig.DefaultPoolSize;
            MaxPoolSize = _remoteConfigService.MissilePoolSizeConfig.MaxPoolSize;
        }

        protected override void ActionReleaseObject(Missile obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }

        protected override Missile ActionCreateObject()
        {
            var missile = Object.Instantiate(Prefab);
            missile.transform.SetParent(_spaceShip.gameObject.transform, true);
            missile.transform.position = _spaceShip.transform.position;
            missile.Construct(_shootingMissile, ScreenSize);
            return missile;
        }

        protected override void ActionGetObject(Missile obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = _spaceShip.transform.position;
            obj.Move();
        }

        protected override async UniTask GetPrefab()
        {
            Prefab = await AssetLoader.CreateMissile();
        }
    }
}