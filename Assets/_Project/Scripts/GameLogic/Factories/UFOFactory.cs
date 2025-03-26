using Characters;
using Player;
using UnityEngine;

namespace Factories
{
    public class UFOFactory : AbstractEnemyFactory<UFO>
    {
        [SerializeField] private SpaceShip _spaceShip;

        protected override UFO ActionCreateObject()
        {
            var UFO = Instantiate(_prefab);
            UFO.Construct(_spawnManager, _dataSpaceShip, _gameOver, _spaceShip);
            UFO.gameObject.transform.position = GetRandomSpawnPosition();
            return UFO;
        }

        protected override void ActionGetObject(UFO obj)
        {
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.position = GetRandomSpawnPosition();
            obj.Move();
        }
    }
}