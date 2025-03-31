using Characters;
using Player;
using UnityEngine;

namespace Factories
{
    public class AsteroidFactory : AbstractEnemyFactory<Asteroid>
    {
        
        public void Construct(DataSpaceShip dataSpaceShip)
        {
            _dataSpaceShip = dataSpaceShip;
        }
        
        protected override Asteroid ActionCreateObject()
        {
            var asteroid = Instantiate(_prefab);
            asteroid.Construct(_dataSpaceShip, _gameOver);
            asteroid.gameObject.transform.position = GetRandomSpawnPosition();
            return asteroid;
        }

        protected override void ActionGetObject(Asteroid obj)
        {
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.position = GetRandomSpawnPosition();
            obj.Move();
            obj.IsObjectParent(true);
        }

        public override void ActionReleaseObject(Asteroid obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }
    }
}