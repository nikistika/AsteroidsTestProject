using Shooting;
using UnityEngine;
using Character = Characters.Character;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Character
    {
        private ShootingMissile _shootingMissile;

        [SerializeField] private ShootingLaser _shootingLaser;

        private void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}