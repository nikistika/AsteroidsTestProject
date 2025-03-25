using GameLogic;
using Managers;
using UI;

namespace Characters
{
    public abstract class Enemy : Character
    {
        protected DataSpaceShip _dataSpaceShip;
        protected GameOver _gameOver;
        protected SpawnManager _spawnManager;
    }
}