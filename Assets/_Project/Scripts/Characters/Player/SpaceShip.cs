using _Project.Scripts.Analytics;
using _Project.Scripts.Audio;
using _Project.Scripts.GameLogic;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.GameLogic.Shootnig;
using UnityEngine;
using Characters_Character = _Project.Scripts.Characters.Character;

namespace _Project.Scripts.Characters.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Characters_Character
    {
        private IAnalyticsService _analyticsService;
        private IKillService _killService;
        private IAudioService _audioService;

        [SerializeField] private ShootingLaser _shootingLaser;

        public void Construct(
            IAnalyticsService analyticsService,
            IKillService killService,
            ScreenSize screenSize,
            IAudioService audioService)
        {
            base.Construct(screenSize);
            _analyticsService = analyticsService;
            _killService = killService;
            _audioService = audioService;
        }

        public void StartWork()
        {
            _shootingLaser.Construct(_analyticsService, _killService, _audioService);
        }
    }
}