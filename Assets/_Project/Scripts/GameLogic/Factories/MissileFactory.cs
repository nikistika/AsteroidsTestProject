using ConfigData;
using Cysharp.Threading.Tasks;
using GameLogic;
using LoadingAssets;
using Player;
using ScriptableObjects;
using Shooting;
using UnityEngine;

namespace Factories
{
    public class MissileFactory : BaseFactory<Missile>
    {
        private readonly SpaceShip _spaceShip;
        private readonly ShootingMissile _shootingMissile;
        private readonly RemoteConfigController _remoteConfigController;

        public MissileFactory(
            ScreenSize screenSize,
            SpaceShip spaceShip,
            ShootingMissile shootingMissile,
            IAssetLoader assetLoader,
            RemoteConfigController remoteConfigController) :
            base(screenSize, assetLoader, remoteConfigController)
        {
            _spaceShip = spaceShip;
            _shootingMissile = shootingMissile;
            _remoteConfigController = remoteConfigController;
        }
        
        protected override void InitializeFactory()
        {
            DefaultPoolSize = _remoteConfigController.MissilePoolSizeData.DefaultPoolSize;
            MaxPoolSize = _remoteConfigController.MissilePoolSizeData.MaxPoolSize;
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