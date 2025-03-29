using GameLogic;
using Managers;
using Player;

namespace Characters
{
    public abstract class Enemy : Character
    {
        protected DataSpaceShip _dataSpaceShip;
        protected GameOver _gameOver;
        // protected SpawnManager _spawnManager;
    }
}