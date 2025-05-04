using Characters;
using GameLogic;
using Managers;
using Player;
using SciptableObjects;
using UnityEngine;
using Zenject;

namespace Factories
{
    public class UFOFactory : EnemyFactory<UFO>, IInitializable
    {
        private readonly ShipRepository _shipRepository;

        public UFOFactory(
            ScoreManager scoreManager,
            GameOver gameOver,
            ScreenSize screenSize,
            UFO prefab, ShipRepository
                shipRepository,
            PoolSizeSO ufoPoolSizeData,
            KillManager killManager) :
            base(scoreManager, gameOver, screenSize, prefab, ufoPoolSizeData, killManager)
        {
            _shipRepository = shipRepository;
        }

        protected override void ActionReleaseObject(UFO obj)
        {
            obj.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            obj.gameObject.SetActive(false);
        }

        protected override UFO ActionCreateObject()
        {
            var UFO = Object.Instantiate(Prefab);
            UFO.Construct(GameOver, _shipRepository, ScreenSize, _killManager);
            UFO.GetComponent<Score>().Construct(ScoreManager);
            UFO.gameObject.transform.position = GetRandomSpawnPosition();
            return UFO;
        }

        protected override void ActionGetObject(UFO obj)
        {
            obj.gameObject.SetActive(true);
            obj.gameObject.transform.position = GetRandomSpawnPosition();
        }

        public void Initialize()
        {
            StartWork();
        }
    }
}