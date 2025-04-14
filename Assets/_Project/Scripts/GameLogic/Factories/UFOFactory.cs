using Characters;
using GameLogic;
using Player;
using UnityEngine;

namespace Factories
{
    public class UFOFactory : AbstractEnemyFactory<UFO>
    {
        private SpaceShip _spaceShip;

        public UFOFactory(ScoreManager scoreManager, GameOver gameOver, Camera camera, 
            float halfHeightCamera, float halfWidthCamera, UFO prefab, SpaceShip spaceShip) : 
            base(scoreManager, gameOver, camera, halfHeightCamera, halfWidthCamera, prefab)
        {
            _spaceShip = spaceShip;
        }
        
        protected override void ActionReleaseObject(UFO obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }
        
        protected override UFO ActionCreateObject()
        {
            var UFO = Object.Instantiate(_prefab);
            UFO.Construct(_gameOver, _spaceShip, _halfHeightCamera, _halfWidthCamera);
            UFO.GetComponent<Score>().Construct(_scoreManager);
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