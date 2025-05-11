using System.Threading.Tasks;
using Characters;
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
            Missile prefab,
            SpaceShip spaceShip,
            ShootingMissile shootingMissile,
            PoolSizeSO missilePoolSizeData,
            IAssetLoader assetLoader) :
            base(screenSize, prefab, missilePoolSizeData, assetLoader)
        {
            _spaceShip = spaceShip;
            _shootingMissile = shootingMissile;
        }

        protected override void ActionReleaseObject(Missile obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }

        protected override async UniTask<Missile> ActionCreateObject()
        {
            var missile = await _assetLoader.CreateMissile();
            missile.transform.SetParent( _spaceShip.gameObject.transform, true);
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
    }
}