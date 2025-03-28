using Player;
using Shooting;
using UnityEngine;

namespace Factories
{
    public class MissileFactory : BaseFactory<Missile>
    {
        [SerializeField] private SpaceShip _spaceShip;
        [SerializeField] private ShootingMissile _shootingMissile;
        
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