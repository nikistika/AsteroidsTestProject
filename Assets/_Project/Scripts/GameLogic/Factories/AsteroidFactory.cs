using Characters;
using GameLogic;
using Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidFactory : AbstractEnemyFactory<Asteroid>
{

    [SerializeField] private SpawnManager _spawnManager;
    [SerializeField] private DataSpaceShip _dataSpaceShip;
    [SerializeField] private GameOver _gameOver;

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
