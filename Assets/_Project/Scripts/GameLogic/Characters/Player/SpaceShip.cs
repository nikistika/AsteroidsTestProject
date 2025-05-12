using Cysharp.Threading.Tasks;
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
        private ShootingMissile _shootingMissile;
        private AnalyticsController _analyticsController;
        private KillService _killService;

        [SerializeField] private ShootingLaser _shootingLaser;
        
        public void Initialize(
            AnalyticsController analyticsController,
            KillService killService,
            ScreenSize screenSize)
        {
            BaseInitialize(screenSize);
            _analyticsController = analyticsController;
            _killService = killService;
            Rigidbody = GetComponent<Rigidbody2D>();
            _shootingLaser.Initilize(_analyticsController, _killService);
        }
    }
}