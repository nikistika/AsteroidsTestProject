using System;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class Asteroid : MonoBehaviour
{
    
    private float _halfHeightCamera;
    private float _halfWidthCamera;
    private Camera _camera;
    private Rigidbody2D _rigidbody2D;
    private ObjectPool<Asteroid> _asteroidPool;

    public void Construct(ObjectPool<Asteroid> asteroidPool)
    {
        _asteroidPool = asteroidPool;
    }
    
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _camera = Camera.main;
        _halfHeightCamera = _camera.orthographicSize;
        _halfWidthCamera = _halfHeightCamera * _camera.aspect;

        RandomScale();
    }

    private void Update()
    {
        GoingAbroad();
    }

    private void RandomScale()
    {
        int randomIndex = Random.Range(0, 3);

        switch (randomIndex)
        {
            case 1:
                transform.localScale = new Vector3(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), 1);
                break;
            case 2:
                transform.localScale = new Vector3(Random.Range(1f, 2f), Random.Range(1f, 2f), 1);
                break;
        }
    }
    
    public void Move()
    {
        if(transform.position.y > _halfHeightCamera) _rigidbody2D.velocity = new Vector2(Random.Range(0, 0.5f), -1.0f);
        else if(transform.position.y < - _halfHeightCamera) _rigidbody2D.velocity = new Vector2(Random.Range(0, 0.5f), 1.0f);
        else if(transform.position.x > _halfWidthCamera) _rigidbody2D.velocity = new Vector2(-1.0f, Random.Range(0, 0.5f));
        else if(transform.position.x < - _halfWidthCamera) _rigidbody2D.velocity = new Vector2(1.0f, Random.Range(0, 0.5f));
    }
    
    private void GoingAbroad()
    {
        if (gameObject.transform.position.y > _halfHeightCamera +1 ||
            gameObject.transform.position.y < -_halfHeightCamera -1 ||
            gameObject.transform.position.x > _halfWidthCamera +1 ||
            gameObject.transform.position.x < -_halfWidthCamera -1)
        {
            _asteroidPool.Release(this);
        }
    }
    
}