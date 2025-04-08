using System.Collections;
using System.Collections.Generic;
using Characters;
using GameLogic;
using Managers;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class EntryPoint : MonoBehaviour
{
    private Camera _camera;
    private float _halfHeightCamera;
    private float _halfWidthCamera;
    
    [SerializeField] private AsteroidSpawmManager _asteroidSpawmManager;
    [SerializeField] private SpaceShipSpawnManager _spaceShipSpawnManager;
    
    private void Awake()
    {

        DependencyInitialization();
        DependencyTransfer();

    }


    private void DependencyInitialization()
    {
        _camera = Camera.main;
        _halfHeightCamera = _camera.orthographicSize;
        _halfWidthCamera = _halfHeightCamera * _camera.aspect;
    }

    private void DependencyTransfer()
    {
        _asteroidSpawmManager.Construct(_camera, _halfHeightCamera, _halfWidthCamera);
        _spaceShipSpawnManager.Construct(_camera, _halfHeightCamera, _halfWidthCamera);
    }
    
}
