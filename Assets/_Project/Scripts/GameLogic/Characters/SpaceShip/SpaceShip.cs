using Shooting;
using UI;
using UnityEngine;

namespace Characters
{
    public class SpaceShip : Character
    {
        private ShootingMissile _shootingMissile;

        [SerializeField] private ShootingLaser _shootingLaser;

        private void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();

            _shootingMissile = gameObject.GetComponent<ShootingMissile>();
        }



        public void ShootingMissile()
        {
            _shootingMissile.ShotMissile();
        }

        public void ShootingLaser()
        {
            _shootingLaser.Shot();
        }
    }
}