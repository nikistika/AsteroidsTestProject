using Characters;

namespace Factories
{
    public class AsteroidFactory : AbstractEnemyFactory<Asteroid>
    {
        protected override Asteroid ActionCreateObject()
        {
            var asteroid = Instantiate(_prefab);
            asteroid.Construct(_spawnManager, _dataSpaceShip, _gameOver);
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
    }
}