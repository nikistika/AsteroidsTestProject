using Player;
using Shooting;
using UnityEngine;

namespace Factories
{
    public class MissileFactory : BaseFactory<Missile>
    {
        private SpaceShip _spaceShip;
        private ShootingMissile _shootingMissile;

        public MissileFactory(Camera camera, float halfHeightCamera, float halfWidthCamera, 
            Missile prefab, SpaceShip spaceShip, ShootingMissile shootingMissile) : 
            base(camera, halfHeightCamera, halfWidthCamera, prefab)
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
            var missile = Object.Instantiate(_prefab, _spaceShip.gameObject.transform, true);
            missile.transform.position = _spaceShip.transform.position;
            missile.Construct(_shootingMissile, _camera, _halfHeightCamera, _halfWidthCamera);
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