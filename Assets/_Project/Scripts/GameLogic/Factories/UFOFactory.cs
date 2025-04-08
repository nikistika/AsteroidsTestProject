using Characters;
using Player;
using UnityEngine;

namespace Factories
{
    public class UFOFactory : AbstractEnemyFactory<UFO>
    {
        private SpaceShip _spaceShip;

        public void Construct(SpaceShip spaceShip, DataSpaceShip dataSpaceShip, 
            Camera camera, float halfHeightCamera, float halfWidthCamera)
        {
            base.Construct(dataSpaceShip, camera, halfHeightCamera, halfWidthCamera);
            _spaceShip = spaceShip;
        }
        
        public override void ActionReleaseObject(UFO obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }
        
        protected override UFO ActionCreateObject()
        {
            var UFO = Instantiate(_prefab);
            UFO.Construct(_gameOver, _spaceShip);
            UFO.GetComponent<Score>().Construct(_dataSpaceShip);
            UFO.gameObject.transform.position = GetRandomSpawnPosition();
            return UFO;
        }

        protected override void ActionGetObject(UFO obj)
        {
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.position = GetRandomSpawnPosition(); 
        }
    }
}