using System;
using _Project.Scripts.AnimationControllers;
using _Project.Scripts.Audio;
using _Project.Scripts.Characters.Player;
using _Project.Scripts.GameLogic;
using _Project.Scripts.GameLogic.Services;
using _Project.Scripts.GameLogic.Shootnig;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Project.Scripts.Characters.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class UFO : Character
    {
        public event Action<UFO> OnReturnUFO;

        private ShipRepository _shipRepository;
        private GameState _gameState;
        private IKillService _killService;
        private bool _flagGameOver;
        private IAudioService _audioService;
        private EnemyAnimationController _enemyAnimationController;
        
        private Collider2D _collider2D;

        [SerializeField] private int _speed = 1;

        public void Construct(
            GameState gameState,
            ShipRepository shipRepository,
            ScreenSize screenSize,
            IKillService killService,
            IAudioService audioService,
            EnemyAnimationController enemyAnimationController)
        {
            base.Construct(screenSize);
            _gameState = gameState;
            _shipRepository = shipRepository;
            _killService = killService;
            _audioService = audioService;
            _enemyAnimationController = enemyAnimationController;
        }

        private void Start()
        {
            _gameState.OnGameOver += GameState;
            _gameState.OnGameContinue += GameContinue;
            _gameState.OnGameExit += GameExit;
        }

        private void FixedUpdate()
        {
            if (!_flagGameOver)
            {
                Move();
            }
        }

        public void Initialize()
        {
            _collider2D = GetComponent<Collider2D>();
            Rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Move()
        {
            Vector2 direction = (_shipRepository.SpaceShip.transform.position - transform.position).normalized;
            Rigidbody.velocity = direction * _speed;
        }

        private async void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Missile>(out _) || collision.TryGetComponent<Laser>(out _))
            {
                _killService.AddUFO(1);
                _audioService.PlayExplosionAudio();
                
                _collider2D.enabled = false;
                await _enemyAnimationController.ActivateExplosion();
                _collider2D.enabled = true;
                
                OnReturnUFO?.Invoke(this);
            }

            if (collision.TryGetComponent<SpaceShip>(out _))
            {
                _gameState.EndGame();
            }
        }

        private void GameState()
        {
            _flagGameOver = true;
            Rigidbody.velocity = Vector2.zero;
        }

        private UniTask GameContinue()
        {
            gameObject.SetActive(false);
            _flagGameOver = false;
            return UniTask.CompletedTask;
        }

        private void GameExit()
        {
            _gameState.OnGameOver -= GameState;
            _gameState.OnGameContinue -= GameContinue;
            _gameState.OnGameExit -= GameExit;
        }
    }
}