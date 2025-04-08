using Player;
using Shooting;
using UnityEngine;

namespace Characters
{
    public class Score : MonoBehaviour
    {
        private DataSpaceShip _dataSpaceShip;
        
        [SerializeField] private int _scoreKill = 5;

        public void Construct(DataSpaceShip dataSpaceShip)
        {
            _dataSpaceShip = dataSpaceShip;
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Missile>() || collision.GetComponent<Laser>())
            {
                _dataSpaceShip.AddScore(_scoreKill);
            }
        }
        
    }
}