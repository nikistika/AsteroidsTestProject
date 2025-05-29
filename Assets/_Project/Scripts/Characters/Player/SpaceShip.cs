using GameLogic;
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
        private AnalyticsController _analyticsController;
        private KillService _killService;

        [SerializeField] private ShootingLaser _shootingLaser;

        public void Construct(
            AnalyticsController analyticsController,
            KillService killService,
            ScreenSize screenSize)
        {
            base.Construct(screenSize);
            _analyticsController = analyticsController;
            _killService = killService;
        }

        public void StartWork()
        {
            _shootingLaser.Construct(_analyticsController, _killService);
        }
    }
}