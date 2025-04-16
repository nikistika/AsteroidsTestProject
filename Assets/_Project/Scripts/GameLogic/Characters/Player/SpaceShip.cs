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

        protected override void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}