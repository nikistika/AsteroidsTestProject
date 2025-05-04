using GameLogic.Analytics;
using Managers;
using Shooting;
using UnityEngine;
using Character = Characters.Character;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Character
    {
        private ShootingMissile _shootingMissile;
        private AnalyticsController _analyticsController;
        private KillManager _killManager;

        [SerializeField] private ShootingLaser _shootingLaser;

        public void Construct(
            AnalyticsController analyticsController,
            KillManager killManager)
        {
            _analyticsController = analyticsController;
            _killManager = killManager;
        }

        private void Start()
        {
            _shootingLaser.Construct(_analyticsController, _killManager);
        }

        protected override void Initialize()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
        }
    }
}