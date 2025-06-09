using _Project.Scripts.Analytics;
using _Project.Scripts.GameLogic.Services;
using GameLogic;
using Shooting;
using UnityEngine;
using Characters_Character = _Project.Scripts.Characters.Character;

namespace _Project.Scripts.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Characters_Character
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