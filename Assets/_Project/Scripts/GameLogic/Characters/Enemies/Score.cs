using Managers;
using Shooting;
using UnityEngine;
using Zenject;

namespace Characters
{
    public class Score : MonoBehaviour
    {
        private ScoreManager _scoreManager;

        [SerializeField] private int _scoreKill = 5;

        [Inject]
        public void Construct(ScoreManager scoreManager)
        {
            _scoreManager = scoreManager;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
            {
                _scoreManager.AddScore(_scoreKill);
            }
        }
    }
}