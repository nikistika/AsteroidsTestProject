using Characters;
using Player;
using UnityEngine;

namespace Factories
{
    public class AsteroidFactory : AbstractEnemyFactory<Asteroid>
    {
        
        protected override Asteroid ActionCreateObject()
        {
            Asteroid asteroid = Instantiate(_prefab);
            asteroid.Construct(_gameOver, _camera, _halfHeightCamera, _halfWidthCamera);
            asteroid.GetComponent<Score>().Construct(_dataSpaceShip);
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