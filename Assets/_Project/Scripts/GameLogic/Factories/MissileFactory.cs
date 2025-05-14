using Cysharp.Threading.Tasks;
using GameLogic;
using LoadingAssets;
using Player;
using SciptableObjects;
using Shooting;
using UnityEngine;

namespace Factories
{
    public class MissileFactory : BaseFactory<Missile>
    {
        private readonly SpaceShip _spaceShip;
        private readonly ShootingMissile _shootingMissile;

        public MissileFactory(
            ScreenSize screenSize,
            SpaceShip spaceShip,
            ShootingMissile shootingMissile,
            PoolSizeSO missilePoolSizeData,
            IAssetLoader assetLoader) :
            base(screenSize, missilePoolSizeData, assetLoader)
        {
            _spaceShip = spaceShip;
            _shootingMissile = shootingMissile;
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

        protected override async void ActionGetObject(Missile obj)
        {
            obj.gameObject.SetActive(true);
            obj.transform.position = _spaceShip.transform.position;
            obj.Move();
        }

        protected override async UniTask GetPrefab()
        {
            Prefab = await _assetLoader.CreateMissile();
        }
    }
}