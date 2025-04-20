using Characters;
using GameLogic;
using Managers;
using Player;
using SciptableObjects;
using UnityEngine;

namespace Factories
{
    public class UFOFactory : AbstractEnemyFactory<UFO>
    {
        private SpaceShip _spaceShip;

        public UFOFactory(ScoreManager scoreManager, GameOver gameOver,
            ScreenSize screenSize, UFO prefab, SpaceShip spaceShip, PoolSizeSO ufoPoolSizeData) :
            base(scoreManager, gameOver, screenSize, prefab, ufoPoolSizeData)
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
            var UFO = Object.Instantiate(Prefab);
            UFO.Construct(GameOver, _spaceShip, ScreenSize);
            UFO.GetComponent<Score>().Construct(ScoreManager);
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