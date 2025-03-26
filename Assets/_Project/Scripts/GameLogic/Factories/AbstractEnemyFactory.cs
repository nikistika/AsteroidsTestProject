using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractEnemyFactory<T> : BaseFactory<T> where T : MonoBehaviour
{

    private float _halfHeightCamera;
    private float _halfWidthCamera;
    private Camera _camera;

    protected void Awake()
    {
        _camera = Camera.main;
        _halfHeightCamera = _camera.orthographicSize;
        _halfWidthCamera = _halfHeightCamera * _camera.aspect;
    }

    protected Vector2 GetRandomSpawnPosition()
    {
        var randomIndex = Random.Range(1, 5);

        switch (randomIndex)
        {
            case 1:
                return new Vector2(Random.Range(-_halfWidthCamera, _halfWidthCamera), _halfHeightCamera + 0.5f);
            case 2:
                return new Vector2(Random.Range(-_halfWidthCamera, _halfWidthCamera), -_halfHeightCamera - 0.5f);
            case 3:
                return new Vector2(_halfWidthCamera + 0.5f, Random.Range(-_halfHeightCamera, _halfHeightCamera));
            case 4:
                return new Vector2(-_halfWidthCamera - 0.5f, Random.Range(-_halfHeightCamera, _halfHeightCamera));
        }

        return new Vector2(0, 0);
    }
}
