using GameLogic;
using GameLogic.Analytics;
using Service;
using Shooting;
using UnityEngine;
using Character = Characters.Character;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Character
    {
        private IAnalyticsService _analyticsService;
        private IKillService _killService;

        [SerializeField] private ShootingLaser _shootingLaser;

        public void Construct(
            IAnalyticsService analyticsService,
            IKillService killService,
            ScreenSize screenSize)
        {
            base.Construct(screenSize);
            _analyticsService = analyticsService;
            _killService = killService;
        }

        public void StartWork()
        {
            _shootingLaser.Construct(_analyticsService, _killService);
        }
    }
}