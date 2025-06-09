using _Project.Scripts.GameLogic.Services;
using Shooting;
using UnityEngine;

namespace _Project.Scripts.Characters.Enemies
{
    public class Score : MonoBehaviour
    {
        private IScoreService _scoreService;

        [SerializeField] private int _scoreKill = 5;

        public void Initialize(
            IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Missile>(out _) || collision.TryGetComponent<Laser>(out _))
            {
                _scoreService.AddScore(_scoreKill);
            }
        }
    }
}