using Player;
using Shooting;
using UnityEngine;

namespace Factories
{
    public class MissileFactory : BaseFactory<Missile>
    {
        private SpaceShip _spaceShip;
        private ShootingMissile _shootingMissile;

        public void Construct(SpaceShip spaceShip, ShootingMissile shootingMissile)
        {
            _spaceShip = spaceShip;
            _shootingMissile = shootingMissile;
        }
        
        public override void ActionReleaseObject(Missile obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }
        
        protected override Missile ActionCreateObject()
        {
            var missile = Instantiate(_prefab, _spaceShip.gameObject.transform, true);
            missile.transform.position = _spaceShip.transform.position;

            missile.Construct(_shootingMissile);

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