using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    [SerializeField] int _poolSize;
    [SerializeField] GameObject _object;

    Queue<GameObject> _pool = new Queue<GameObject>();

    void Start()
    {
        for(int i = 0; i < _poolSize; i++)
        {
            AddNewObjectToPool();
        }
    }

    /// <summary>
    /// créé un objet, lui rajoute le componenet PoolObject et le rajoute à la pool
    /// </summary>
    /// <returns></returns>
    GameObject AddNewObjectToPool()
    {
        GameObject pooledObject = Instantiate(_object);
        PoolObject poolObject;
        if (_object.TryGetComponent<PoolObject>(out PoolObject pObject))
        {
            poolObject = pObject;
        }
        else
        {
            poolObject = pooledObject.AddComponent<PoolObject>();
        }
        poolObject.OriginPool = this;
        ReturnToPool(pooledObject);
        return pooledObject;
    }

    /// <summary>
    /// returns an object to its pool
    /// </summary>
    /// <param name="objectToReturn"></param>
    public void ReturnToPool(GameObject objectToReturn)
    {
        objectToReturn.transform.parent = transform;
        objectToReturn.transform.position = Vector3.zero;
        objectToReturn.SetActive(false);
        _pool.Enqueue(objectToReturn);
    }

    /// <summary>
    /// returns an object removed from the pool and activated
    /// </summary>
    /// <returns></returns>
    public GameObject TakeFromPool(Vector3 pos, Quaternion rot)
    {
        GameObject pooledObject;
        if (_pool.Count == 0)
        {
            print("empty pool case");
            pooledObject = AddNewObjectToPool();
        }
        else
        {
            pooledObject = _pool.Dequeue();
        }
        pooledObject.transform.position = pos;
        pooledObject.transform.rotation = rot;
        pooledObject.SetActive(true);
        pooledObject.GetComponent<PoolObject>().PullFromPool();
        return pooledObject;
    }

    /// <summary>
    /// acces a un objet de la pool (si la pool est vide : une nouvelle instance est créée et rajoutée à la pool
    /// </summary>
    /// <param name="parent"></param>
    /// <returns></returns>
    public GameObject TakeFromPool(Transform parent)
    {
        GameObject pooledObject;
        if (_pool.Count == 0)
        {
            pooledObject = AddNewObjectToPool();
        }
        else
        {
            pooledObject = _pool.Dequeue();
        }
        pooledObject.transform.parent = parent;
        pooledObject.transform.position = parent.position;
        pooledObject.SetActive(true);
        pooledObject.GetComponent<PoolObject>().PullFromPool();
        return pooledObject;
    }

}
