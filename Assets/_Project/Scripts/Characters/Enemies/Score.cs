using Managers;
using Shooting;
using UnityEngine;

namespace Characters
{
    public class Score : MonoBehaviour
    {
        private ScoreService _scoreService;

        [SerializeField] private int _scoreKill = 5;

        public void Initialize(
            ScoreService scoreService)
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