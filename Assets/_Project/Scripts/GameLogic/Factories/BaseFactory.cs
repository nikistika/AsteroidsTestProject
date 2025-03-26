using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class BaseFactory<T> : MonoBehaviour where T: MonoBehaviour
{

    [SerializeField] protected T _prefab;

    [SerializeField] private int _defaultPoolSize;
    [SerializeField] private int _maxPoolSize;

    protected ObjectPool<T> _pool;
    

    void Awake()
    {
        PoolInitialization();
    }


    private void PoolInitialization()
    {
        _pool = new ObjectPool<T>(
            createFunc: () =>
            {
                return ActionCreateObject();

                //var asteroid = Instantiate(_asteroid);
                //asteroid.Construct(this, _dataSpaceShip, _gameOver);
                //asteroid.gameObject.transform.position = GetRandomSpawnPosition();
                //return asteroid;
            },
            actionOnGet: (obj) =>
            {
                ActionGetObject(obj);

                //obj.gameObject.SetActive(true);
                //obj.gameObject.transform.position = GetRandomSpawnPosition();
                //obj.Move();
                //obj.IsObjectParent(true);
            },
            actionOnRelease: (obj) =>
            {
                obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                obj.gameObject.SetActive(false); //не редактируем
            },
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: false,
            defaultCapacity: _defaultPoolSize,
            maxSize: _maxPoolSize
        );
    }

    protected abstract T ActionCreateObject();
    protected abstract void ActionGetObject(T obj);

    public void ReturnObject(T obj)
    {
        _pool.Release(obj);
    }

    public T SpawnObject()
    {
        return _pool.Get();
    }


}
